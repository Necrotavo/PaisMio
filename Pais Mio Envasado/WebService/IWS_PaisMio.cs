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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_PaisMio" in both code and config file together.
    [ServiceContract]
    public interface IWS_PaisMio
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
        UriTemplate = "modificarDatos")]
        bool modificarDatos(DO_PaisMio datos);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        Method = "GET",
        UriTemplate = "consultarDatos")]
        DO_PaisMio consultarDatos(DO_PaisMio datos);
    }
}
