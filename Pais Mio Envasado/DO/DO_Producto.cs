using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DO_Producto
    {
        public Int32 codigo { set; get; }
        public String nombre { set; get; }
        public String descripcion { set; get; }
        public DO_EstadoHabilitacion estado { set; get; }

        public DO_Producto(int codigo, string nombre, string descripcion, DO_EstadoHabilitacion estado)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.estado = estado;
        }

        public DO_Producto()
        {
        }
    }
}
