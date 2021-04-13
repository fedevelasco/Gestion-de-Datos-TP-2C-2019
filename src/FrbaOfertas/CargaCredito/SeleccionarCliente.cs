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
    public partial class SeleccionarCliente : Form
    {
        public Intermedia form_anterior { get; set; }

        public SeleccionarCliente(Intermedia intermedia)
        {
            InitializeComponent();
            form_anterior = intermedia;
        }

        private void SeleccionarCliente_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            this.limpiar_inputs();
            dgvClientes.DataSource = ClienteDAO.listarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = ClienteDAO.buscar_clientes(txtNombre.Text, txtApellido.Text, txtDni.Text, txtMail.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.limpiar_inputs();
        }

        private void limpiar_inputs()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtMail.Text = "";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.RowCount != 0)
            {
                Cliente cliente_seleccionado = this.get_cliente_seleccionado();

                (new CargaCredito(this,cliente_seleccionado)).ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un Cliente");
            }
        }

        private Cliente get_cliente_seleccionado()
        {
            Cliente cliente_seleccionado = new Cliente();
            cliente_seleccionado.id = int.Parse(dgvClientes.SelectedCells[0].Value.ToString());
            cliente_seleccionado.nombre = dgvClientes.SelectedCells[1].Value.ToString();
            cliente_seleccionado.apellido = dgvClientes.SelectedCells[2].Value.ToString();
            cliente_seleccionado.dni = dgvClientes.SelectedCells[3].Value.ToString();
            cliente_seleccionado.mail = dgvClientes.SelectedCells[4].Value.ToString();
            cliente_seleccionado.telefono = dgvClientes.SelectedCells[5].Value.ToString();
            cliente_seleccionado.direccion = dgvClientes.SelectedCells[6].Value.ToString();
            cliente_seleccionado.codigo_postal = int.Parse(dgvClientes.SelectedCells[7].Value.ToString());
            cliente_seleccionado.fecha_nacimiento = DateTime.Parse(dgvClientes.SelectedCells[8].Value.ToString());
            cliente_seleccionado.credito = double.Parse(dgvClientes.SelectedCells[9].Value.ToString());
            cliente_seleccionado.habilitado = bool.Parse(dgvClientes.SelectedCells[10].Value.ToString());

            return cliente_seleccionado;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
