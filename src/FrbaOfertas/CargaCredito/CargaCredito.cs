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

namespace FrbaOfertas.CargaCredito
{
    public partial class CargaCredito : Form
    {
        public Intermedia form_intermedia { get; set; }
        public SeleccionarCliente form_clientes { get; set; }
        Cliente cliente;

        public CargaCredito(Intermedia intermedia)
        {
            InitializeComponent();
            form_intermedia = intermedia;
            cliente = ClienteDAO.getClienteDesdeUsuario(form_intermedia.usuario.username);
        }

        public CargaCredito(SeleccionarCliente form,Cliente unCliente)
        {
            InitializeComponent();
            form_clientes = form;
            cliente = unCliente;
        }

        private void CargaCredito_Load(object sender, EventArgs e)
        {
            cmbTipoPago.DataSource = TipoPagoDAO.listarDatosTarjetas();
            cmbTipoPago.DisplayMember = "descripcion"; //muestra solo los datos de esa columna 
            cmbTipoPago.ValueMember = "id";
            cmbTipoPago.SelectedIndex = -1; // para que no seleccione nada

            txtNombreCliente.Text = cliente.nombre + " " + cliente.apellido;
            txtFecha.Text = FechaAplicacion.get().ToString("dd/MM/yyyy");

            txtNumeroTarjeta1.MaxLength = 4;
            txtNumeroTarjeta2.MaxLength = 4;
            txtNumeroTarjeta3.MaxLength = 4;
            txtNumeroTarjeta4.MaxLength = 4;
            txtMes.MaxLength = 2;
            txtAño.MaxLength = 2;
            txtCodigoSeguridad.MaxLength = 3;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Hay campos incompletos o campos invalidos");
            }
            else
            {
                Modelo.CargaCredito cargaCredito = new Modelo.CargaCredito();
                cargaCredito.cliente = cliente;
                cargaCredito.fecha = FechaAplicacion.get();
                cargaCredito.monto = int.Parse(numMonto.Value.ToString());
                cargaCredito.tipo_pago = new TipoPago() { id = Int32.Parse(cmbTipoPago.SelectedValue.ToString()) };
                cargaCredito.numero_tarjeta = txtNumeroTarjeta1.Text + "-" + txtNumeroTarjeta2.Text + "-" + txtNumeroTarjeta3.Text + "-" + txtNumeroTarjeta4.Text;
                cargaCredito.fecha_vencimiento = txtMes.Text + "/" + txtAño.Text;
                cargaCredito.cod_seguridad = txtCodigoSeguridad.Text;


                cliente.credito += cargaCredito.monto = int.Parse(numMonto.Value.ToString());

                if (CargaCreditoDAO.agregarcargaCredito(cargaCredito) && ClienteDAO.actualizarCreditos(cliente))
                {
                    MessageBox.Show("Carga de credito realizada correctamente", "Oferta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (form_clientes != null)
                    {
                        form_clientes.cargarDatos();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al realizar Carga de Credito");
                } 

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        {
            return ( cmbTipoPago.Text == "" || txtNumeroTarjeta1.Text == "" || txtNumeroTarjeta2.Text == "" || txtNumeroTarjeta3.Text == "" || txtNumeroTarjeta4.Text == "" || txtMes.Text == "" || (int.Parse(txtMes.Text) >= 13) || (int.Parse(txtMes.Text) <= 0) || txtAño.Text == "" || (int.Parse(txtAño.Text) <= 0) || txtCodigoSeguridad.Text == "");
        }
    }
}
