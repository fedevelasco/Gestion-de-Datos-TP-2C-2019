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

namespace FrbaOfertas.AbmProveedor
{
    public partial class CrearProveedor : Form
    {
        AbmUsuario.ListadoUsuariosSinRol form_listado = null;
        AbmUsuario.SelectorRol form_rol = null;
        AbmUsuario.CrearUsuario form_usuario = null;
        Usuario usuario;
        int rol_id;

        public CrearProveedor(AbmUsuario.ListadoUsuariosSinRol form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_listado = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        public CrearProveedor(AbmUsuario.SelectorRol form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_rol = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        public CrearProveedor(AbmUsuario.CrearUsuario form_anterior, Usuario unUsuario, int unIdRol)
        {
            InitializeComponent();
            form_usuario = form_anterior;
            usuario = unUsuario;
            rol_id = unIdRol;
        }

        private void CrearProveedor_Load(object sender, EventArgs e)
        {
            cmbRubro.DataSource = RubroDAO.listarDatos();
            cmbRubro.DisplayMember = "descripcion"; //muestra solo los datos de esa columna 
            cmbRubro.ValueMember = "id";
            cmbRubro.SelectedIndex = -1; // para que no seleccione nada
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                if (ProveedorDAO.existeProveedor(txtRazonSocial.Text, txtCuit.Text))
                {
                    MessageBox.Show("Proveedor repetido. Verifique datos");
                }
                else
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.razon_social = txtRazonSocial.Text;
                    proveedor.rubro = new Rubro() { id = Int32.Parse(cmbRubro.SelectedValue.ToString()) };
                    proveedor.cuit = txtCuit.Text;
                    proveedor.mail = txtMail.Text;
                    proveedor.telefono = txtTelefono.Text;
                    proveedor.nombre_contacto = txtNombreContacto.Text;
                    proveedor.direccion = txtDireccion.Text;
                    proveedor.ciudad = txtCiudad.Text;

                    if (ProveedorDAO.agregarProveedor(proveedor, usuario.id) && UsuarioDAO.agregarRol(usuario, rol_id))
                    {
                        MessageBox.Show("Proveedor creado");
                        this.Close();
                        if (form_listado != null)
                        {
                            form_listado.form_proveedor.cargarDatos();
                            form_listado.Close();
                        }
                        else if (form_rol != null)
                        {
                            form_rol.Close();
                        }
                        else
                        {
                            form_usuario.home_proveedor.cargarDatos();
                            form_usuario.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al crear proveedor");
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
            return (txtRazonSocial.Text == "" || cmbRubro.Text == "" || txtCuit.Text == "" || txtMail.Text == "" || txtTelefono.Text == "" || txtNombreContacto.Text == "" || txtDireccion.Text == "" || txtCiudad.Text == "");
        }
    }
}
