using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public class UnidadDeMedida
    {
        public String unidad { set; get; }

        public UnidadDeMedida(string unidad)
        {
            this.unidad = unidad;
        }

        public UnidadDeMedida()
        {
        }
    }
}