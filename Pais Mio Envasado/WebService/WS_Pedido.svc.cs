using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Pedido" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Pedido.svc or WS_Pedido.svc.cs at the Solution Explorer and start debugging.
    public class WS_Pedido : IWS_Pedido
    {
        public bool agregarPedido(DO_Pedido pedido)
        {
            BL_Pedido blPedido = new BL_Pedido();
            pedido.fechaIngreso = DateTime.Now;
            pedido.estado = "EN PROCESO";

            return blPedido.registrarPedido(pedido);

        }

        

        public bool eliminarPedido(int codigoPedido)
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.eliminarPedido(codigoPedido);
        }
    }
}
