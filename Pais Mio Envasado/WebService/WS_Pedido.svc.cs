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
        public bool agregarAnalisisAA(DO_Analisis_AA analisisAA)
        {
            BL_Analisis_AA blAnalisisAA = new BL_Analisis_AA();
            return blAnalisisAA.agregarAnalisisAA(analisisAA);
        }

        public bool agregarPedido(DO_Pedido pedido)
        {
            BL_Pedido blPedido = new BL_Pedido();
            pedido.fechaIngreso = DateTime.Now;
            pedido.estado = "EN PROCESO";

            return blPedido.registrarPedido(pedido);

        }

        public DO_Analisis_AA buscarAnalisisAA(int pedCodigo)
        {
            BL_Analisis_AA BLanalisAA = new BL_Analisis_AA();

            return BLanalisAA.buscarAnalisisAAporPedCodigo(pedCodigo);
        }

        public DO_Pedido consultarDetallesPedido(int codigoPedido)
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.consultarDatosPedido(codigoPedido);
        }

        public bool despacharPedido(DO_Pedido doPedido)
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.despacharPedido(doPedido.codigo, doPedido.correoAdminDespacho, DateTime.Now, doPedido.estado);
        }

        public bool eliminarPedido(int codigoPedido)
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.eliminarPedido(codigoPedido);
        }

        public List<DO_Analisis_FQ> listAnalisisFQs()
        {
            BL_Analisis_AA BLanalisAA = new BL_Analisis_AA();

            return BLanalisAA.getTipoAnalisisFQs();
        }

        public List<DO_Pedido> listarPedidos()
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.listarPedidosHabilitados();
        }

        public List<DO_Pedido> listarPedidosTotales()
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.listarPedidosTotales();
        }

        public bool modificarEstado(DO_Pedido doPedido)
        {
            BL_Pedido blPedido = new BL_Pedido();

            return blPedido.modificarEstado(doPedido.codigo, doPedido.estado);
        }
    }
}
