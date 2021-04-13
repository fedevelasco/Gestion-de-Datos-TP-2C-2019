using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using FrbaOfertas.Modelo;

namespace FrbaOfertas.DAOs
{
    public static class FuncionalidadDAO
    {
        public static DataTable listarDatos()
        {
            string consulta = "SELECT * FROM JARDCOUD.Funcionalidad";
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable buscarFuncionalidades(int rol_id)
        {
            string consulta = "SELECT F.id,F.nombre FROM JARDCOUD.Funcionalidad F INNER JOIN JARDCOUD.Rol_Funcionalidad RF ON F.id = RF.funcionalidad_id WHERE RF.rol_id=" + rol_id;
            return ConexionBD.listarDatos(consulta);
        }

        public static List<Funcionalidad> obtener_todas_funcionalidades()
        {
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            string consulta = string.Format(@"SELECT * FROM JARDCOUD.Funcionalidad");
            SqlConnection conn = ConexionBD.getConexion();
            SqlCommand cmd = new SqlCommand(consulta, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader[0].ToString());
                string nombre = reader[1].ToString();

                Funcionalidad funcionalidad = new Funcionalidad();
                funcionalidad.id = id;
                funcionalidad.nombre = nombre;

                funcionalidades.Add(funcionalidad);
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return funcionalidades;
        }

        public static List<Funcionalidad> obtener_funcionalidades_de_rol(Rol rol)
        {
            int rol_id = rol.id;
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            string consulta = string.Format(@"SELECT F.id,F.nombre FROM JARDCOUD.Rol R INNER JOIN JARDCOUD.Rol_Funcionalidad RF ON R.id = RF.rol_id INNER JOIN JARDCOUD.Funcionalidad F ON RF.funcionalidad_id = F.id WHERE R.id=" + rol_id);
            SqlConnection conn = ConexionBD.getConexion();
            SqlCommand cmd = new SqlCommand(consulta, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader[0].ToString());
                string nombre = reader[1].ToString();

                Funcionalidad funcionalidad = new Funcionalidad();
                funcionalidad.id = id;
                funcionalidad.nombre = nombre;

                funcionalidades.Add(funcionalidad);
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return funcionalidades;
        }
    }
}
