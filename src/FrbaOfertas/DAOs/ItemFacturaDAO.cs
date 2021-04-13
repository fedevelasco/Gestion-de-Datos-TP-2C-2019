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
    public static class ItemFacturaDAO
    {
        public static bool agregarItemFactura(Item_Factura item_factura, int factura_id)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Item_Factura VALUES (@oferta_id, @factura_id, @importe, @cantidad)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@oferta_id", item_factura.oferta.id);
                cmd.Parameters.AddWithValue("@factura_id", factura_id);
                cmd.Parameters.AddWithValue("@importe", item_factura.importe);
                cmd.Parameters.AddWithValue("@cantidad", item_factura.cantidad);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar ItemFactura");
                return false;
            }
        }
    }
}
