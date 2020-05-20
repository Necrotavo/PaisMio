using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DO_Operario
    {
        public String correo { set; get; }
        public DO_EstadoHabilitacion estado { set; get; }
        public String nombre { set; get; }
        public String apellidos { set; get; }
        public String contrasena { set; get; }

        public DO_Operario(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena)
        {
            this.correo = correo;
            this.estado = estado;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.contrasena = contrasena;
        }

        public DO_Operario()
        {
        }
    }
}
