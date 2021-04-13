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

namespace FrbaOfertas.ComprarOferta
{
    public partial class ComprarOferta : Form
    {
        public Intermedia form_intermedia { get; set; }
        public SeleccionarCliente form_clientes { get; set; }
        Cliente cliente;

        public ComprarOferta(Intermedia intermedia)
        {
            InitializeComponent();
            form_intermedia = intermedia;
            cliente = ClienteDAO.getClienteDesdeUsuario(form_intermedia.usuario.username);
        }

        public ComprarOferta(SeleccionarCliente form, Cliente unCliente)
        {
            InitializeComponent();
            form_clientes = form;
            cliente = unCliente;
        }

        private void ComprarOferta_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            this.limpiar_inputs();
            dgvOfertas.DataSource = OfertaDAO.listarDatos();
            txtCreditosDisponibles.Text = cliente.credito.ToString();
            numCantidad.Value = 1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvOfertas.DataSource = OfertaDAO.buscar_ofertas(txtDescripcion.Text,txtPrecioMinimo.Text,txtPrecioMaximo.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.limpiar_inputs();
        }

        private void limpiar_inputs()
        {
            txtDescripcion.Text = "";
            txtPrecioMinimo.Text = "";
            txtPrecioMaximo.Text = "";
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (this.siguiente())
            {
                Oferta oferta_seleccionada = this.get_oferta_seleccionada();
                int cantidad_ingresada = int.Parse(numCantidad.Value.ToString());

                if (cantidad_ingresada <= 0)
                {
                    MessageBox.Show("La cantidad ingresada es invalida");
                    return;
                }

                if (cantidad_ingresada <= oferta_seleccionada.cantidad_disponible)
                {
                    if(cantidad_ingresada <= oferta_seleccionada.cantidad_max_cliente)
                    {
                        double total_importe = oferta_seleccionada.precio_oferta * cantidad_ingresada;
                        if (cliente.credito >= (total_importe))
                        {
                            if (MessageBox.Show("¿Está a punto de comprar " + cantidad_ingresada.ToString() + " unidad/es de " + oferta_seleccionada.descripcion + " por $" + total_importe + " en total, desea continuar?", "ComprarOferta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                Compra compra = new Compra();
                                compra.oferta = oferta_seleccionada;
                                compra.cliente = cliente;
                                compra.fecha = FechaAplicacion.get();
                                compra.cantidad = cantidad_ingresada;

                                Cupon cupon = new Cupon();
                                //cupon.codigo = this.obtenerCodigoCuponAleatorio().ToString();
                                cupon.fecha_vencimiento = FechaAplicacion.get().AddDays(15); //15 dias desde la compra
                                cupon.canjeado = false;             

                                if (CompraFacadeDAO.agregarCompraYGenerarCupon(compra,cupon))
                                {
                                    if (form_clientes != null)
                                    {
                                        form_clientes.cargarDatos();
                                    }
                                    (new CompraDetalle(this,compra,cupon)).ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("Error al realizar la compra");
                                }
                            }                         
                        }
                        else
                        {
                            MessageBox.Show("No dispone de credito suficiente para realizar la compra");
                        }
                    }
                    else
                    {
                        MessageBox.Show("La cantidad ingresada supera la cantidad maxima por cliente de la oferta");
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad ingresada supera la cantidad disponible de la oferta");
                }
            }
            else
            {
                MessageBox.Show("Datos invalidos");
            }
        }

        private Oferta get_oferta_seleccionada()
        {

            Oferta oferta_seleccionada = new Oferta();
            oferta_seleccionada.id = int.Parse(dgvOfertas.SelectedCells[0].Value.ToString());
            oferta_seleccionada.descripcion = dgvOfertas.SelectedCells[1].Value.ToString();
            oferta_seleccionada.precio_oferta = double.Parse(dgvOfertas.SelectedCells[4].Value.ToString());
            oferta_seleccionada.cantidad_disponible = int.Parse(dgvOfertas.SelectedCells[5].Value.ToString());
            oferta_seleccionada.cantidad_max_cliente = int.Parse(dgvOfertas.SelectedCells[6].Value.ToString());

            return oferta_seleccionada;
        }

        private int obtenerNumeroFacturaAleatorio()
        {
            int numeroAleatorio = new Random().Next(150000, 160000);
            while (FacturaDAO.existeNumeroFactura(numeroAleatorio)) //mientras exista, genera otro
            {
                numeroAleatorio = new Random().Next(150000, 160000);
            }
            return numeroAleatorio;
        }

        private int obtenerCodigoCuponAleatorio()
        {
            int numeroAleatorio = new Random().Next(50000, 60000);
            while (CuponDAO.existeCodigoCupon(numeroAleatorio)) //mientras exista, genera otro
            {
                numeroAleatorio = new Random().Next(50000, 60000);
            }
            return numeroAleatorio;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool siguiente()
        {
            return (dgvOfertas.SelectedRows.Count > 0 );
        }
    }
}
