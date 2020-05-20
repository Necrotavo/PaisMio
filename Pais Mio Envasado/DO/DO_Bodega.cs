using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DO_Bodega
    {
        public Int32 codigo { set; get; }
        public DO_EstadoHabilitacion estado { set; get; }
        public String nombre { set; get; }
        public String direccion { set; get; }
        public String telefono { set; get; }
        public List<DO_InsumoEnBodega> listaInsumosEnBodega { set; get; }

        public DO_Bodega(int codigo, DO_EstadoHabilitacion estado, string nombre, string direccion, string telefono, List<DO_InsumoEnBodega> listaInsumosEnBodega)
        {
            this.codigo = codigo;
            this.estado = estado;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.listaInsumosEnBodega = listaInsumosEnBodega;
        }

        public DO_Bodega()
        {
        }
    }
}
