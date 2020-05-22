using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clase sirve para tener los estados de habilitación para diversos objetos
    /// </summary>
    public class DO_EstadoHabilitacion
    {
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
