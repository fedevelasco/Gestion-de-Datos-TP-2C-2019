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

namespace FrbaOfertas.AbmCliente
{
    public partial class HomeCliente : Form
    {
        public HomeCliente()
        {
            InitializeComponent();
        }

        private void HomeCliente_Load(object sender, EventArgs e)
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
            dgvClientes.DataSource = ClienteDAO.buscar_clientes(txtNombre.Text,txtApellido.Text,txtDni.Text,txtMail.Text);
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

        private void btnCrear_Click(object sender, EventArgs e)
        {
            (new AbmUsuario.ListadoUsuariosSinRol(this)).ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.RowCount != 0)
            {
                Cliente cliente_seleccionado = this.get_cliente_seleccionado();
                (new AbmCliente.ModificarCliente(this, cliente_seleccionado)).ShowDialog();
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

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.RowCount != 0)
            {
                string mensaje;
                Cliente cliente_seleccionado = this.get_cliente_seleccionado();

                if (cliente_seleccionado.habilitado)
                {
                    mensaje = "¿Está ud. seguro de querer deshabilitar el Cliente: " + cliente_seleccionado.nombre + "?";
                }
                else
                {
                    mensaje = "¿Está ud. seguro de querer habilitar el Cliente: " + cliente_seleccionado.nombre + "?";
                }
                if (MessageBox.Show(mensaje, "ABM Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) //si selecciona que si
                {
                    if (ClienteDAO.deshabilitarCliente(cliente_seleccionado))
                    {
                        MessageBox.Show("Operacion exitosa");
                        this.cargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("Se produjo un error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un Cliente");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            (new AbmUsuario.CrearUsuario(this)).ShowDialog();
        }
    }
}
