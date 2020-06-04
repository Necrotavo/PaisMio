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


        public String aSensorial { get; set; }

        public String exOlfativo { get; set; }

        public String exVisual { get; set; }

        public String fechaEmision { get; set; }

        public String fechaVigencia { get; set; }

        public String producto { get; set; }

        public String notas { get; set; }


    }
}
