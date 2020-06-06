using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace DO
{
    [DataContract]
    public class DO_Analisis_AA
    {
        //ANA_ARMONIA_SENSORIAL,ANA_EXAMEN_GUSTATIVO,
        //ANA_EXAMEN_OLFATIVO,ANA_EXAMEN_VISUAL,ANA_FECHA_EMISION,ANA_FECHA_VIGENCIA,
        //ANA_NOMBRE_PRODUCTO,ANA_NOTAS,IPM_CODIGO, PED_CODIGO

        [DataMember(Name = "pedCodigo")]
        public int pedCodigo { get; set; }

        [DataMember(Name = "ipmCodigo")]
        public int ipmCodigo { get; set; }

        [DataMember(Name = "aSensorial")]
        public int aSensorial { get; set; }

        [DataMember(Name = "exGustativo")]
        public int exGustativo { get; set; }

        [DataMember(Name = "exOlfativo")]
        public int exOlfativo { get; set; }

        [DataMember(Name = "exVisual")]
        public int exVisual { get; set; }

        [DataMember(Name = "fechaEmision")]
        public String fechaEmision { get; set; }

        [DataMember(Name = "fechaVigencia")]
        public String fechaVigencia { get; set; }

        [DataMember(Name = "nombreProducto")]
        public String nombreProducto { get; set; }

        [DataMember(Name = "notas")]
        public String notas { get; set; }

        [DataMember(Name = "analisisFQs")]
        public List<DO_Analisis_FQ> analisisFQs {get; set;}

        public DO_Analisis_AA(int pedCodigo, int impCodigo, int aSensorial, int exGustativo,int exOlfativo, int exVisual, String fechaEmision,
            String fechaVigencia, String nombreProducto, String notas, List<DO_Analisis_FQ> analisisFQs){
            this.pedCodigo = pedCodigo;
            this.ipmCodigo = impCodigo;
            this.aSensorial = aSensorial;
            this.exGustativo = exGustativo;
            this.exOlfativo = exOlfativo;
            this.exVisual = exVisual;
            this.fechaEmision = fechaEmision;
            this.fechaVigencia = fechaVigencia;
            this.nombreProducto = nombreProducto;
            this.notas = notas;
            this.analisisFQs = analisisFQs;
        }

        public DO_Analisis_AA () { }

    }
}
