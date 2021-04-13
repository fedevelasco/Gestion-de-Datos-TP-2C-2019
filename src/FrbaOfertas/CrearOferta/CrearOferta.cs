using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaOfertas.Modelo;
using FrbaOfertas.DAOs;

namespace FrbaOfertas.CrearOferta
{
    public partial class CrearOferta : Form
    {
        public Intermedia form_intermedia { get; set; }
        Proveedor proveedor;

        public CrearOferta(Intermedia form_anterior)
        {
            InitializeComponent();
            form_intermedia = form_anterior;
            proveedor = ProveedorDAO.getProveedorDesdeUsuario(form_intermedia.usuario.username);
        }

        public CrearOferta(Proveedor unProveedor)
        {
            InitializeComponent();
            proveedor = unProveedor;
        }

        private void CrearOferta_Load(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else if (!this.fechasValidas())
            {
                MessageBox.Show("La fecha de publicación debe ser mayor igual a la actual y la fecha de vencimiento debe ser mayor a la de publicación");
            }
            else if (!this.preciosValidos())
            {
                MessageBox.Show("El precio de oferta debe ser menor o igual al precio de lista");
            }
            else
            {
                Oferta oferta = new Oferta();
                oferta.proveedor = proveedor;
                oferta.cantidad_disponible = int.Parse(numCantidadDisponible.Value.ToString());
                oferta.cantidad_max_cliente = int.Parse(numCantidadMaxima.Value.ToString());
                oferta.codigo = this.obtenerCodigoOfertaAleatorio();
                oferta.descripcion = txtDescripcion.Text;
                oferta.precio_lista = double.Parse(numPrecioLista.Value.ToString());
                oferta.precio_oferta = double.Parse(numPrecioOferta.Value.ToString());
                oferta.descuento = (double.Parse(numPrecioLista.Value.ToString()) - double.Parse(numPrecioOferta.Value.ToString())) / double.Parse(numPrecioLista.Value.ToString());
                oferta.fecha_vencimiento = DateTime.Parse(dtmFechaVencimiento.Value.ToString("yyyy-MM-dd"));
                oferta.fecha_publicacion = DateTime.Parse(dtmFechaPublicacion.Value.ToString("yyyy-MM-dd"));

                if (OfertaDAO.agregarOferta(oferta))
                {
                    MessageBox.Show("Oferta creada correctamente", "Oferta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al crear Oferta");
                } 

            }
        }

        private string obtenerCodigoOfertaAleatorio()
        {
            string codigoAleatorio = this.obtenerCodigoAleatorio(10);
            while (OfertaDAO.existeCodigoOferta(codigoAleatorio)) //mientras exista, genera otro
            {
                codigoAleatorio = this.obtenerCodigoAleatorio(10);
            }
            return codigoAleatorio;
        }

        public string obtenerCodigoAleatorio(int longitud)
        {
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        {
            return (txtDescripcion.Text == "" ||dtmFechaPublicacion.Text == "" || dtmFechaVencimiento.Text == "");
        }

        private bool fechasValidas()
        {
            return (dtmFechaPublicacion.Value.Date >= FechaAplicacion.get().Date && dtmFechaVencimiento.Value.Date > dtmFechaPublicacion.Value.Date);
        }

        private bool preciosValidos()
        {
            double precio_oferta = double.Parse(numPrecioOferta.Value.ToString());
            double precio_lista = double.Parse(numPrecioLista.Value.ToString());

            return precio_oferta <= precio_lista;
        }
    }
}
