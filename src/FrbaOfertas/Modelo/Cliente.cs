using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public int codigo_postal { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public double credito { get; set; }
        public Usuario usuario { get; set; }
        public bool habilitado { get; set; }
        public List<Cupon> cupones { get; set; }
        public List<CargaCredito> cargas_de_credito { get; set; }
    }
}
