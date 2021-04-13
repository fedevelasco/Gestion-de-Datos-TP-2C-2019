using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FrbaOfertas
{
    static class FechaAplicacion
    {
        public static DateTime get()
        {
            DateTime localDate = DateTime.Parse(ConfigurationManager.AppSettings["current_date"].ToString());

            return localDate;
        }
    }
}
