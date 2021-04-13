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

namespace FrbaOfertas.Facturar
{
    public partial class Facturar : Form
    {
        public Facturar()
        {
            InitializeComponent();
        }

        private void Facturar_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            dgvProveedores.DataSource = ProveedorDAO.listarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvProveedores.DataSource = ProveedorDAO.buscar_proveedores(txtRazonSocial.Text, txtCuit.Text, txtMail.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtRazonSocial.Text = "";
            txtCuit.Text = "";
            txtMail.Text = "";
        }

        private void btnBuscarOfertas_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.RowCount == 0)
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                Proveedor proveedor_seleccionado = this.get_proveedor_seleccionado();
                (new ListadoOfertas(proveedor_seleccionado,dtmPeriodoInicio.Value.Date,dtmPeriodoFin.Value.Date)).ShowDialog();
            }
        }

        private Proveedor get_proveedor_seleccionado()
        {
            Proveedor proveedor_seleccionado = new Proveedor();
            proveedor_seleccionado.id = int.Parse(dgvProveedores.SelectedCells[0].Value.ToString());
            proveedor_seleccionado.razon_social = dgvProveedores.SelectedCells[1].Value.ToString();
            proveedor_seleccionado.mail = dgvProveedores.SelectedCells[2].Value.ToString();
            proveedor_seleccionado.telefono = dgvProveedores.SelectedCells[3].Value.ToString();
            proveedor_seleccionado.direccion = dgvProveedores.SelectedCells[4].Value.ToString();
            proveedor_seleccionado.ciudad = dgvProveedores.SelectedCells[5].Value.ToString();
            proveedor_seleccionado.cuit = dgvProveedores.SelectedCells[6].Value.ToString();
            proveedor_seleccionado.rubro = RubroDAO.getRubro(dgvProveedores.SelectedCells[7].Value.ToString());
            proveedor_seleccionado.nombre_contacto = dgvProveedores.SelectedCells[8].Value.ToString();
            proveedor_seleccionado.habilitado = bool.Parse(dgvProveedores.SelectedCells[9].Value.ToString());

            return proveedor_seleccionado;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}
