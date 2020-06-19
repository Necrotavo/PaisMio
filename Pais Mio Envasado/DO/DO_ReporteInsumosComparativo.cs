using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_ReporteInsumosComparativo
    {
        [DataMember(Name = "listaInsumos")]
        public List<DO_InsumosComparados> listaInsumos { get; set; }

        [DataMember(Name = "infoPaisMio")]
        public DO_PaisMio infoPaisMio { get; set; }

        [DataMember(Name = "inicioMes1")]
        public String inicioMes1 { get; set; }

        [DataMember(Name = "finalMes1")]
        public String finalMes1 { get; set; }

        [DataMember(Name = "inicioMes2")]
        public String inicioMes2 { get; set; }

        [DataMember(Name = "finalMes2")]
        public String finalMes2 { get; set; }

        public DO_ReporteInsumosComparativo(List<DO_InsumosComparados> listaInsumos, DO_PaisMio infoPaisMio, string inicioMes1, string finalMes1, string inicioMes2, string finalMes2)
        {
            this.listaInsumos = listaInsumos;
            this.infoPaisMio = infoPaisMio;
            this.inicioMes1 = inicioMes1;
            this.finalMes1 = finalMes1;
            this.inicioMes2 = inicioMes2;
            this.finalMes2 = finalMes2;
        }

        public DO_ReporteInsumosComparativo() { }
    }
}
