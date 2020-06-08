using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using DO;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Reporte" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Reporte.svc or WS_Reporte.svc.cs at the Solution Explorer and start debugging.
    public class WS_Reporte : IWS_Reporte
    {
        public List<DO_ReporteInsumos> obtenerReporteInsumos(string fechaInicio, string fechaFinal)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.obtenerReporteInsumos(fechaInicio, fechaFinal);
        }

        public List<DO_ReporteInsumos> reporteInsumos(string fechaInicio, string fechaFinal)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reporteInsumos(fechaInicio, fechaFinal);
        }

        public List<DO_ReportePedido> reportePedidos(int mes, int anho)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reportePedidos(mes,anho);
        }
    }
}
