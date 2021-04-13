using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using FrbaOfertas.Modelo;

namespace FrbaOfertas.DAOs
{
    public static class CuponDAO
    {
        public static DataTable listarCupondesDeProveedor(int proveeedor_id, string codigo=null)
        {
            SqlConnection conn = ConexionBD.getConexion();
            var tabla = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("JARDCOUD.listar_cupones_de_proveedor", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@proveedor_id", proveeedor_id);
                if (codigo!="" && codigo !=null)
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                }
                cmd.Parameters.AddWithValue("@fecha", FechaAplicacion.get());

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

        public static bool darBajaCupon(Cliente cliente , FrbaOfertas.Modelo.Cupon cupon)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Cupon SET cliente_id = @cliente_id, canjeado = 1, fecha_de_consumo = @fecha WHERE id=@cupon_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@cliente_id", cliente.id);
                cmd.Parameters.AddWithValue("@fecha", cupon.fecha_de_consumo);
                cmd.Parameters.AddWithValue("@cupon_id", cupon.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al dar de baja el cupon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool existeCodigoCupon(int codigo_cupon)
        {
            string consulta = "SELECT id FROM JARDCOUD.Cupon WHERE codigo=" + codigo_cupon.ToString();
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool agregarCupon(Cupon cupon, int compra_id)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Cupon VALUES (@compra_id, NULL ,@codigo, @fecha_vencimiento, NULL, @canjeado)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@compra_id", compra_id);
                cmd.Parameters.AddWithValue("@codigo", cupon.codigo);
                cmd.Parameters.AddWithValue("@fecha_vencimiento", cupon.fecha_vencimiento);
                cmd.Parameters.AddWithValue("@canjeado", cupon.canjeado);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Cupon");
                return false;
            }
        }
    }
}
