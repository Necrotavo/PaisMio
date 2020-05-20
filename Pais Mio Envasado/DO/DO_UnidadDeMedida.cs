using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public class DO_UnidadDeMedida
    {
        public String unidad { set; get; }

        public DO_UnidadDeMedida(string unidad)
        {
            this.unidad = unidad;
        }

        public DO_UnidadDeMedida()
        {
        }
    }
}