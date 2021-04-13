using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Item_Factura
    {
        public int id { get; set; }
        public Oferta oferta { get; set; }
        public double importe { get; set; }
        public int cantidad { get; set; }
    }
}
