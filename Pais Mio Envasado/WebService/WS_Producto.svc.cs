using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using DO;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Producto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Producto.svc or WS_Producto.svc.cs at the Solution Explorer and start debugging.
    public class WS_Producto : IWS_Producto
    {

        public DO_Producto buscarProducto(int codigoProducto)
        {
            BL_Producto blProducto = new BL_Producto();
            return blProducto.obtenerProducto(codigoProducto);
        }

        public bool ingresarProducto(DO_Producto doProducto)
        {
            BL_Producto blProducto = new BL_Producto();
            return blProducto.ingresarProducto(doProducto);
        }

        public List<DO_Producto> listaProductos()
        {
            BL_Producto blProducto = new BL_Producto();
            return blProducto.obtenerListaProductos();
        }

        public List<DO_Producto> listaProductosHabilitados()
        {
            BL_Producto blProducto = new BL_Producto();
            return blProducto.obtenerListaProductosHabilitados();
        }

        public bool modificarProducto(DO_Producto doProducto)
        {
            BL_Producto blProducto = new BL_Producto();
            return blProducto.modificarProducto(doProducto);
        }
    }
}
