using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DO_Administrador : DO_Supervisor
    {
        public DO_Administrador(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena)
                : base(correo, estado, nombre, apellidos, contrasena)
        {
        }
    }
}
