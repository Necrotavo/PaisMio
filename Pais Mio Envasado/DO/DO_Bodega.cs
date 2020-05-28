using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clase representa a las bodegan en las que se almacenan los insumos
    /// </summary>
    [DataContract]
    public class DO_Bodega
    {
        [DataMember(Name = "cedula")]
        public Int32 codigo { set; get; }

        [DataMember(Name = "estado")]
        public String estado { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "direccion")]
        public String direccion { set; get; }

        [DataMember(Name = "telefono")]
        public String telefono { set; get; }

        [DataMember(Name = "listaInsumosEnBodega")]
        public List<DO_InsumoEnBodega> listaInsumosEnBodega { set; get; }
        
        public DO_Bodega(String estado, string nombre, string direccion, string telefono, List<DO_InsumoEnBodega> listaInsumosEnBodega)
        {
            this.estado = estado;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.listaInsumosEnBodega = listaInsumosEnBodega;
        }
        
        public DO_Bodega()
        {
            listaInsumosEnBodega = new List<DO_InsumoEnBodega>();
        }
    }
}
