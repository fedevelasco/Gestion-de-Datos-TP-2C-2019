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
    public partial class ModificarProveedor : Form
    {
        HomeProveedor form_home;
        Proveedor proveedor;

        public ModificarProveedor(HomeProveedor form_anterior, Proveedor unProveedor)
        {
            InitializeComponent();
            form_home = form_anterior;
            proveedor = unProveedor;
        }

        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            txtRazonSocial.Text = proveedor.razon_social;
            txtCuit.Text = proveedor.cuit;
            txtMail.Text = proveedor.mail;
            txtTelefono.Text = proveedor.telefono;
            txtNombreContacto.Text = proveedor.nombre_contacto;
            txtDireccion.Text = proveedor.direccion;
            txtCiudad.Text = proveedor.ciudad;

            cmbRubro.DataSource = RubroDAO.listarDatos();
            cmbRubro.DisplayMember = "descripcion"; //muestra solo los datos de esa columna 
            cmbRubro.ValueMember = "id";
            cmbRubro.Text = proveedor.rubro.descripcion;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                proveedor.razon_social = txtRazonSocial.Text;
                proveedor.rubro = new Rubro() { id = Int32.Parse(cmbRubro.SelectedValue.ToString()) };
                proveedor.cuit = txtCuit.Text;
                proveedor.mail = txtMail.Text;
                proveedor.telefono = txtTelefono.Text;
                proveedor.nombre_contacto = txtNombreContacto.Text;
                proveedor.direccion = txtDireccion.Text;
                proveedor.ciudad = txtCiudad.Text;

                if (ProveedorDAO.modificarProveedor(proveedor))
                {
                    MessageBox.Show("Proveedor modificado correctamente");
                    this.form_home.cargarDatos();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al modificar proveedor");
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
