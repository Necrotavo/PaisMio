using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_ReporteEntradaInsumos
    {
        [DataMember(Name = "listaEntradas")]
        public List<DO_EntradaReportable> listaEntradas { set; get; }

        [DataMember(Name = "fechaInicio")]
        public String fechaInicio { set; get; }

        [DataMember(Name = "fechaFinal")]
        public String fechaFinal { set; get; }

        [DataMember(Name = "infoPaisMio")]
        public DO_PaisMio infoPaisMio { get; set; }

        public DO_ReporteEntradaInsumos(List<DO_EntradaReportable> listaEntradas, string fechaInicio, string fechaFinal, DO_PaisMio infoPaisMio)
        {
            this.listaEntradas = listaEntradas;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
            this.infoPaisMio = infoPaisMio;
        }

        public DO_ReporteEntradaInsumos(){}
    }
}
