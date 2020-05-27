using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Esta clse representa a los clientes de País mío
    /// </summary>
    /// 
    [DataContract]
    public class DO_Cliente
    {
        [DataMember(Name ="cedula")]
        public String cedula { set; get;}

        [DataMember(Name = "estado")]
        public String estado { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "telefono")]
        public String telefono { set; get; }

        [DataMember(Name = "correo")]
        public String correo { set; get; }

        [DataMember(Name = "direccion")]
        public String direccion { set; get; }
        
        public DO_Cliente(string cedula, String estado, string nombre, 
            string telefono, string correo, string direccion)
        {
            this.cedula = cedula;
            this.estado = estado;
            this.nombre = nombre;
            this.telefono = telefono;
            this.correo = correo;
            this.direccion = direccion;
        }

        public DO_Cliente()
        {
        }
    }
}
