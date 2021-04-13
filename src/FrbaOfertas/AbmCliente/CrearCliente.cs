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
    public partial class CrearCliente : Form
    {
        AbmUsuario.ListadoUsuariosSinRol form_listado = null;
        AbmUsuario.SelectorRol form_rol = null;
        AbmUsuario.CrearUsuario form_usuario = null;
        Usuario usuario;
        int rol_id;

        public CrearCliente(AbmUsuario.ListadoUsuariosSinRol form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_listado = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        public CrearCliente(AbmUsuario.SelectorRol form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_rol = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        public CrearCliente(AbmUsuario.CrearUsuario form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_usuario = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                if (ClienteDAO.existeCliente(txtDni.Text))
                {
                    MessageBox.Show("Cliente repetido. Verifique datos");
                }
                else
                {
                    Cliente cliente = new Cliente();
                    cliente.nombre = txtNombre.Text;
                    cliente.apellido = txtApellido.Text;
                    cliente.dni = txtDni.Text;
                    cliente.mail = txtMail.Text;
                    cliente.telefono = txtTelefono.Text;
                    cliente.fecha_nacimiento = DateTime.Parse(dtmFechaNacimiento.Value.ToString());
                    cliente.direccion = txtDireccion.Text;
                    cliente.codigo_postal = int.Parse(txtCP.Text);

                    if (ClienteDAO.agregarCliente(cliente, usuario.id) && UsuarioDAO.agregarRol(usuario, rol_id))
                    {
                        MessageBox.Show("Cliente creado");
                        this.Close();
                        if (form_listado != null)
                        {
                            form_listado.form_cliente.cargarDatos();
                            form_listado.Close();
                        }
                        else if (form_rol != null)
                        {
                            form_rol.Close();
                        }
                        else
                        {
                            form_usuario.home_cliente.cargarDatos();
                            form_usuario.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al crear cliente");
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        {
            return (txtNombre.Text == "" || txtApellido.Text == "" || txtDni.Text == "" || txtMail.Text == "" || txtTelefono.Text == "" || dtmFechaNacimiento.Text == "" || txtDireccion.Text == "" || txtCP.Text == "");
        }
    }
}
