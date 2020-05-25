using System.Collections.Generic;
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
        List<DO_SolicitudInsumos> listarInsumos();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool ingresarSolicitud(
            string operadorId,
            int codigoPedido,
            int bodega,
            List<DO_InsumoEnBodega> consumidos,
            List<DO_Insumo> descartados
            );
    }
}
