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
    public partial class ModificarRol : Form
    {
        HomeRol form_home;
        Rol rol_a_modificar;

        public ModificarRol(HomeRol form_anterior, Rol rol)
        {
            InitializeComponent();
            form_home = form_anterior;
            rol_a_modificar = rol;
        }

        private void ModificarRol_Load(object sender, EventArgs e)
        {
            this.cargarFuncionalidades();
        }

        private void cargarFuncionalidades()
        {
            List<Funcionalidad> funcionalidades = FuncionalidadDAO.obtener_todas_funcionalidades();
            List<Funcionalidad> funcionalidades_rol = FuncionalidadDAO.obtener_funcionalidades_de_rol(rol_a_modificar);

            foreach (Funcionalidad funcionalidad in funcionalidades)
            {

                if (funcionalidades_rol.Any(f => f.id == funcionalidad.id)) //rol_a_modificar.funcionalidades.Any(f => f.id == funcionalidad.id)
                {
                    chkFuncionalidades.Items.Add(funcionalidad, CheckState.Checked);
                }
                else
                {
                    chkFuncionalidades.Items.Add(funcionalidad, CheckState.Unchecked);
                }

                chkFuncionalidades.DisplayMember = "nombre";
                chkFuncionalidades.SelectedValue = "id";
            }

            txtNombre.Text = rol_a_modificar.nombre;

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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.datosValidos())
            {
                MessageBox.Show("Complete los campos");
            }
            else
            {
                List<Funcionalidad> funcionalidades_seleccionadas = this.obtenerFuncionalidadesSeleccionadas();

                Rol rol_modificado = new Rol();
                rol_modificado.nombre = txtNombre.Text;
                rol_modificado.funcionalidades = funcionalidades_seleccionadas;
                rol_modificado.id = rol_a_modificar.id; //le paso el id

                List<Funcionalidad> funcionalidades_rol_a_modificar = FuncionalidadDAO.obtener_funcionalidades_de_rol(rol_a_modificar);

                if (UsuarioDAO.tieneRol(form_home.form_anterior.usuario, rol_a_modificar.id) && !rol_modificado.funcionalidades.Any(f => f.nombre == "Abm Rol"))
                {
                    if (MessageBox.Show("¿Está a punto de quitar sus permisos para el ABM de Rol en el usuario que se encuentra logueado, desea continuar?", "ABM Rol", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (RolDAO.modificar_rol(rol_modificado, funcionalidades_rol_a_modificar))
                        {
                            MessageBox.Show("Rol modificado correctamente");
                            this.Close();
                            form_home.Close();
                            form_home.form_anterior.cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("Error al modificar rol ");
                        }
                    }
                }
                else
                {
                    if (RolDAO.modificar_rol(rol_modificado, funcionalidades_rol_a_modificar))
                    {
                        MessageBox.Show("Rol modificado correctamente");
                        this.Close();
                        form_home.cargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar rol ");
                    }
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool datosValidos()
        {
            return (txtNombre.Text == "" || chkFuncionalidades.CheckedItems.Count == 0);
        }
    }
}
