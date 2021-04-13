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
    public static class ProveedorDAO
    {
        public static string consulta = "SELECT P.id,P.razon_social,P.mail,P.telefono,P.direccion,P.ciudad,P.cuit,R.descripcion,P.nombre_contacto,P.habilitado FROM JARDCOUD.Proveedor P INNER JOIN JARDCOUD.Rubro R ON P.rubro_id = R.id";

        public static bool existeProveedor(string unaRazonSocial, string unCuit)
        {
            string consulta = "SELECT * FROM JARDCOUD.Proveedor P WHERE P.razon_social = '" + unaRazonSocial + "' OR P.cuit = '" + unCuit + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static bool agregarProveedor(Proveedor proveedor, int usuario_id)
        {
            try
            {
                string consulta = string.Format(@"INSERT INTO JARDCOUD.Proveedor VALUES (@RS, @mail, @phone, @address, @city, @cuit, @rubro, @name, @user_id, 1)");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@RS", proveedor.razon_social);
                cmd.Parameters.AddWithValue("@mail", proveedor.mail);
                cmd.Parameters.AddWithValue("@phone", proveedor.telefono);
                cmd.Parameters.AddWithValue("@city", proveedor.ciudad);
                cmd.Parameters.AddWithValue("@address", proveedor.direccion);
                cmd.Parameters.AddWithValue("@cuit", proveedor.cuit);
                cmd.Parameters.AddWithValue("@name", proveedor.nombre_contacto);
                cmd.Parameters.AddWithValue("@rubro", proveedor.rubro.id);
                cmd.Parameters.AddWithValue("@user_id", usuario_id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Proveedor");
                return false;
            }
        }

        public static bool modificarProveedor(Proveedor proveedor)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Proveedor SET razon_social = @RS, mail = @mail, telefono = @phone, ciudad = @city, direccion = @address, cuit = @cuit, nombre_contacto = @name, rubro_id = @rubro  WHERE id=@prov_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@RS", proveedor.razon_social);
                cmd.Parameters.AddWithValue("@mail", proveedor.mail);
                cmd.Parameters.AddWithValue("@phone", proveedor.telefono);
                cmd.Parameters.AddWithValue("@city", proveedor.ciudad);
                cmd.Parameters.AddWithValue("@address", proveedor.direccion);
                cmd.Parameters.AddWithValue("@cuit", proveedor.cuit);
                cmd.Parameters.AddWithValue("@name", proveedor.nombre_contacto);
                cmd.Parameters.AddWithValue("@rubro", proveedor.rubro.id);
                cmd.Parameters.AddWithValue("@prov_id", proveedor.id);



                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable listarDatos()
        {
            string consulta = ProveedorDAO.consulta;
            return ConexionBD.listarDatos(consulta);
        }

        public static DataTable buscar_proveedores(string razon_social, string cuit, string mail)
        {
            string consulta_razon_social = null, consulta_cuit = null, consulta_mail = null, consulta_where = null, consulta;

            if (!string.IsNullOrEmpty(razon_social))
            {
                consulta_razon_social = "P.razon_social LIKE '%" + razon_social + "%'";
                consulta_where = " WHERE ";
            }
            if (!string.IsNullOrEmpty(cuit))
            {
                consulta_cuit = "p.cuit = '" + cuit + "'";
                if (!string.IsNullOrEmpty(razon_social))
                {
                    consulta_cuit = "AND " + consulta_cuit;
                }
                consulta_where = " WHERE ";
            }

            if (!string.IsNullOrEmpty(mail))
            {
                consulta_mail = "P.mail = '" + mail + "'";
                if (!string.IsNullOrEmpty(razon_social) || (!string.IsNullOrEmpty(cuit)))
                {
                    consulta_mail = "AND " + consulta_mail;
                }
                consulta_where = " WHERE ";
            }

            consulta = string.Format(ProveedorDAO.consulta + consulta_where + consulta_razon_social + consulta_cuit + consulta_mail);
            //MessageBox.Show(consulta);
            return ConexionBD.listarDatos(consulta);
        }

        public static bool deshabilitarProveedor(Proveedor proveedor)
        {
            try
            {
                string consulta = string.Format(@"UPDATE JARDCOUD.Proveedor SET habilitado = @proveedor_hab WHERE id=@proveedor_id");
                SqlConnection conn = ConexionBD.getConexion();
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@proveedor_hab", Convert.ToInt32(!proveedor.habilitado));
                cmd.Parameters.AddWithValue("@proveedor_id", proveedor.id);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static bool estaHabilitado(Usuario usuario)
        {
            string consulta = "SELECT * FROM JARDCOUD.Proveedor P INNER JOIN JARDCOUD.Usuario U ON P.usuario_id = U.id WHERE P.habilitado = 1 AND U.id = " + usuario.id;
            SqlDataReader registro = ConexionBD.comando(consulta);

            return registro.Read();
        }

        public static Proveedor getProveedorDesdeUsuario(string unUsername)
        {
            string consulta = "SELECT P.* FROM JARDCOUD.Proveedor P INNER JOIN JARDCOUD.Usuario U ON P.usuario_id = U.id WHERE U.username='" + unUsername + "'";
            SqlDataReader registro = ConexionBD.comando(consulta);
            if (registro.Read())
            {
                Proveedor proveedor = new Proveedor();

                proveedor.id = registro.GetInt32(0);
                proveedor.razon_social = registro.GetString(1);
                proveedor.mail = registro.GetString(2);
                proveedor.telefono = registro.GetDecimal(3).ToString();
                proveedor.direccion = registro.GetString(4);
                proveedor.ciudad = registro.GetString(5);
                proveedor.cuit = registro.GetString(6);
                proveedor.nombre_contacto = registro.GetString(8);
                proveedor.habilitado = registro.GetBoolean(10);

                return proveedor;
            }
            else
            {
                return null;
            }
        }

    }
}
