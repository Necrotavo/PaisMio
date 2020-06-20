using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clase contiene un insumo, y las cantidades que se consumieron o descartaron del mismo para reportarlos
    /// </summary>
    [DataContract]
    public class DO_InsumoReportable
    {
        [DataMember(Name = "CantidadConsumida")]
        public Int32 cantidadConsumida;

        [DataMember(Name = "cantidadDescartada")]
        public Int32 cantidadDescartada;

        [DataMember(Name = "insumo")]
        public DO_Insumo insumo;

        [DataMember(Name = "total")]
        public Int32 total { set; get; }

        public DO_InsumoReportable(int cantidadConsumida, int cantidadDescartada, DO_Insumo insumo, int total)
        {
            this.cantidadConsumida = cantidadConsumida;
            this.cantidadDescartada = cantidadDescartada;
            this.insumo = insumo;
            this.total = total;
        }

        public DO_InsumoReportable(){}
    }
}
