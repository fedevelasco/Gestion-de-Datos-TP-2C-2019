using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaOfertas.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace FrbaOfertas.DAOs
{
    public static class OfertaDAO
    {
        public static string consulta = "SELECT O.id,O.descripcion,O.fecha_publicacion,O.fecha_vencimiento,O.precio_oferta,O.cantidad_disponible,O.cantidad_max_cliente FROM JARDCOUD.Oferta O WHERE O.cantidad_disponible > 0 AND O.fecha_publicacion <= '" + FechaAplicacion.get().Date.ToString("yyyy-MM-dd HH:mm:ss") + "' AND O.fecha_vencimiento >= '"+ FechaAplicacion.get().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'";

        public static DataTable listarDatos()
        {
            string consulta = OfertaDAO.consulta;
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable listarOfertasDeProveedor(int proveeedor_id,DateTime fecha_inicio, DateTime fecha_fin)
        {
            SqlConnection conn = ConexionBD.getConexion();
            var tabla = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("JARDCOUD.listar_ofertas_de_proveedor", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@proveedor_id", proveeedor_id);
                cmd.Parameters.AddWithValue("@fecha_desde", fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_hasta", fecha_fin);

                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                return tabla;
            }
            conn.Close();
            return tabla;
        }

        public static bool agregarOferta(Oferta oferta)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Oferta VALUES (@prov_id,@cod, @desc, @fpub, @fven, @poferta, @plista, @descuento, @disponible, @max)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@prov_id", oferta.proveedor.id);
                cmd.Parameters.AddWithValue("@cod", oferta.codigo);
                cmd.Parameters.AddWithValue("@desc", oferta.descripcion);
                cmd.Parameters.AddWithValue("@fpub", oferta.fecha_publicacion);
                cmd.Parameters.AddWithValue("@fven", oferta.fecha_vencimiento);
                cmd.Parameters.AddWithValue("@poferta", oferta.precio_oferta);
                cmd.Parameters.AddWithValue("@plista", oferta.precio_lista);
                cmd.Parameters.AddWithValue("@descuento", oferta.descuento);
                cmd.Parameters.AddWithValue("@disponible", oferta.cantidad_disponible);
                cmd.Parameters.AddWithValue("@max", oferta.cantidad_max_cliente);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Oferta");
                return false;
            }
        }

        public static bool existeCodigoOferta(string oferta_codigo)
        {
            string consulta = "select id from JARDCOUD.Oferta where codigo='" + oferta_codigo + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static DataTable buscar_ofertas(string descripcion, string precio_min, string precio_max)
        {
            string consulta_descripcion = null, consulta_precio_min = null, consulta_precio_max = null, consulta_where = null, consulta;

            if (!string.IsNullOrEmpty(descripcion))
            {
                consulta_descripcion = "O.descripcion LIKE '%" + descripcion + "%'";
                consulta_where = " AND ";
            }
            if (!string.IsNullOrEmpty(precio_min))
            {
                consulta_precio_min = "O.precio_oferta >= " + precio_min;
                if (!string.IsNullOrEmpty(descripcion))
                {
                    consulta_precio_min = "AND " + consulta_precio_min;
                }
                consulta_where = " AND ";
            }

            if (!string.IsNullOrEmpty(precio_max))
            {
                consulta_precio_max = "O.precio_oferta <=" + precio_max;
                if (!string.IsNullOrEmpty(descripcion) || (!string.IsNullOrEmpty(precio_min)))
                {
                    consulta_precio_max = "AND " + consulta_precio_max;
                }
                consulta_where = " AND ";
            }

            consulta = string.Format(OfertaDAO.consulta + consulta_where + consulta_descripcion + consulta_precio_min + consulta_precio_max);
            //MessageBox.Show(consulta);
            return ConexionBD.listarDatos(consulta);
        }

        public static bool actualizarCantidadDisponible(Oferta oferta)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Oferta SET cantidad_disponible = @cantidad WHERE id=@oferta_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@cantidad", oferta.cantidad_disponible);
                cmd.Parameters.AddWithValue("@oferta_id", oferta.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar Oferta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
