using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DO_Insumo
    {
        public Int32 codigo { set; get; }
        public DO_EstadoHabilitacion estado { set; get; }
        public String nombre { set; get; }
        public Int32 cantMinStock { set; get; }
        public DO_UnidadDeMedida unidad { set; get; }

        public DO_Insumo(DO_EstadoHabilitacion estado, string nombre, int cantMinStock, DO_UnidadDeMedida unidad)
        {
            this.estado = estado;
            this.nombre = nombre;
            this.cantMinStock = cantMinStock;
            this.unidad = unidad;
        }

        public DO_Insumo()
        {
        }
    }
}
