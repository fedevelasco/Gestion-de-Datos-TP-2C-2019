using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Oferta
    {
        public int id { get; set; }
        public Proveedor proveedor { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public double precio_oferta { get; set; }
        public double precio_lista { get; set; }
        public double descuento { get; set; }
        public int cantidad_disponible { get; set; }
        public int cantidad_max_cliente { get; set; }
        public List<Item_Factura> items_facturas { get; set; }
        public List<Compra> compras { get; set; }
    }
}
