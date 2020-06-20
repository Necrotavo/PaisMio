using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using DO;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_SolicitudInsumo" in both code and config file together.
    [ServiceContract]
    public interface IWS_SolicitudInsumo
    {
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<DO_SolicitudInsumos> listarSolicitudes();//tested

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "solicitudPorPedido")]
        List<DO_SolicitudInsumos> listarSolicitudesPorPedido(DO_Pedido pedido);//tested

        /*
         {
	"pedido":1
         }
         */

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "solicitudPorOperario")]
        List<DO_SolicitudInsumos> listarSolicitudesPorOperario(string operario);//tested
        /*
         {
        "operario":"jm_rc@yahoo.es"
         }
        */

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "POST", 
            UriTemplate = "ingresoSolicitud")]
        bool ingresarSolicitud(DO_SolicitudInsumos solicitud);//tested


        [OperationContract]
        [WebInvoke(
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           UriTemplate = "solicitudSingular")]
        DO_SolicitudInsumos solicitarSolicitud(int idSolicitud);//tested

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "decisionAdmin")]
        bool decision(DO_DecisionSolicitudInsumos decision);//tested
    }
}
