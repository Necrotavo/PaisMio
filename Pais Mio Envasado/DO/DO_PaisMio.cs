using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Clase que representa los datos de País Mío.
    /// </summary>
    [DataContract]
    public class DO_PaisMio
    {
        [DataMember (Name = "codigo")]
        public Int32 codigo { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "cedulaJuridica")]
        public String cedula { set; get; }

        [DataMember(Name = "correo")]
        public String correo { set; get; }

        [DataMember(Name = "telefono")]
        public String telefono { set; get; }

        [DataMember(Name = "direccion")]
        public String direccion { set; get; }

        [DataMember(Name = "logo")]
        public String logo { set; get; }

        public DO_PaisMio(int codigo, string nombre, string cedula, string correo, string telefono, string direccion)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.cedula = cedula;
            this.correo = correo;
            this.telefono = telefono;
            this.direccion = direccion;
        }

        public DO_PaisMio(int codigo, string nombre, string cedula, string correo, string telefono, string direccion, string logo) : this(codigo, nombre, cedula, correo, telefono, direccion)
        {
            this.logo = logo;
        }

        public DO_PaisMio()
        {
        }
    }
}
