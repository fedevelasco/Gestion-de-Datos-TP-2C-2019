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

namespace FrbaOfertas.AbmUsuario
{
    public partial class ListadoUsuariosSinRol : Form
    {
        public AbmCliente.HomeCliente form_cliente { get; set; }
        public AbmProveedor.HomeProveedor form_proveedor { get; set; }

        public ListadoUsuariosSinRol(AbmCliente.HomeCliente form_anterior)
        {
            InitializeComponent();
            form_cliente = form_anterior;
        }

        public ListadoUsuariosSinRol(AbmProveedor.HomeProveedor form_anterior)
        {
            InitializeComponent();
            form_proveedor = form_anterior;
        }

        private void ListadoUsuariosSinRol_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            dgvUsuarios.DataSource = UsuarioDAO.listarUsuariosSinRol();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;

            dgvUsuarios.DataSource = UsuarioDAO.buscar_usuarios_sin_rol(username);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.RowCount != 0)
            {
                Usuario usuario = this.get_usuario_seleccionado();
                if (form_cliente != null)
                {
                    if (ClienteDAO.getClienteDesdeUsuario(usuario.username) != null) // Si ya tenia un cliente asociado
                    {
                        if (UsuarioDAO.agregarRol(usuario, 2))
                        {
                            MessageBox.Show("Cliente reasignado correctamente");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al reasignar rol Cliente");
                        }
                    }
                    else
                    {
                        (new AbmCliente.CrearCliente(this, usuario, 2)).ShowDialog();
                    }
                }
                else
                {
                    if (ProveedorDAO.getProveedorDesdeUsuario(usuario.username) != null) // Si ya tenia un proveedor asociado
                    {
                        if (UsuarioDAO.agregarRol(usuario, 3))
                        {
                            MessageBox.Show("Proveedor reasignado correctamente");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al reasignar rol Proveedor");
                        }
                    }
                    else
                    {
                        (new AbmProveedor.CrearProveedor(this, usuario, 3)).ShowDialog();
                    }
                }  
            }
            else
            {
                MessageBox.Show("Seleccione un usuario");
            }
        }

        private Usuario get_usuario_seleccionado()
        {
            int usuario_id = int.Parse(dgvUsuarios.SelectedCells[0].Value.ToString());
            string usuario_username = dgvUsuarios.SelectedCells[1].Value.ToString();
            int usuario_intentos = int.Parse(dgvUsuarios.SelectedCells[2].Value.ToString());
            bool usuario_habilitado = bool.Parse(dgvUsuarios.SelectedCells[3].Value.ToString());

            Usuario usuario_seleccionado = new Usuario();
            usuario_seleccionado.id = usuario_id;
            usuario_seleccionado.username = usuario_username;
            usuario_seleccionado.login_intentos = usuario_intentos;
            usuario_seleccionado.habilitado = usuario_habilitado;

            return usuario_seleccionado;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
