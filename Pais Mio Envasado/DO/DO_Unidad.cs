using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_Unidad
    {
        [DataMember]
        public string unidad { get; set; }

        public DO_Unidad()
        {
            this.unidad = "";
        }

        public DO_Unidad(string v)
        {
            this.unidad = v;
        }
    }
}
