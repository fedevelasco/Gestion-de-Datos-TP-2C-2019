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
    public partial class HomeUsuario : Form
    {
        public Intermedia form_anterior { get; set; }

        public HomeUsuario(Intermedia intermedia)
        {
            InitializeComponent();
            form_anterior = intermedia;
        }

        private void HomeUsuario_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            this.limpiar_inputs();
            dgvUsuarios.DataSource = UsuarioDAO.listarDatos();

            cmbHabilitado.Items.Add("Si");
            cmbHabilitado.Items.Add("No");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string habilitado_indice = cmbHabilitado.Text;
            string habilitado = null;

            if (habilitado_indice == "Si")
            {
                habilitado = "1";
            }
            else if (habilitado_indice == "No")
            {
                habilitado = "0";
            }

            dgvUsuarios.DataSource = UsuarioDAO.buscar_usuarios(username,habilitado);

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.limpiar_inputs();
        }

        private void limpiar_inputs()
        {
            txtUsername.Text = "";
            cmbHabilitado.SelectedIndex = -1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.RowCount != 0)
            {
                Usuario usuario = this.get_usuario_seleccionado();
                (new ModificarUsuario(usuario)).ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario");
            }
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.RowCount != 0)
            {
                Usuario usuario_seleccionado = this.get_usuario_seleccionado();
                string mensaje = "¿Está ud. seguro de querer deshabilitar el usuario " + usuario_seleccionado.username + "?";

                if (usuario_seleccionado.habilitado)
                {
                    mensaje = "¿Está ud. seguro de querer deshabilitar el Usuario: " + usuario_seleccionado.username + "?";
                }
                else
                {
                    mensaje = "¿Está ud. seguro de querer habilitar el Usuario: " + usuario_seleccionado.username + "?";
                }

                if (MessageBox.Show(mensaje, "ABM Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) //si selecciona que si
                {
                    if (form_anterior.usuario.id == usuario_seleccionado.id)
                    {
                        if (MessageBox.Show("¿Está a punto de inhabilitar el Usuario en el que se encuentra logueado, se cerrará la sesión al finalizar, desea continuar?", "ABM Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            UsuarioDAO.darDeBajaUsuario(usuario_seleccionado);
                            Application.Exit();
                            Application.Restart();
                        }
                    }
                    else
                    {
                        UsuarioDAO.darDeBajaUsuario(usuario_seleccionado);
                        MessageBox.Show("Operacion exitosa");
                        this.cargarDatos();
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
