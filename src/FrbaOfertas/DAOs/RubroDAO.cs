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
    public static class RubroDAO
    {
        public static DataTable listarDatos()
        {
            string consulta = "SELECT * FROM JARDCOUD.Rubro";
            return ConexionBD.listarDatos(consulta);
        }

        public static Rubro getRubro(string unaDescripcion)
        {
            string consulta = "SELECT * FROM JARDCOUD.Rubro WHERE descripcion='" + unaDescripcion + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);
            if (registro.Read())
            {
                Rubro rubro = new Rubro();

                rubro.id = registro.GetInt32(0);
                rubro.descripcion = registro.GetString(1);

                return rubro;
            }
            else
            {
                return null;
            }
        }
    }
}
