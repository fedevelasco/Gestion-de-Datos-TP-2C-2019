using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using FrbaOfertas.Modelo;
using FrbaOfertas.DAOs;


namespace FrbaOfertas.DAOs
{
    public static class CompraDAO
    {
        public static int agregarCompra(Compra compra)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Compra VALUES (@cliente_id, @oferta_id, @fecha, @cantidad);  SELECT SCOPE_IDENTITY()");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@cliente_id", compra.cliente.id);
                cmd.Parameters.AddWithValue("@oferta_id", compra.oferta.id);
                cmd.Parameters.AddWithValue("@fecha", compra.fecha);
                cmd.Parameters.AddWithValue("@cantidad", compra.cantidad);

                int compra_id_generado = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return compra_id_generado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Compra");
                return -1;
            }
        }
    }
}
