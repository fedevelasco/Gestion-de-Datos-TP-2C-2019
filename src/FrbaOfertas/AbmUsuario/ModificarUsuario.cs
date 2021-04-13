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
    public partial class ModificarUsuario : Form
    {
        Usuario usuario;

        public ModificarUsuario(Usuario unUsuario)
        {
            InitializeComponent();
            usuario = unUsuario;
        }

        private void ModificarUsuario_Load(object sender, EventArgs e)
        {
            txtContraseñaAnterior.PasswordChar = '*';
            txtContraseñaNueva.PasswordChar = '*';
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string old_password = txtContraseñaAnterior.Text;
            string new_password = txtContraseñaNueva.Text;

            if (old_password == "" || new_password == "")
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                if (UsuarioDAO.validarUsuario(usuario.username, old_password))
                {
                    if(UsuarioDAO.modificarUsuario(usuario,new_password))
                    {
                        MessageBox.Show("Usuario modificado correctamente");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar usuario");
                    }
                }
                else
                {
                    MessageBox.Show("Datos invalidos");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
