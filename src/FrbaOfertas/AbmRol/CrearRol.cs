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

namespace FrbaOfertas.AbmRol
{
    public partial class CrearRol : Form
    {
        HomeRol form_home;

        public CrearRol(HomeRol form_anterior)
        {
            InitializeComponent();
            form_home = form_anterior;
        }

        private void CrearRol_Load(object sender, EventArgs e)
        {
            this.cargarFuncionalidades();
        }

        private void cargarFuncionalidades()
        {
            List<Funcionalidad> funcionalidades = FuncionalidadDAO.obtener_todas_funcionalidades();
            foreach (Funcionalidad funcionalidad in funcionalidades)
            {
                chkFuncionalidades.Items.Add(funcionalidad, CheckState.Unchecked);
            }
            chkFuncionalidades.DisplayMember = "nombre";
            chkFuncionalidades.SelectedValue = "id";
        }

        private List<Funcionalidad> obtenerFuncionalidadesSeleccionadas()
        {
            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();
            foreach (Funcionalidad func in chkFuncionalidades.CheckedItems)
            {
                funcionalidades.Add(func);
            }
            return funcionalidades;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                string nombreRol = txtNombre.Text;
                List<Funcionalidad> funcionalidades = this.obtenerFuncionalidadesSeleccionadas();

                Rol rol = new Rol();
                rol.nombre = nombreRol;
                rol.funcionalidades = funcionalidades;

                if (RolDAO.validarNombreRol(nombreRol))
                {
                    if (RolDAO.agregarRol(rol))
                    {
                        MessageBox.Show("Rol agregado correctamente");
                        this.Close();
                        form_home.cargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar rol");
                    }
                }
                else
                {
                    MessageBox.Show("Nombre de rol repetido");
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        { 
            return(txtNombre.Text == "" || chkFuncionalidades.CheckedItems.Count == 0);
        }
    }
}
