using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Con esta clase se representa a los insumos que se utilizan en el proceso de embotellado
    /// </summary>
    [DataContract]
    public class DO_Insumo
    {
        [DataMember(Name = "codigo")]
        public Int32 codigo { set; get; }

        [DataMember(Name = "estado")]
        public String estado { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "cantMinStock")]
        public Int32 cantMinStock { set; get; }

        [DataMember(Name = "unidad")]
        public String unidad { set; get; }

        public DO_Insumo(String estado, string nombre, int cantMinStock, String unidad)
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
