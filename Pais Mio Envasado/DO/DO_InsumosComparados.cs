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
    public class DO_InsumosComparados
    {
        [DataMember(Name = "insumoPrimerMes")]
        public DO_InsumoReportable insumoPrimerMes { set; get; }

        [DataMember (Name= "insumoSegundoMes")]
        public DO_InsumoReportable insumoSegundoMes { set; get; }

        [DataMember(Name = "diferenciaConsumir")]
        public double diferenciaConsumir { set; get; }

        [DataMember(Name = "diferenciaDescarte")]
        public double diferenciaDescarte { set; get; }

        [DataMember(Name = "diferenciaTotal")]
        public double diferenciaTotal { set; get; }

        public DO_InsumosComparados(DO_InsumoReportable insumoPrimerMes, DO_InsumoReportable insumoSegundoMes, double diferenciaConsumir, double diferenciaDescarte, double diferenciaTotal)
        {
            this.insumoPrimerMes = insumoPrimerMes;
            this.insumoSegundoMes = insumoSegundoMes;
            this.diferenciaConsumir = diferenciaConsumir;
            this.diferenciaDescarte = diferenciaDescarte;
            this.diferenciaTotal = diferenciaTotal;
        }

        public DO_InsumosComparados() { }
    }
}
