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
    public static class ListadoEstadisticoFacadeDAO
    {
        public static DataTable listarDatos(int consulta_index, int anio, int semestre)
        {
            string[] consultas = new string[] {
                "JARDCOUD.top_proveedores_mayor_descuento",
                "JARDCOUD.top_proveedores_mayor_facturacion"
            };

            string consulta = consultas[consulta_index];

            SqlConnection conn = ConexionBD.getConexion();
            var tabla = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@semestre", semestre);

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
    }
}
