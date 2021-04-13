using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using FrbaOfertas.Modelo;
using FrbaOfertas.DAOs;

namespace FrbaOfertas.DAOs
{
    public static class CompraFacadeDAO
    {
        //Funcion que se encarga de toda la logica de la compra
        //Registrar Compra y Cupon
        //Actualizar campos en Oferta y Cliente

        public static bool agregarCompraYGenerarCupon(Compra compra, Cupon cupon)
        {
            int compra_id = CompraDAO.agregarCompra(compra);

            //Genero un codigo de cupon usando la compra_id
            cupon.codigo = "CODIGO_" + compra_id.ToString();

            Oferta oferta_actualizada = compra.oferta;
            oferta_actualizada.cantidad_disponible = oferta_actualizada.cantidad_disponible - compra.cantidad;

            Cliente cliente_actualizado = compra.cliente;
            cliente_actualizado.credito = cliente_actualizado.credito - compra.oferta.precio_oferta * compra.cantidad;

            if (compra_id != -1)
            {
                if ( CuponDAO.agregarCupon(cupon, compra_id) && OfertaDAO.actualizarCantidadDisponible(compra.oferta) && ClienteDAO.actualizarCreditos(cliente_actualizado))
                {
                    return true;
                }
                return false;
            }
            return false;         
        }
    }
}
