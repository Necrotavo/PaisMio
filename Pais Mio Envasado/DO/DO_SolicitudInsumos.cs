using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace DO
{
    /// <summary>
    /// Representa a las solicitudes de insumo que realizan los operarios
    /// </summary>
    [DataContract]
    public class DO_SolicitudInsumos
    {


        private readonly string[] estados = new string[] { "En proceso", "Rechazada", "Aprobada" };
        [DataMember(Name = "codigo")] public int codigoSolicitud { get; set; }
        [DataMember (Name = "operario")] public string correoOperario { get; set; }
        [DataMember(Name = "codigoPedido")] public int codigoPedido { get; set; }
        [DataMember(Name = "administrador")] public string correoAdministrador { get; set; }
        [DataMember(Name = "estado")] public string estado { get; set; }
        [DataMember(Name = "fecha")] public DateTime fechaSolicitud { get; set; }
        [DataMember(Name = "insumosConsumo")] public List<DO_InsumoEnBodega> listaConsumo { set; get; }
        [DataMember(Name = "insumosDescarte")] public List<DO_InsumoEnBodega> listaDescarte { set; get; }
        [DataMember(Name = "bodega")] public int codigoBodega { get; set; }
        public DO_SolicitudInsumos(string operarioId, int bodega)
        {
            fechaSolicitud = System.DateTime.Now;
            correoOperario = operarioId;
            estado = "En proceso";
            codigoBodega = bodega;
        }
        public DO_SolicitudInsumos()
        {

        }

    }
}
