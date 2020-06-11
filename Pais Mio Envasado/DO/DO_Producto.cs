using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Representa a los productos finales del proceso de embotellado
    /// </summary>
    [DataContract]
    public class DO_Producto
    {
        [DataMember(Name ="codigo")]
        public Int32 codigo { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "descripcion")]
        public String descripcion { set; get; }

        [DataMember(Name = "estado")]
        public String estado { set; get; }

        [DataMember(Name = "id")]
        public String id { set; get; }

        public DO_Producto(int codigo, string nombre, string descripcion, string estado, string id)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.estado = estado;
            this.id = id;
        }

        public DO_Producto()
        {
        }
    }
}
