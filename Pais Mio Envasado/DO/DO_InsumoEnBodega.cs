using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clase sirve para representar un insumo y una cantidad del mismo, para hacer listas
    /// </summary>
    [DataContract]
    public class DO_InsumoEnBodega
    {
        [DataMember(Name = "insumo")]
        public DO_Insumo insumo { set; get; }

        [DataMember(Name = "cantidadDisponible")]
        public Int32 cantidadDisponible { set; get; }

        public DO_InsumoEnBodega(DO_Insumo insumo, int cantidadDisponible)
        {
            this.insumo = insumo;
            this.cantidadDisponible = cantidadDisponible;
        }

        public DO_InsumoEnBodega()
        {
        }
    }
}
