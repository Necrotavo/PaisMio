using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_InsumoEntrante
    {
        [DataMember(Name = "doBodega")]
        public DO_Bodega doBodega { get; set; }

        [DataMember(Name = "listaInsumos")]
        public DO_InsumoEnBodega insumo { get; set; }
    }
}
