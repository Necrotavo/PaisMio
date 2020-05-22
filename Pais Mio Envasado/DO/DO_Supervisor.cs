using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Clase para los supervisores, heredan los atributos de la clase Operario
    /// </summary>
    public class DO_Supervisor : DO_Operario
    {
        public DO_Supervisor()
        {
        }

        public DO_Supervisor(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena)
                : base(correo, estado, nombre, apellidos, contrasena)
        {
        }
    }
}
