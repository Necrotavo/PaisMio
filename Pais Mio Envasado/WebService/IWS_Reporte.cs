using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DO;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Reporte" in both code and config file together.
    [ServiceContract]
    public interface IWS_Reporte
    {

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "reporteInsumos")]
        DO_ReporteInsumos reporteInsumos(DO_ReporteInsumos reporteInsumos);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "reportePedidos")]
        DO_ReportePedido reportePedidos(DO_ReportePedido reportePedido);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "reporteInsumosComparativo")]
        DO_ReporteInsumosComparativo obtenerReporteInsumosComparativo(DO_ReporteInsumosComparativo reporteInsumosComparativo);
    }
}
