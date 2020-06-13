using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Bodega" in both code and config file together.
    [ServiceContract]
    public interface IWS_Bodega
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "entradaInsumos")]
        bool entradaInsumos(DO_EntradaInsumosBodega bodega);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "obtenerBodega")]
        DO_Bodega obtenerBodega(int codigo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "registrarBodega")]
        bool registrarBodega(DO_Bodega doBodega);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "modificarBodega")]
        bool modificarBodega(DO_Bodega doBodega);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "cambiarEstadoBodega")]
        bool cambiarEstadoBodega(int codigoBodega, String estado);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<DO_Bodega> obtenerListaBodegas();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<DO_Bodega> obtenerListaBodegasHabilitados();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "moverInsumoDeBodega")]
        bool moverInsumoDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega, Int32 codigoInsumo, Int32 cantidad);

        //[OperationContract]
        //[WebInvoke(RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    Method = "POST",
        //    UriTemplate = "moverTodosInsumosDeBodega")]
        //bool moverTodosInsumosDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "obtenerInsumosBodega")]
        List<DO_InsumoEnBodega> obtenerInsumosBodega(Int32 codigoBodega);
    }
}
