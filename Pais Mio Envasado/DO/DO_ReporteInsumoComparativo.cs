using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// En esta clase se toman los datos necesarios para hacer un reporte comparativo de insumos
    /// </summary>
    [DataContract]
    public class DO_ReporteInsumoComparativo
    {
        [DataMember(Name = "insumoPrimerMes")]
        public DO_ReporteInsumos insumoPrimerMes { set; get; }

        [DataMember (Name= "insumoSegundoMes")]
        public DO_ReporteInsumos insumoSegundoMes { set; get; }

        [DataMember(Name = "diferenciaConsumir")]
        public double diferenciaConsumir { set; get; }

        [DataMember(Name = "diferenciaDescarte")]
        public double diferenciaDescarte { set; get; }

        [DataMember(Name = "diferenciaTotal")]
        public double diferenciaTotal { set; get; }

        public DO_ReporteInsumoComparativo(DO_ReporteInsumos insumoPrimerMes, DO_ReporteInsumos insumoSegundoMes, double diferenciaConsumir, double diferenciaDescarte, double diferenciaTotal)
        {
            this.insumoPrimerMes = insumoPrimerMes;
            this.insumoSegundoMes = insumoSegundoMes;
            this.diferenciaConsumir = diferenciaConsumir;
            this.diferenciaDescarte = diferenciaDescarte;
            this.diferenciaTotal = diferenciaTotal;
        }

        public DO_ReporteInsumoComparativo() { }
    }
}
