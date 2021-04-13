using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using FrbaOfertas.Modelo;

namespace FrbaOfertas.DAOs
{
    public static class RolDAO
    {
        public static DataTable listarDatos()
        {
            string consulta = "SELECT * FROM JARDCOUD.Rol";
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable listarRolesUsuario()
        {
            string consulta = "SELECT * FROM JARDCOUD.Rol WHERE nombre != 'Administrativo' AND habilitado = 1";
            return ConexionBD.listarDatos(consulta);
        }

        public static bool validarNombreRol(string nombreRol)
        {
            string consulta = string.Format(@"SELECT R.id FROM JARDCOUD.Rol R WHERE R.nombre=@nombre");
            SqlConnection conn = ConexionBD.getConexion();
            SqlCommand cmd = new SqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@nombre", nombreRol);
            bool rta = cmd.ExecuteScalar() == null;
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return rta;
        }

        public static bool agregarRol(Rol rol)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Rol VALUES (@rol_nombre,1); SELECT SCOPE_IDENTITY()");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@rol_nombre", rol.nombre);

                int rol_cod_generado = Convert.ToInt32(cmd.ExecuteScalar());
                foreach (Funcionalidad funcionalidad in rol.funcionalidades)
                {
                    cmd = new SqlCommand("INSERT INTO JARDCOUD.Rol_Funcionalidad VALUES (@rol_id, @func_id)", conn);

                    cmd.Parameters.AddWithValue("@rol_id", rol_cod_generado);
                    cmd.Parameters.AddWithValue("@func_id", funcionalidad.id);

                    cmd.ExecuteNonQuery();
                }
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Rol");
            }
            return false;
        }

        public static bool modificar_rol(Rol rol, List<Funcionalidad> funcionalidades_anteriores)
        {
            try
            {
                List<Funcionalidad> funcionalidades_rol = rol.funcionalidades;
                List<Funcionalidad> funcionalidades_nuevas = new List<Funcionalidad> { };
                List<Funcionalidad> funcionalidades_quitadas = new List<Funcionalidad> { };

                foreach (Funcionalidad funcionalidad in funcionalidades_rol)
                {
                    if (!funcionalidades_anteriores.Any(f => f.id == funcionalidad.id))
                    {
                        funcionalidades_nuevas.Add(funcionalidad);
                    }
                }

                foreach (Funcionalidad funcionalidad in funcionalidades_anteriores)
                {
                    if (!funcionalidades_rol.Any(f => f.id == funcionalidad.id))
                    {
                        funcionalidades_quitadas.Add(funcionalidad);
                    }
                }

                string consulta = string.Format(@"UPDATE JARDCOUD.Rol SET nombre=@rol_nombre WHERE id=@rol_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@rol_nombre", rol.nombre);
                cmd.Parameters.AddWithValue("@rol_id", rol.id);

                cmd.ExecuteNonQuery();

                //Inserto funcionalidades nuevas
                foreach (Funcionalidad funcionalidad in funcionalidades_nuevas)
                {
                    cmd = new SqlCommand("INSERT INTO JARDCOUD.Rol_Funcionalidad VALUES (@rol_id, @func_id)", conn);
                    cmd.Parameters.AddWithValue("@rol_id", rol.id);
                    cmd.Parameters.AddWithValue("@func_id", funcionalidad.id);

                    cmd.ExecuteNonQuery();
                }
                //Borro funcionalidades quitadas
                foreach (Funcionalidad funcionalidad in funcionalidades_quitadas)
                {
                    cmd = new SqlCommand("DELETE FROM JARDCOUD.Rol_Funcionalidad WHERE funcionalidad_id=@func_id AND rol_id=@rol_id", conn);
                    cmd.Parameters.AddWithValue("@func_id", funcionalidad.id);
                    cmd.Parameters.AddWithValue("@rol_id", rol.id);

                    cmd.ExecuteNonQuery();
                }


                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Rol", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static bool borrar_rol(Rol rol)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Rol SET habilitado = @rol_hab WHERE id=@rol_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@rol_hab", Convert.ToInt32(!rol.habilitado));
                cmd.Parameters.AddWithValue("@rol_id", rol.id);

                cmd.ExecuteNonQuery();

                if (rol.habilitado) // si el rol estaba habilitado
                {
                    cmd = new SqlCommand("DELETE FROM JARDCOUD.Usuario_Rol WHERE rol_id=@rol_id", conn);
                    cmd.Parameters.AddWithValue("@rol_id", rol.id);

                    cmd.ExecuteNonQuery();
                }

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al borrar Rol", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
