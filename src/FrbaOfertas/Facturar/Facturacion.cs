using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.Facturar
{
    public partial class Facturacion : Form
    {
        Modelo.Factura factura;
        ListadoOfertas form_anterior;

        public Facturacion(ListadoOfertas unForm, Modelo.Factura unaFactura)
        {
            InitializeComponent();
            factura = unaFactura;
            form_anterior = unForm;
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            txtNombreProveedor.Text = factura.proveedor.razon_social;
            txtImporte.Text = factura.total.ToString();
            txtNumero.Text = factura.numero.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            form_anterior.Close();
        }
    }
}
