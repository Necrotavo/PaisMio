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
        public DO_ReporteInsumosComparativo obtenerReporteInsumosComparativo(DO_ReporteInsumosComparativo reporteInsumosComparativo)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reporteInsumosComparativo(reporteInsumosComparativo.inicioMes1, reporteInsumosComparativo.finalMes1,
                reporteInsumosComparativo.inicioMes2, reporteInsumosComparativo.finalMes2);
        }

        public DO_ReporteInsumos reporteEntradaInsumos(DO_ReporteEntradaInsumos reporteEntradaInsumos)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reporteInsumos(reporteEntradaInsumos.fechaInicio, reporteEntradaInsumos.fechaFinal);
        }

        public DO_ReporteInsumos reporteInsumos(DO_ReporteInsumos reporteInsumos)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reporteInsumos(reporteInsumos.fechaInicio, reporteInsumos.fechaFinal);
        }

        public DO_ReportePedido reportePedidos(DO_ReportePedido reportePedido)
        {
            BL_Reportes blReporte = new BL_Reportes();
            return blReporte.reportePedidos(reportePedido.fechaInicio, reportePedido.fechaFinal);
        }
    }
}
