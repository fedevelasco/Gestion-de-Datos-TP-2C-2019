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
    public partial class SelectorRol : Form
    {
        Usuario usuario;

        public SelectorRol(Usuario unUsuario)
        {
            InitializeComponent();
            usuario = unUsuario;
        }

        private void SelectorRol_Load(object sender, EventArgs e)
        {
            cmbRol.DataSource = RolDAO.listarRolesUsuario();
            cmbRol.DisplayMember = "nombre"; //muestra solo los datos de esa columna 
            cmbRol.ValueMember = "id";
            cmbRol.SelectedIndex = -1; // para que no seleccione nada
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cmbRol.Text == "")
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                int rol_id = Int32.Parse(cmbRol.SelectedValue.ToString());

                this.selector(cmbRol.Text, usuario, rol_id);
            }
        }

        private void selector(string nombreRol, Usuario unUsuario, int rol_id)
        {
            switch (nombreRol)
            {
                case "Cliente":
                    if (ClienteDAO.getClienteDesdeUsuario(unUsuario.username) != null)  // Si ya tenia un cliente asociado
                    {
                        if (UsuarioDAO.agregarRol(usuario, rol_id))
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
                        (new AbmCliente.CrearCliente(this, unUsuario, rol_id)).ShowDialog();
                    }
                    break;
                case "Proveedor":
                    if (ProveedorDAO.getProveedorDesdeUsuario(usuario.username) != null) // Si ya tenia un proveedor asociado
                    {
                        if (UsuarioDAO.agregarRol(usuario, rol_id))
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
                        (new AbmProveedor.CrearProveedor(this, unUsuario, rol_id)).ShowDialog();
                    }
                    break;
                default:
                    //Le asigno el rol directamente
                    if (UsuarioDAO.agregarRol(unUsuario, rol_id))
                    {
                        MessageBox.Show("Rol asignado");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar rol");
                        Application.Exit();
                    }
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
