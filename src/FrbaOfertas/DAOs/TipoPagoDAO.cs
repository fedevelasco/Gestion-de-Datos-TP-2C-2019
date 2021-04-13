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
    public static class TipoPagoDAO
    {
        public static DataTable listarDatos()
        {
            string consulta = "SELECT * FROM JARDCOUD.TipoPago";
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable listarDatosTarjetas()
        {
            string consulta = "SELECT * FROM JARDCOUD.TipoPago WHERE descripcion != 'Efectivo'";
            return ConexionBD.listarDatos(consulta);
        }
    }
}
