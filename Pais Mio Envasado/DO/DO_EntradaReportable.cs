using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [DataContract]
    public class DO_EntradaReportable
    {
        [DataMember(Name = "codigo")]
        public Int32 codigo { get; set; }

        [DataMember(Name = "correoAdministrador")]
        public string correoAdministrador { get; set; }

        [DataMember(Name = "fecha")]
        public string fecha { get; set; }

        [DataMember(Name = "listaInsumos")]
        public List<DO_InsumoEntrante> listaInsumos { get; set; }

        public DO_EntradaReportable(int codigo, string correoAdministrador, string fecha, List<DO_InsumoEntrante> listaInsumos)
        {
            this.codigo = codigo;
            this.correoAdministrador = correoAdministrador;
            this.fecha = fecha;
            this.listaInsumos = listaInsumos;
        }

        public DO_EntradaReportable() { }
    }
}
