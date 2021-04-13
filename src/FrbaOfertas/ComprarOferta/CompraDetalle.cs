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

namespace FrbaOfertas.ComprarOferta
{
    public partial class CompraDetalle : Form
    {
        ComprarOferta form_anterior;
        Compra compra;
        Cupon cupon;

        public CompraDetalle(ComprarOferta unForm, Compra unaCompra, Cupon unCupon)
        {
            InitializeComponent();
            form_anterior = unForm;
            compra = unaCompra;
            cupon = unCupon;
        }

        private void CompraDetalle_Load(object sender, EventArgs e)
        {
            txtFecha.Text = compra.fecha.Date.ToString();
            txtImporte.Text = (compra.oferta.precio_oferta * compra.cantidad).ToString();
            txtOferta.Text = compra.oferta.descripcion;
            txtCantidad.Text = compra.cantidad.ToString();
            txtCodigo.Text = cupon.codigo;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.form_anterior.cargarDatos();
            this.Close();
        }
    }
}
