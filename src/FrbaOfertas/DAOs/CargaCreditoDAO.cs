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
    public static class CargaCreditoDAO
    {
        public static bool agregarcargaCredito(FrbaOfertas.Modelo.CargaCredito cargaCredito)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.CargaCredito VALUES (@fech,@clie_id, @tpago_id, @monto, @tarj, @fven, @cod)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@fech", cargaCredito.fecha);
                cmd.Parameters.AddWithValue("@clie_id", cargaCredito.cliente.id);
                cmd.Parameters.AddWithValue("@tpago_id", cargaCredito.tipo_pago.id);
                cmd.Parameters.AddWithValue("@monto", cargaCredito.monto);
                cmd.Parameters.AddWithValue("@tarj", cargaCredito.numero_tarjeta);
                cmd.Parameters.AddWithValue("@fven", cargaCredito.fecha_vencimiento);
                cmd.Parameters.AddWithValue("@cod", cargaCredito.cod_seguridad);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar CargaCredito");
                return false;
            }
        }
    }
}
