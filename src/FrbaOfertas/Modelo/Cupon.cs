using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Cupon
    {
        public int id { get; set; }
        public Compra compra { get; set; }
        public Cliente cliente { get; set; }
        public string codigo { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public DateTime fecha_de_consumo { get; set; }
        public bool canjeado { get; set; }
    }
}
