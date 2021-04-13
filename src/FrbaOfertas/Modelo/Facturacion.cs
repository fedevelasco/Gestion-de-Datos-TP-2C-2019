using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Facturacion
    {
        public int id { get; set; }
        public Proveedor proveedor { get; set; }
        public double importe { get; set; }
        public int numero { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
    }
}
