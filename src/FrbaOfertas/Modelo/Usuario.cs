using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas.Modelo
{
    public class Usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int login_intentos { get; set; }
        public bool habilitado { get; set; }
        public List <Rol> roles { get; set; }
        public Cliente cliente { get; set; }
        public Proveedor proveedor { get; set; }
    }
}
