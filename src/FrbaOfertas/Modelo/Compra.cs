using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Compra
    {
        public int id { get; set; }
        public Cliente cliente { get; set; }
        public Oferta oferta { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad { get; set; }
    }
}
