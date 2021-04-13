using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class CargaCredito
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Cliente cliente { get; set; }
        public TipoPago tipo_pago { get; set; }
        public double monto { get; set; }
        public string numero_tarjeta { get; set; }
        public string fecha_vencimiento { get; set; }
        public string cod_seguridad { get; set; }
    }
}
