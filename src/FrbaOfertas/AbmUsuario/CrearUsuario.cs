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
    public partial class CrearUsuario : Form
    {
        Inicio inicio = null;
        public AbmCliente.HomeCliente home_cliente = null;
        public AbmProveedor.HomeProveedor home_proveedor = null;

        public CrearUsuario(Inicio unFormAnterior)
        {
            InitializeComponent();
            inicio = unFormAnterior;
        }

        public CrearUsuario(AbmCliente.HomeCliente unFormAnterior)
        {
            InitializeComponent();
            home_cliente = unFormAnterior;
        }

        public CrearUsuario(AbmProveedor.HomeProveedor unFormAnterior)
        {
            InitializeComponent();
            home_proveedor = unFormAnterior;
        }

        private void CrearUsuario_Load(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = '*';
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                string username = txtUsuario.Text;
                string password = txtContraseña.Text;

                if (!UsuarioDAO.existeUsername(username))
                {
                    if (UsuarioDAO.agregarUsuario(username, password))
                    {
                        MessageBox.Show("Usuario creado");
                        Usuario usuario = UsuarioDAO.getUsuario(username);
                        if (inicio != null)
                        {
                            this.Close();
                            (new SelectorRol(usuario)).ShowDialog(); 
                        }
                        else if (home_cliente != null)
                        {
                            (new AbmCliente.CrearCliente(this,usuario,2)).ShowDialog();
                        }
                        else
                        {
                            (new AbmProveedor.CrearProveedor(this, usuario, 3)).ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al crear usuario");
                    }        
                }
                else
                {
                    MessageBox.Show("Nombre de usuario repetido");
                }     
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            if (inicio != null)
            {
                inicio.Visible = true;
            }
        }
    }
}
