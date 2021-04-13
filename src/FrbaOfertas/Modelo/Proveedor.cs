using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Proveedor
    {
        public int id { get; set; }
        public string razon_social { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string cuit { get; set; }
        public Rubro rubro { get; set; }
        public string nombre_contacto { get; set; }
        public Usuario usuario { get; set; }
        public bool habilitado { get; set; }
        public List<Facturacion> facturaciones { get; set; }
        public List<Oferta> ofertas { get; set; }
        public List<Factura> facturas { get; set; }
    }
}
