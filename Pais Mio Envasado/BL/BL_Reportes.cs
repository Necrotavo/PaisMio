using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DO;

namespace BL
{
    public class BL_Reportes
    {
        //public List<DO_ReporteInsumos> obtenerReporteInsumos(String inicio, String final) {
        //    DAO_Reporte daoReporte = new DAO_Reporte();
        //    List<DO_InsumoEnBodega> listaInsumosConsumidos = daoReporte.obtenerListaInsumosConsumidos(inicio, final);
        //    List<DO_InsumoEnBodega> listaInsumosDescartados = daoReporte.obtenerListaInsumosDescartados(inicio, final);
        //    List<DO_ReporteInsumos> insumosReportados = new List<DO_ReporteInsumos>();

        //    foreach (DO_InsumoEnBodega insumoEnBodega in listaInsumosConsumidos)
        //    {
        //        DO_ReporteInsumos doIsumoReportable = new DO_ReporteInsumos();

        //        doIsumoReportable.insumo = insumoEnBodega.insumo;
        //        doIsumoReportable.cantidadConsumida = insumoEnBodega.cantidadDisponible;

        //        insumosReportados.Add(doIsumoReportable);
        //    }

        //    foreach (DO_InsumoEnBodega insumoEnBodega in listaInsumosDescartados)
        //    {
        //        Int32 indiceEncontrado = existeInsumoReportado(insumoEnBodega.insumo.codigo, insumosReportados);
        //        if (indiceEncontrado != -1)

        //        {
        //            insumosReportados.ElementAt(indiceEncontrado).cantidadDescartada = insumoEnBodega.cantidadDisponible;
        //        }
        //        else {
        //            DO_ReporteInsumos doIsumoReportable = new DO_ReporteInsumos();

        //            doIsumoReportable.insumo = insumoEnBodega.insumo;
        //            doIsumoReportable.cantidadDescartada = insumoEnBodega.cantidadDisponible;

        //            insumosReportados.Add(doIsumoReportable);
        //        }
        //    }

        //    return insumosReportados;
        //}

        //private Int32 existeInsumoReportado(Int32 codigoInsumo, List<DO_ReporteInsumos> insumosReportados) {
        //    for (int index = 0; index < insumosReportados.Count(); index++)
        //    {
        //        if (insumosReportados.ElementAt(index).insumo.codigo == codigoInsumo)
        //        {
        //            return index;
        //        }
        //    }
        //    return -1;
        //}

        public List<DO_ReporteInsumos> reporteInsumos(String inicio, String final) {
            if (inicio is null || final is null
                || inicio == "" || final == "")
            {
                return null;
            }
            else {
                DAO_Reporte daoReporte = new DAO_Reporte();
                return daoReporte.reporteInsumos(inicio, final);
            }
        }
    }
}
