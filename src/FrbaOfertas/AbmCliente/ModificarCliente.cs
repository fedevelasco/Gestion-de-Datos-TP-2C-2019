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
    public partial class ModificarCliente : Form
    {
        HomeCliente form_home;
        Cliente cliente;

        public ModificarCliente(HomeCliente form_anterior,Cliente unCliente)
        {
            InitializeComponent();
            form_home = form_anterior;
            cliente = unCliente;
        }

        private void ModificarCliente_Load(object sender, EventArgs e)
        {
            txtNombre.Text = cliente.nombre;
            txtApellido.Text = cliente.apellido;
            txtDni.Text = cliente.dni;
            txtMail.Text = cliente.mail;
            txtTelefono.Text = cliente.telefono;
            dtmFechaNacimiento.Text = cliente.fecha_nacimiento.ToString();
            txtDireccion.Text = cliente.direccion;
            txtCP.Text = cliente.codigo_postal.ToString();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                cliente.nombre = txtNombre.Text;
                cliente.apellido = txtApellido.Text;
                cliente.dni = txtDni.Text;
                cliente.mail = txtMail.Text;
                cliente.telefono = txtTelefono.Text;
                cliente.fecha_nacimiento = DateTime.Parse(dtmFechaNacimiento.Value.ToString());
                cliente.direccion = txtDireccion.Text;
                cliente.codigo_postal = int.Parse(txtCP.Text);
                if (ClienteDAO.modificarCliente(cliente))
                {
                    MessageBox.Show("Cliente modificado correctamente");
                    this.form_home.cargarDatos();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al modificar cliente");
                }

            }
        }

        private bool datosValidos()
        {
            return (txtNombre.Text == "" || txtApellido.Text == "" || txtDni.Text == "" || txtMail.Text == "" || txtTelefono.Text == "" || dtmFechaNacimiento.Text == "" || txtDireccion.Text == "" || txtCP.Text == "");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
