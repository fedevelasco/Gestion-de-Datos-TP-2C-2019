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

namespace FrbaOfertas
{
    public partial class Intermedia : Form
    {
        public Inicio inicio { get; set; }
        public Usuario usuario { get; set; }

        public Intermedia(Inicio unFormAnterior, Usuario unUsuario)
        {
            InitializeComponent();
            inicio = unFormAnterior;
            usuario = unUsuario;
        }

        private void Intermedia_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        public void cargarDatos()
        {
            cmbFunClie.DataSource = UsuarioDAO.listaFuncionalidades(usuario);
            cmbFunClie.DisplayMember = "nombre"; //muestra solo los datos de esa columna 
            cmbFunClie.SelectedIndex = -1; // para que no seleccione nada
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (cmbFunClie.Text == "")
            {
                MessageBox.Show("Debe selecionar una!");
            }
            else
            {
                selector(cmbFunClie.Text);
            }
        }

        private void selector(String nombreFuncionabilidad)
        {
            switch (nombreFuncionabilidad)
            {
                case "Abm Usuario":
                    (new AbmUsuario.HomeUsuario(this)).ShowDialog();
                    break;
                case "Abm Cliente":
                    (new AbmCliente.HomeCliente()).ShowDialog();
                    break;
                case "Abm Proveedor":
                    (new AbmProveedor.HomeProveedor()).ShowDialog();
                    break;
                case "Abm Rol":
                    (new AbmRol.HomeRol(this)).ShowDialog();
                    break;
                case "Comprar Oferta":
                    if (UsuarioDAO.tieneRol(usuario, 2)) //Rol Cliente
                    {
                        if (ClienteDAO.estaHabilitado(usuario))
                        {
                            (new ComprarOferta.ComprarOferta(this)).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No puede acceder a esta funcionalidad");
                        }
                    }
                    else
                    {
                        (new ComprarOferta.SeleccionarCliente(this)).ShowDialog();
                    }
                    break;
                case "Carga Credito":
                    if (UsuarioDAO.tieneRol(usuario, 2)) //Rol Cliente
                    {
                        if (ClienteDAO.estaHabilitado(usuario))
                        {
                            (new CargaCredito.CargaCredito(this)).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No puede acceder a esta funcionalidad");
                        }
                    }
                    else
                    {
                        (new CargaCredito.SeleccionarCliente(this)).ShowDialog();
                    }
                    break;
                case "Crear Oferta":
                    if (UsuarioDAO.tieneRol(usuario, 3)) //Rol Proveedor
                    {
                        if (ProveedorDAO.estaHabilitado(usuario))
                        {
                            (new CrearOferta.CrearOferta(this)).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No puede acceder a esta funcionalidad");
                        }
                    }
                    else
                    {
                        (new CrearOferta.SeleccionarProveedor(this)).ShowDialog();
                    }
                    break;
                case "Entrega Consumo de oferta":
                    if (UsuarioDAO.tieneRol(usuario, 3)) //Rol Proveedor
                    {
                        if (ProveedorDAO.estaHabilitado(usuario))
                        {
                            (new EntregaConsumo.EntregaConsumo(this)).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No puede acceder a esta funcionalidad");
                        }
                    }
                    else
                    {
                        (new EntregaConsumo.SeleccionarProveedor(this)).ShowDialog();
                    }
                    break;
                case "Facturar":
                    (new Facturar.Facturar()).ShowDialog();
                    break;
                case "Listado Estadistico":
                    (new ListadoEstadistico.ListadoEstadistico()).ShowDialog();
                    break;
                default:
                    MessageBox.Show("Error inesperado");
                    Application.Exit();
                    break;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            inicio.Visible = true;
        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            (new AbmUsuario.ModificarUsuario(usuario)).ShowDialog();
        }
    }
}
