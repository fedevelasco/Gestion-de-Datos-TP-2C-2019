using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace FrbaOfertas
{
    public static class ConexionBD
    {
        static string server = ConfigurationManager.AppSettings["server"].ToString();
        static string database = ConfigurationManager.AppSettings["database"].ToString();
        static string user = ConfigurationManager.AppSettings["user"].ToString();
        static string password = ConfigurationManager.AppSettings["password"].ToString();

        public static SqlConnection getConexion()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=localhost\\" + server + ";Initial Catalog=" + database + ";user=" + user + ";password=" + password + "";
            conn.Open();
            return conn;

        }

        public static DataTable listarDatos(string consulta)
        {
            SqlConnection conn = ConexionBD.getConexion();
            var tabla = new DataTable();
            try
            {
                using (var adaptador = new SqlDataAdapter(consulta, conn))
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

        public static SqlDataReader comando(string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(consulta, getConexion());
            SqlDataReader ejecutar = sqlCommand.ExecuteReader();
            return ejecutar;
        }
    }
}
