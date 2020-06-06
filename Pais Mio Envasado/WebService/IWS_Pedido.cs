using System;
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
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "agregarPedido")]
        bool agregarPedido(DO_Pedido doPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "Eliminar")]
        bool eliminarPedido(Int32 codigoPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "Modificar")]
        bool modificarEstado(Int32 codigoPedido, String estado);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "Consultar")]
        DO_Pedido consultarDetallesPedido(Int32 codigoPedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "Despachar")]
        bool despacharPedido(Int32 codigoPedido, String correoAdmin, String estado);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "AgregarAnalisisAA")]
        bool agregarAnalisisAA(DO_Analisis_AA analisisAA);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "BuscarAnalisisAA")]
        DO_Analisis_AA buscarAnalisisAA(int pedCodigo);
    }
}
