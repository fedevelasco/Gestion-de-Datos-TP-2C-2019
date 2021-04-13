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
    public partial class ListadoOfertas : Form
    {
        Proveedor proveedor;
        DateTime inicio;
        DateTime fin;

        public ListadoOfertas(Proveedor unProveedor,DateTime periodo_inicio,DateTime periodo_fin)
        {
            InitializeComponent();
            proveedor = unProveedor;
            inicio = new DateTime(periodo_inicio.Year,periodo_inicio.Month,periodo_inicio.Day, 0, 0, 0);
            fin = new DateTime(periodo_fin.Year, periodo_fin.Month, periodo_fin.Day, 23, 59, 59);
        }

        private void ListadoOfertas_Load(object sender, EventArgs e)
        {
            dgvOfertas.DataSource = OfertaDAO.listarOfertasDeProveedor(proveedor.id,inicio,fin);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (dgvOfertas.RowCount != 0)
            {
                double total_facturacion = 0;
                List<Item_Factura> items = new List<Item_Factura> { };

                foreach (DataGridViewRow row in dgvOfertas.Rows)
                {
                    Item_Factura item = new Item_Factura();
                    item.oferta = new Oferta() { id = Int32.Parse(row.Cells[0].Value.ToString())};
                    item.importe = Double.Parse(row.Cells[5].Value.ToString());
                    item.cantidad = Int32.Parse(row.Cells[4].Value.ToString());
                    total_facturacion += Double.Parse(row.Cells[5].Value.ToString());

                    items.Add(item);
                }

                Factura factura = new Factura();
                factura.total = total_facturacion;
                factura.numero = this.obtenerNumeroFacturacionAleatorio();
                factura.proveedor = proveedor;
                factura.fecha = FechaAplicacion.get();

                if (FacturacionFacadeDAO.agregarFacturacion(factura,items))
                {
                    (new Facturacion(this, factura)).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error al agragar Facturación");
                }
            }
            else
            {
                MessageBox.Show("No se encontraron ofertas");
                this.Close();
            }
        }

        private int obtenerNumeroFacturacionAleatorio()
        {
            int numeroAleatorio = new Random().Next(150000, 160000);
            while (FacturaDAO.existeNumeroFactura(numeroAleatorio)) //mientras exista, genera otro
            {
                numeroAleatorio = new Random().Next(150000, 160000);
            }
            return numeroAleatorio;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
