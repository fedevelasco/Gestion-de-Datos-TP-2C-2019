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

namespace FrbaOfertas.EntregaConsumo
{
    public partial class EntregaConsumo : Form
    {
        public Intermedia form_intermedia { get; set; }
        public SeleccionarProveedor form_proveedores { get; set; }
        Proveedor proveedor;

        public EntregaConsumo(Intermedia form_anterior)
        {
            InitializeComponent();
            form_intermedia = form_anterior;
            proveedor = ProveedorDAO.getProveedorDesdeUsuario(form_intermedia.usuario.username);
        }

        public EntregaConsumo(SeleccionarProveedor form_anterior, Proveedor unProveedor)
        {
            InitializeComponent();
            form_proveedores = form_anterior;
            proveedor = unProveedor;
        }

        private void EntregaConsumo_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            dgvCupones.DataSource = CuponDAO.listarCupondesDeProveedor(proveedor.id);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvCupones.DataSource = CuponDAO.listarCupondesDeProveedor(proveedor.id,txtCodigo.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
        }

        private void btnDarBajaCupon_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                Cupon cupon_seleccionado = this.get_cupon_seleccionado();
                (new SeleccionarCliente(this, cupon_seleccionado)).ShowDialog();
            }
            else
            {
                MessageBox.Show("Error al dar de baja el cupón");
            }
        }

        private Cupon get_cupon_seleccionado()
        {
            Cupon cupon_seleccionado = new Cupon();
            cupon_seleccionado.id = int.Parse(dgvCupones.SelectedCells[0].Value.ToString());
            //cupon_seleccionado.canjeado = bool.Parse(dgvCupones.SelectedCells[3].Value.ToString());

            return cupon_seleccionado;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        {
            return (dgvCupones.SelectedRows.Count > 0);
        }
    }
}
