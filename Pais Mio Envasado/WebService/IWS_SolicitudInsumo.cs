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
        List<DO_SolicitudInsumos> listarSolicitudes();

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "POST",
            UriTemplate = "solicitudPorPedido")]
        List<DO_SolicitudInsumos> listarSolicitudesPorPedido(int pedido);
        /*
         {
	"pedido":1
         }
         */

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "POST",
            UriTemplate = "solicitudPorOperario")]
        List<DO_SolicitudInsumos> listarSolicitudesPorOperario(string operario);
        /*
         {
        "operario":"jm_rc@yahoo.es"
         }
        */

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            Method = "POST", 
            UriTemplate = "ingresoSolicitud")]
        bool ingresarSolicitud(DO_SolicitudInsumos solicitud);


        [OperationContract]
        [WebInvoke(
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           Method = "POST",
           UriTemplate = "solicitudSingular")]
        DO_SolicitudInsumos solicitarSolicitud(int idSolicitud);

        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "POST",
            UriTemplate = "decisionAdmin")]
        bool decision(DO_SolicitudInsumos solicitud, DO_Administrador admin, string estado);
    }
}
