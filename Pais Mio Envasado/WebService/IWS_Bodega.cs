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
        bool entradaInsumos(DO_Bodega doBodega, string correoAdministrador);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST",
            UriTemplate = "obtenerBodega")]
        DO_Bodega obtenerBodega(int codigoBodega);
    }
}
