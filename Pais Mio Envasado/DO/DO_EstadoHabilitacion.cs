using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace DO
{
    /// <summary>
    /// Esta clase sirve para tener los estados de habilitación para diversos objetos
    /// </summary>
    /// 
    [DataContract]
    public class DO_EstadoHabilitacion
    {
        
        [DataMember(Name = "estado")]
        public String estado { set; get; }

        public DO_EstadoHabilitacion(string estado)
        {
            this.estado = estado;
        }

        public DO_EstadoHabilitacion()
        {
        }
    }
}
