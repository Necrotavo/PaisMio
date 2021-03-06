﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using DO;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Pedido" in both code and config file together.
    [ServiceContract]
    public interface IWS_Pedido
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", UriTemplate = "agregarPedido")]
        bool agregarPedido(DO_Pedido doPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "Eliminar")]
        bool eliminarPedido(int codigo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "Modificar")]
        bool modificarEstado(DO_Pedido doPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "Consultar")]
        DO_Pedido consultarDetallesPedido(int codigo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "Despachar")]
        bool despacharPedido(DO_Pedido doPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "AgregarAnalisisAA")]
        bool agregarAnalisisAA(DO_Analisis_AA analisisAA);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "BuscarAnalisisAA")]
        DO_Analisis_AA buscarAnalisisAA(int pedCodigo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", UriTemplate = "listarPedidos")]
        List <DO_Pedido> listarPedidos();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", UriTemplate = "listarPedidosTotales")]
        List<DO_Pedido> listarPedidosTotales();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, Method = "GET", UriTemplate = "AnalisisFQs")]
        List<DO_Analisis_FQ> listaAnalisisFQs();
    }
}
