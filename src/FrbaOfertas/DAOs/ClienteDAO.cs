using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrbaOfertas.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace FrbaOfertas.DAOs
{
    public static class ClienteDAO
    {
        private static string consulta = "SELECT id,nombre,apellido,dni,mail,telefono,direccion,codigo_postal,fecha_nacimiento,credito,habilitado FROM JARDCOUD.Cliente";

        public static bool existeCliente(string unDni)
        {
            string consulta = "SELECT * FROM JARDCOUD.Cliente C WHERE C.dni = '" + unDni + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool agregarCliente(Cliente cliente, int usuario_id)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Cliente VALUES (@name, @lastname, @dni, @mail, @phone, @address, @cp, @nacimiento, 200, @user_id, 1)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@name", cliente.nombre);
                cmd.Parameters.AddWithValue("@lastname", cliente.apellido);
                cmd.Parameters.AddWithValue("@dni", cliente.dni);
                cmd.Parameters.AddWithValue("@mail", cliente.mail);
                cmd.Parameters.AddWithValue("@phone", cliente.telefono);
                cmd.Parameters.AddWithValue("@nacimiento", cliente.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@address", cliente.direccion);
                cmd.Parameters.AddWithValue("@cp", cliente.codigo_postal);
                cmd.Parameters.AddWithValue("@user_id", usuario_id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Cliente");
                return false;
            }
        }

        public static bool modificarCliente(Cliente cliente)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Cliente SET nombre = @name, apellido = @lastname, dni = @dni, mail = @mail, telefono = @phone, codigo_postal = @cp, fecha_nacimiento = @nacimiento WHERE id=@cliente_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@name", cliente.nombre);
                cmd.Parameters.AddWithValue("@lastname", cliente.apellido);
                cmd.Parameters.AddWithValue("@dni", cliente.dni);
                cmd.Parameters.AddWithValue("@mail", cliente.mail);
                cmd.Parameters.AddWithValue("@phone", cliente.telefono);
                cmd.Parameters.AddWithValue("@nacimiento", cliente.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@address", cliente.direccion);
                cmd.Parameters.AddWithValue("@cp", cliente.codigo_postal);
                cmd.Parameters.AddWithValue("@cliente_id", cliente.id);



                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable listarDatos()
        {
            string consulta = ClienteDAO.consulta;
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable buscar_clientes(string nombre, string apellido, string dni, string mail)
        {
            string consulta_nombre = null, consulta_apellido = null, consulta_dni = null, consulta_mail = null, consulta_where = null, consulta;

            if (!string.IsNullOrEmpty(nombre))
            {
                consulta_nombre = "nombre LIKE '%" + nombre + "%'";
                consulta_where = " WHERE ";
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                consulta_apellido = "apellido LIKE '%" + apellido + "%'";
                if (!string.IsNullOrEmpty(nombre))
                {
                    consulta_apellido = "AND " + consulta_apellido;
                }
                consulta_where = " WHERE ";
            }

            if (!string.IsNullOrEmpty(dni))
            {
                consulta_dni = "dni = '" + dni + "'";
                if (!string.IsNullOrEmpty(nombre) || (!string.IsNullOrEmpty(apellido)))
                {
                    consulta_dni = "AND " + consulta_dni;
                }
                consulta_where = " WHERE ";
            }

            if (!string.IsNullOrEmpty(mail))
            {
                consulta_mail = "mail = '" + mail + "'";
                if (!string.IsNullOrEmpty(nombre) || (!string.IsNullOrEmpty(apellido)) || (!string.IsNullOrEmpty(dni)))
                {
                    consulta_dni = "AND " + consulta_dni;
                }
                consulta_where = " WHERE ";
            }

            consulta = string.Format(ClienteDAO.consulta + consulta_where + consulta_nombre + consulta_apellido + consulta_dni + consulta_mail);
            //MessageBox.Show(consulta);
            return ConexionBD.listarDatos(consulta);
        }

        public static bool deshabilitarCliente(Cliente cliente)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Cliente SET habilitado = @cliente_hab WHERE id=@cliente_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@cliente_hab", Convert.ToInt32(!cliente.habilitado));
                cmd.Parameters.AddWithValue("@cliente_id", cliente.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static bool estaHabilitado(Usuario usuario)
        {
            string consulta = "SELECT * FROM JARDCOUD.Cliente C INNER JOIN JARDCOUD.Usuario U ON C.usuario_id = U.id WHERE C.habilitado = 1 AND U.id = " + usuario.id;
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static Cliente getClienteDesdeUsuario(string unUsername)
        {
            string consulta = "SELECT C.* FROM JARDCOUD.Cliente C INNER JOIN JARDCOUD.Usuario U ON C.usuario_id = U.id WHERE U.username='" + unUsername + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);
            if (registro.Read())
            {
                Cliente cliente = new Cliente();

                cliente.id = registro.GetInt32(0);
                cliente.nombre = registro.GetString(1);
                cliente.apellido = registro.GetString(2);
                cliente.dni = registro.GetDecimal(3).ToString();
                cliente.mail = registro.GetString(4);
                cliente.telefono = registro.GetDecimal(5).ToString();
                cliente.direccion = registro.GetString(6);
                cliente.codigo_postal = int.Parse(registro.GetString(7));
                cliente.fecha_nacimiento = registro.GetDateTime(8);
                cliente.credito = double.Parse(registro.GetDecimal(9).ToString());
                cliente.habilitado = registro.GetBoolean(11);

                return cliente;
            }
            else
            {
                return null;
            }
        }

        public static bool actualizarCreditos(Cliente cliente)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Cliente SET credito = @creditos WHERE id=@cliente_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@creditos", cliente.credito);
                cmd.Parameters.AddWithValue("@cliente_id", cliente.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
