using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaOfertas.DAOs;

namespace FrbaOfertas.ListadoEstadistico
{
    public partial class ListadoEstadistico : Form
    {
        public ListadoEstadistico()
        {
            InitializeComponent();
        }

        private void ListadoEstadistico_Load(object sender, EventArgs e)
        {
            cmbTipoListado.Items.Insert(0, "TOP 5 Proveedores con mayor porcentaje de descuento ofrecido en sus ofertas");
            cmbTipoListado.Items.Insert(1, "TOP 5 Proveedores con mayor facturación");

            cmbSemestre.Items.Insert(0, 1);
            cmbSemestre.Items.Insert(1, 2);

            numAnio.Value = 2000;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                int anio = Convert.ToInt32(numAnio.Value.ToString());
                int semestre = cmbSemestre.SelectedIndex + 1;
                int consulta_seleccionada = cmbTipoListado.SelectedIndex;

                dgvListado.DataSource = ListadoEstadisticoFacadeDAO.listarDatos(consulta_seleccionada,anio,semestre);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbTipoListado.SelectedIndex = -1;
            cmbSemestre.SelectedIndex = -1;
            numAnio.Value = 2000;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        { 
            return( cmbSemestre.Text == "" || cmbTipoListado.Text == "");
        }
    }
}
