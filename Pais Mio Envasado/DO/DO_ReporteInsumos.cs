using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_ReporteInsumos
    {
        [DataMember(Name = "listaInsumos")]
        public List<DO_InsumoReportable> listaInsumos { get; set; }

        [DataMember(Name = "infoPaisMio")]
        public DO_PaisMio infoPaisMio { get; set; }

        [DataMember(Name = "fechaInicio")]
        public string fechaInicio { get; set; }

        [DataMember(Name = "fechaFinal")]
        public string fechaFinal { get; set; }

        public DO_ReporteInsumos(List<DO_InsumoReportable> listaInsumos, DO_PaisMio infoPaisMio, string fechaInicio, string fechaFinal)
        {
            this.listaInsumos = listaInsumos;
            this.infoPaisMio = infoPaisMio;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
        }

        public DO_ReporteInsumos(){}
    }
}
