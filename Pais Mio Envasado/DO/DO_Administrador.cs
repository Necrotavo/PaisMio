using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Clase para los administradores, heredan los atributos de la clase Supervisor
    /// </summary>
    public class DO_Administrador : DO_Supervisor
    {
        public DO_Administrador(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena)
                : base(correo, estado, nombre, apellidos, contrasena)
        {
        }
        
        public DO_Administrador() { }
    }
}
