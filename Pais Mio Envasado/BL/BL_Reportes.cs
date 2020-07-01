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
        public DO_ReporteInsumos reporteInsumos(String inicio, String final) {
            if (inicio is null || final is null
                || inicio == "" || final == "")
            {
                return null;
            }
            else {
                DAO_Reporte daoReporte = new DAO_Reporte();
                DO_ReporteInsumos doReporteInsumos = new DO_ReporteInsumos();
                doReporteInsumos.listaInsumos = daoReporte.reporteInsumos(inicio, final);

                DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
                doReporteInsumos.infoPaisMio = daoPaisMio.obtenerDatos();
                doReporteInsumos.fechaInicio = inicio;
                doReporteInsumos.fechaFinal = final;

                return doReporteInsumos;
            }
        }

        /// <summary>
        /// Método que se encarga de listar los pedidos con sus respectivos datos que ocurrieron en un mes determinado.
        /// </summary>
        /// <param name="mes">Mes a buscar los pedidos</param>
        /// <param name="anho">Año de los pedidos a buscar</param>
        /// <returns>Lista de reporte de pedidos de ese mes</returns>
        public DO_ReportePedido reportePedidos(String inicio, String final)
        {
            if (inicio is null || final is null
                || inicio == "" || final == "")
            {
                return null;
            }
            else {
                DAO_Reporte daoReporte = new DAO_Reporte();
                DO_ReportePedido reporte = daoReporte.reportePedidos(inicio, final);
                reporte.fechaInicio = inicio;
                reporte.fechaFinal = final;
                return reporte;
            }
        }

        public DO_ReporteInsumosComparativo reporteInsumosComparativo(String inicioMes1, String finalMes1, String inicioMes2, String finalMes2)
        {
            if (inicioMes1 is null || finalMes1 is null || inicioMes2 is null || finalMes2 is null
                || inicioMes1 == "" || finalMes1 == "" || inicioMes2 == "" || finalMes2 == "")
            {
                return null;
            }
            else
            {
                DAO_Reporte daoReporte = new DAO_Reporte();
                DO_ReporteInsumosComparativo reporteInsComparativo = new DO_ReporteInsumosComparativo();
                reporteInsComparativo.listaInsumos = new List<DO_InsumosComparados>();
                List<DO_InsumoReportable> listaPrimerMes = daoReporte.reporteInsumos(inicioMes1, finalMes1);
                List<DO_InsumoReportable> listaSegundoMes = daoReporte.reporteInsumos(inicioMes2, finalMes2);

                foreach (DO_InsumoReportable insumoPrimerMes in listaPrimerMes) {
                    DO_InsumosComparados insumoComparado = new DO_InsumosComparados();
                    insumoComparado.insumoPrimerMes = insumoPrimerMes;
                    insumoComparado.insumoSegundoMes = seRepite(insumoPrimerMes.insumo.codigo, listaSegundoMes);

                    if (!(insumoComparado.insumoSegundoMes is null))
                    {
                        insumoComparado.diferenciaConsumir = sacarDiferenciaPorcentual(
                            insumoComparado.insumoPrimerMes.cantidadConsumida,
                            insumoComparado.insumoSegundoMes.cantidadConsumida);

                        insumoComparado.diferenciaDescarte = sacarDiferenciaPorcentual(
                            insumoComparado.insumoPrimerMes.cantidadDescartada,
                            insumoComparado.insumoSegundoMes.cantidadDescartada);

                        insumoComparado.diferenciaTotal = sacarDiferenciaPorcentual(
                            insumoComparado.insumoPrimerMes.total,
                            insumoComparado.insumoSegundoMes.total);

                        listaSegundoMes.Remove(insumoComparado.insumoSegundoMes);
                    }
                    else {
                        insumoComparado.diferenciaConsumir = -100;
                        insumoComparado.diferenciaDescarte = -100;
                        insumoComparado.diferenciaTotal = -100;
                    }
                    reporteInsComparativo.listaInsumos.Add(insumoComparado);
                }

                foreach (DO_InsumoReportable insumoSegundoMes in listaSegundoMes)
                {
                    DO_InsumosComparados insumoComparado = new DO_InsumosComparados();

                    insumoComparado.insumoSegundoMes = insumoSegundoMes;
                    insumoComparado.diferenciaConsumir = 100;
                    insumoComparado.diferenciaDescarte = 100;
                    insumoComparado.diferenciaTotal = 100;

                    reporteInsComparativo.listaInsumos.Add(insumoComparado);
                }

                DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
                reporteInsComparativo.infoPaisMio = daoPaisMio.obtenerDatos();
                reporteInsComparativo.inicioMes1 = inicioMes1;
                reporteInsComparativo.inicioMes2 = inicioMes2;
                reporteInsComparativo.finalMes1 = finalMes1;
                reporteInsComparativo.finalMes2 = finalMes2;

                return reporteInsComparativo;
            }
        }

        private double sacarDiferenciaPorcentual(double mes1, double mes2) {
            if (mes1 == 0 && mes2 > 0)
            {
                return 100;
            }
            else if (mes2 == 0 && mes1 > 0)
            {
                return -100;
            }
            else if (mes2 == 0 && mes1 == 0)
            {
                return 0;
            }
            else {
                double resta = (mes2 - mes1);
                double division = resta / mes1;
                double total = division * 100;
                return total;
            }
        }

        private DO_InsumoReportable seRepite(Int32 insumoPrimerMes, List<DO_InsumoReportable> listaSegundoMes)
        {
            foreach (DO_InsumoReportable insumoSegundoMes in listaSegundoMes)
            {
                if (insumoPrimerMes == insumoSegundoMes.insumo.codigo)
                {
                    return insumoSegundoMes;
                }
            }
            return null;
        }

        public DO_ReporteEntradaInsumos reporteEntradaInsumos(String inicio, String final)
        {
            if (inicio is null || final is null
                || inicio == "" || final == "")
            {
                return null;
            }
            else
            {
                DAO_Reporte daoReporte = new DAO_Reporte();
                DO_ReporteEntradaInsumos doReporteEntradas = daoReporte.reporteEntradas(inicio, final);
                doReporteEntradas.fechaInicio = inicio;
                doReporteEntradas.fechaFinal = final;
                return doReporteEntradas;
            }
        }
    }
}
