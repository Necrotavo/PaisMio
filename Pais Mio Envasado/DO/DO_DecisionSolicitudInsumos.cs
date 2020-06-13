using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_DecisionSolicitudInsumos
    {
        [DataMember]
        public DO_SolicitudInsumos solicitud { get; set; }
        [DataMember]
        public DO_Operario admin { get; set; }
        [DataMember]
        public string estado { get; set; }

        public DO_DecisionSolicitudInsumos()
        {

        }
    }
}
