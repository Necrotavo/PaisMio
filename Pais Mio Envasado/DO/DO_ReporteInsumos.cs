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
    public class DO_ReporteInsumos
    {
        [DataMember(Name = "CantidadConsumida")]
        public Int32 cantidadConsumida;

        [DataMember(Name = "cantidadDescartada")]
        public Int32 cantidadDescartada;

        [DataMember(Name = "insumo")]
        public DO_Insumo insumo;

        public DO_ReporteInsumos(int cantidadConsumida, int cantidadDescartada, DO_Insumo insumo)
        {
            this.cantidadConsumida = cantidadConsumida;
            this.cantidadDescartada = cantidadDescartada;
            this.insumo = insumo;
        }

        public DO_ReporteInsumos(){}
    }
}
