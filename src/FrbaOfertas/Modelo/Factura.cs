using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Factura
    {
        public int id { get; set; }
        public Proveedor proveedor { get; set; }
        public int numero { get; set; }
        public double total { get; set; }
        public DateTime fecha { get; set; }
        public List<Item_Factura> items_facturas { get; set; }
    }
}
