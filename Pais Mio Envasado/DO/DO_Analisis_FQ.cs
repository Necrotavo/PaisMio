using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Esta clase representa el apartado Análisis fisicoquímico perteneciente al Análisis de Aguardiente
    /// </summary>
    [DataContract]
    public class DO_Analisis_FQ
    {
        //PED_CODIGO, TAF_TIPO_ANALISIS_FQ, AFQ_MEDICION_RESULTADO, AFQ_UNIDAD_CONDICION
        [DataMember (Name ="pedCodigo")]
        public int pedCodigo { get; set; }

        [DataMember(Name = "tipoAnalisisFQ")]
        public String tipoAnalisisFQ { get; set; }

        [DataMember(Name = "medicionResultado")]
        public String medicionResultado { get; set; }

        [DataMember(Name = "unidadCondicion")]
        public String unidadCondicion { get; set; }
        
        public DO_Analisis_FQ(int pedCodigo, String tipoAnalisisFQ, String medicionResultado, String unidadCondicion) {
            this.pedCodigo = pedCodigo;
            this.tipoAnalisisFQ = tipoAnalisisFQ;
            this.medicionResultado = medicionResultado;
            this.unidadCondicion = unidadCondicion;
        }

        public DO_Analisis_FQ() { }
    }
}
