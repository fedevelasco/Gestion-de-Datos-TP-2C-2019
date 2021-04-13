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
    public static class FacturaDAO
    {
        public static int agregarFactura(Factura factura)
        {
            try
            {
                //Factura
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Factura VALUES (@proveedor_id, @numero, @total, @fecha);  SELECT SCOPE_IDENTITY()");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@proveedor_id", factura.proveedor.id);
                cmd.Parameters.AddWithValue("@numero", factura.numero);
                cmd.Parameters.AddWithValue("@total", factura.total);
                cmd.Parameters.AddWithValue("@fecha", factura.fecha);

                int factura_id_generado = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return factura_id_generado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Factura");
                return -1;
            }
        }

        public static bool existeNumeroFactura(int numero_factura)
        {
            string consulta = "SELECT id FROM JARDCOUD.Factura WHERE numero=" + numero_factura.ToString();
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }
    }
}
