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

        /// <summary>
        /// Retorna los datos de un reporte de insumos de un periodo
        /// </summary>
        /// <param name="inicio">Fecha de inicio del periodo</param>
        /// <param name="final">Fecha de final del periodo</param>
        /// <returns>El reporte de insumos del periodo</returns>
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

        /// <summary>
        /// Método que se enarga de listar los pedidos con sus respectivos datos que ocurrieron en un mes determinado.
        /// </summary>
        /// <param name="mes">Mes a buscar los pedidos</param>
        /// <param name="anho">Año de los pedidos a buscar</param>
        /// <returns>Lista de reporte de pedidos de ese mes</returns>
        public List<DO_ReportePedido> reportePedidos(Int32 mes, Int32 anho)
        {
            if (mes <= 0 || anho <= 0)
            {
                return null;
            }
            else {
                DAO_Reporte daoReporte = new DAO_Reporte();
                return daoReporte.reportePedidos(mes, anho);
            }
        }

        public List<DO_ReporteInsumoComparativo> reporteInsumosComparativo(String inicioMes1, String finalMes1, String inicioMes2, String finalMes2)
        {
            if (inicioMes1 is null || finalMes1 is null || inicioMes2 is null || finalMes2 is null
                || inicioMes1 == "" || finalMes1 == "" || inicioMes2 == "" || finalMes2 == "")
            {
                return null;
            }
            else
            {
                DAO_Reporte daoReporte = new DAO_Reporte();
                List<DO_ReporteInsumoComparativo> listaReporteComparativo = new List<DO_ReporteInsumoComparativo>();
                List<DO_ReporteInsumos> listaPrimerMes = daoReporte.reporteInsumos(inicioMes1, finalMes1);
                List<DO_ReporteInsumos> listaSegundoMes = daoReporte.reporteInsumos(inicioMes2, finalMes2);

                foreach (DO_ReporteInsumos insumoPrimerMes in listaPrimerMes) {
                    DO_ReporteInsumoComparativo reporteComparativo = new DO_ReporteInsumoComparativo();
                    reporteComparativo.insumoPrimerMes = insumoPrimerMes;
                    reporteComparativo.insumoSegundoMes = seRepite(insumoPrimerMes.insumo.codigo, listaSegundoMes);

                    if (!(reporteComparativo.insumoSegundoMes is null))
                    {
                        reporteComparativo.diferenciaConsumir = sacarDiferenciaPorcentual(
                            reporteComparativo.insumoPrimerMes.cantidadConsumida,
                            reporteComparativo.insumoSegundoMes.cantidadConsumida);

                        reporteComparativo.diferenciaDescarte = sacarDiferenciaPorcentual(
                            reporteComparativo.insumoPrimerMes.cantidadDescartada,
                            reporteComparativo.insumoSegundoMes.cantidadDescartada);

                        reporteComparativo.diferenciaTotal = sacarDiferenciaPorcentual(
                            reporteComparativo.insumoPrimerMes.total,
                            reporteComparativo.insumoSegundoMes.total);

                        listaSegundoMes.Remove(reporteComparativo.insumoSegundoMes);
                    }
                    else {
                        reporteComparativo.diferenciaConsumir = -100;
                        reporteComparativo.diferenciaDescarte = -100;
                        reporteComparativo.diferenciaTotal = -100;
                    }
                    listaReporteComparativo.Add(reporteComparativo);
                }

                foreach (DO_ReporteInsumos insumoSegundoMes in listaSegundoMes)
                {
                    DO_ReporteInsumoComparativo reporteComparativo = new DO_ReporteInsumoComparativo();

                    reporteComparativo.insumoSegundoMes = insumoSegundoMes;
                    reporteComparativo.diferenciaConsumir = 100;
                    reporteComparativo.diferenciaDescarte = 100;
                    reporteComparativo.diferenciaTotal = 100;

                    listaReporteComparativo.Add(reporteComparativo);
                }

                return listaReporteComparativo;
            }
        }

        private double sacarDiferenciaPorcentual(double mes1, double mes2) {
            double resta = (mes2 - mes1);
            double division = resta / mes1;
            double total = division * 100;
            return total;
        }

        private DO_ReporteInsumos seRepite(Int32 insumoPrimerMes, List<DO_ReporteInsumos> listaSegundoMes)
        {
            foreach (DO_ReporteInsumos insumoSegundoMes in listaSegundoMes)
            {
                if (insumoPrimerMes == insumoSegundoMes.insumo.codigo)
                {
                    return insumoSegundoMes;
                }
            }
            return null;
        }
    }
}
