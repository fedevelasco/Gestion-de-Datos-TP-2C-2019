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

namespace FrbaOfertas
{
    public partial class Inicio : Form
    {
        private int intentos = 0;

        public Inicio()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                if (UsuarioDAO.existeUsernameHabilitado(txtUsuario.Text))//verifico que exista el username
                {
                    string username = txtUsuario.Text;
                    string password = txtContraseña.Text;

                    if (UsuarioDAO.validarUsuario(username, password))
                    {
                        UsuarioDAO.setIntentosFallidos(username, 0);

                        Usuario usuario = UsuarioDAO.getUsuario(username);

                        if (UsuarioDAO.tieneRolAsignado(usuario))
                        {
                            if (UsuarioDAO.tieneRolHabilitado(usuario))
                            {
                                Intermedia intermedia = new Intermedia(this, usuario);
                                intermedia.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("No tiene ningun rol habilitado");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tiene ningun rol asignado");
                            (new AbmUsuario.SelectorRol(usuario)).ShowDialog();
                        }        
                    }
                    else
                    {
                        intentos = UsuarioDAO.obtenerLoginIntentos(username); //intentos fallidos antes
                        intentos++; //intentos fallidos ahora
                        if (intentos <= 3)
                        {
                            int intentos_disponibles = 3 - intentos;
                            MessageBox.Show("Contraseña incorrecta. Cantidad de intentos disponibles:" + intentos_disponibles.ToString());
                            UsuarioDAO.setIntentosFallidos(username, intentos);
                        }
                        else
                        {
                            MessageBox.Show("Usuario inhabilitado");
                            UsuarioDAO.inhabilitarUsuario(username);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("El Usuario es invalido o no se encuentra habilitado");
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            (new AbmUsuario.CrearUsuario(this)).ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Cierro el programa
        }
    }
}
