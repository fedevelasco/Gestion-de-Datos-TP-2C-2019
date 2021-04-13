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
    public static class FacturacionFacadeDAO
    {
        //Funcion que se encarga de toda la logica de la compra
        //Registrar Compra y Cupon
        //Actualizar campos en Oferta y Cliente

        public static bool agregarFacturacion(Factura factura, List<Item_Factura> items)
        {
            int factura_id = FacturaDAO.agregarFactura(factura);

            if (factura_id != -1)
            {
                foreach(Item_Factura item in items)
                {
                    if (!ItemFacturaDAO.agregarItemFactura(item, factura_id))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
