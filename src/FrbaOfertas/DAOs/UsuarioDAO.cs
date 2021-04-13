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
    public static class UsuarioDAO
    {
        private static string consulta = "SELECT id,username,login_intentos,habilitado FROM JARDCOUD.Usuario";

        public static DataTable listarDatos()
        {
            string consulta = UsuarioDAO.consulta;
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable listarUsuariosSinRol()
        {
            string consulta = UsuarioDAO.consulta + " U WHERE U.id NOT IN (SELECT UR.usuario_id FROM  JARDCOUD.Usuario_Rol UR WHERE U.id = UR.usuario_id)";
            return ConexionBD.listarDatos(consulta);
        }

        public static bool tieneRolAsignado(Usuario usuario)
        {
            string consulta = "SELECT * FROM JARDCOUD.Usuario U WHERE U.id IN (SELECT UR.usuario_id FROM  JARDCOUD.Usuario_Rol UR WHERE U.id = UR.usuario_id) AND U.username = '" + usuario.username + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool tieneRolHabilitado(Usuario usuario)
        {
            string consulta = "SELECT U.id FROM JARDCOUD.Usuario U INNER JOIN JARDCOUD.Usuario_Rol UR ON U.id = UR.usuario_id INNER JOIN JARDCOUD.Rol R ON UR.rol_id = R.id WHERE R.habilitado = 1 AND U.username='" + usuario.username + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool validarUsuario(string unUsername, string unPassword)
        {
            string consulta = "SELECT id FROM JARDCOUD.Usuario U WHERE U.username='" + unUsername + "' AND U.password= HASHBYTES('SHA2_256', '" + unPassword + "')";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static Usuario getUsuario(string unUsername)
        {
            string consulta = "SELECT U.* FROM JARDCOUD.Usuario U WHERE U.username='" + unUsername + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);
            if (registro.Read())
            {
                Usuario usuario = new Usuario();

                usuario.id = registro.GetInt32(0);
                usuario.username = registro.GetString(1);
                //usuario.password = registro.GetString(2);
                usuario.login_intentos = registro.GetInt32(3);
                usuario.habilitado = registro.GetBoolean(4);

                return usuario;
            }
            else
            {
                return null;
            }
        }

        public static int obtenerLoginIntentos(string unUsername)
        {
            string consulta = "SELECT U.login_intentos FROM JARDCOUD.Usuario U WHERE U.username='" + unUsername + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);
            if (registro.Read())
            {
                return registro.GetInt32(0);//obtengo el unico dato (posicion 0)
            }
            else
            {
                return -1; //error
            }
        }

        public static void setIntentosFallidos(string unUsername, int intentos)
        {
            string consulta = "UPDATE JARDCOUD.Usuario SET login_intentos=" + intentos + " WHERE username='" + unUsername + "'";
            ConexionBD.comando(consulta);
        }

        public static void inhabilitarUsuario(string unUsername)
        {
            string consulta = "UPDATE JARDCOUD.Usuario SET habilitado=0 WHERE username='" + unUsername + "'";
            ConexionBD.comando(consulta);
        }

        public static DataTable listaFuncionalidades(Usuario usuario)
        {
            string consulta = "SELECT DISTINCT F.nombre " +
            "FROM JARDCOUD.Usuario U " +
            "INNER JOIN JARDCOUD.Usuario_Rol UR ON U.id = UR.usuario_id " +
            "INNER JOIN JARDCOUD.Rol R ON UR.rol_id = R.id " +
            "INNER JOIN JARDCOUD.Rol_Funcionalidad RF ON R.id = RF.rol_id " +
            "INNER JOIN JARDCOUD.Funcionalidad F ON RF.funcionalidad_id = F.id " +
            "WHERE U.username = '" + usuario.username + "' AND R.habilitado = 1";

            return ConexionBD.listarDatos(consulta);
        }

        public static bool tieneRol(Usuario usuario, int rol_id)
        {
            string consulta = "SELECT UR.rol_id FROM JARDCOUD.Usuario U INNER JOIN JARDCOUD.Usuario_Rol UR ON U.id = UR.usuario_id WHERE U.username = '"+ usuario.username +"' AND UR.rol_id = " + rol_id.ToString();
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool existeUsername(string unUsername)
        {
            string consulta = "SELECT U.id FROM JARDCOUD.Usuario U WHERE U.username='" + unUsername + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool existeUsernameHabilitado(string unUsername)
        {
            string consulta = "SELECT U.id FROM JARDCOUD.Usuario U WHERE U.username='" + unUsername + "' AND U.habilitado = 1";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool agregarUsuario(string unUsername, string unPassword)
        {
            try
            {
                string consulta = "INSERT INTO JARDCOUD.Usuario VALUES ('"+ unUsername+"', HASHBYTES('SHA2_256','"+unPassword+"'), 0, 1)";
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Usuario");
                return false;
            }
        }

        public static bool darDeBajaUsuario(Usuario usuario)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Usuario SET habilitado = @usuario_hab WHERE id=@usuario_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@usuario_hab", Convert.ToInt32(!usuario.habilitado));
                cmd.Parameters.AddWithValue("@usuario_id", usuario.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }    
        }

        public static bool modificarUsuario(Usuario usuario,string unPassword)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Usuario SET password = HASHBYTES('SHA2_256', '"+unPassword+"') WHERE id="+usuario.id);
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable buscar_usuarios(string username, string habilitado)
        {
            string consulta_username = null, consulta_habilitado = null, consulta_where = null, consulta;

            if (!string.IsNullOrEmpty(username))
            {
                consulta_username = "username LIKE '%" + username + "%'";
                consulta_where = " WHERE ";
            }
            if (!string.IsNullOrEmpty(habilitado))
            {
                consulta_habilitado = "habilitado = '" + habilitado + "'";
                if (!string.IsNullOrEmpty(username))
                {
                    consulta_habilitado = "AND " + consulta_habilitado;
                }
                consulta_where = " WHERE ";
            }

            consulta = string.Format(UsuarioDAO.consulta + consulta_where + consulta_username + consulta_habilitado);
            //MessageBox.Show(consulta);
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable buscar_usuarios_sin_rol(string username)
        {
            string consulta_username = null, consulta_where = null, consulta;

            if (!string.IsNullOrEmpty(username))
            {
                consulta_username = "username LIKE '%" + username + "%'";
                consulta_where = " AND ";
            }

            consulta = string.Format(UsuarioDAO.consulta + " U WHERE U.id NOT IN (SELECT UR.usuario_id FROM  JARDCOUD.Usuario_Rol UR WHERE U.id = UR.usuario_id)" + consulta_where + consulta_username);
            //MessageBox.Show(consulta);
            return ConexionBD.listarDatos(consulta);
        }

        public static bool agregarRol(Usuario usuario, int rol_id)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Usuario_Rol VALUES (@user_id, @rol_id)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@user_id", usuario.id);
                cmd.Parameters.AddWithValue("@rol_id", rol_id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Rol");
                return false;
            }
        }
    }
}
