using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clse representa a los clientes de País mío
    /// </summary>
    public class DO_Cliente
    {
        public String cedula { set; get;}
        public String estado { set; get; }
        public String nombre { set; get; }
        public String telefono { set; get; }
        public String correo { set; get; }
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
