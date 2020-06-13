using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    [DataContract]
    public class DO_EntradaInsumosBodega
    {
        [DataMember]
        public DO_Bodega doBodega { get; set; }
        [DataMember]
        public string correoAdministrador { get; set; }

        public DO_EntradaInsumosBodega()
        {

        }
    }
}
