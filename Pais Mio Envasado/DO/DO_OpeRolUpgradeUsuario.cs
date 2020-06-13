using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_OpeRolUpgradeUsuario
    {
        [DataMember]
        public DO_Operario usuario { get; set; }
        [DataMember]
        public string rolNuevo { get; set; }
        public DO_OpeRolUpgradeUsuario()
        {

        }
    }
}
