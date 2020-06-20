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
        [DataMember(Name = "codigo")] public int codigoSolicitud { get; set; }
        [DataMember (Name = "operario")] public string correoOperario { get; set; }
        [DataMember(Name = "codigoPedido")] public int codigoPedido { get; set; }
        [DataMember(Name = "administrador")] public string correoAdministrador { get; set; }
        [DataMember(Name = "estado")] public string estado { get; set; }
        [DataMember(Name = "fecha")] public DateTime fechaSolicitud { get; set; }
        [DataMember(Name = "insumosConsumo")] public List<DO_InsumoEnBodega> listaConsumo { set; get; }
        [DataMember(Name = "insumosDescarte")] public List<DO_InsumoEnBodega> listaDescarte { set; get; }
        [DataMember(Name = "bodega")] public int codigoBodega { get; set; }
        [DataMember(Name = "notas")] public string notas { get; set; }
        /**  public DO_SolicitudInsumos(string operarioId, int bodega)
          {
              fechaSolicitud = System.DateTime.Now;
              correoOperario = operarioId;
              estado = "PENDIENTE";
              codigoBodega = bodega;
          }**/
        public DO_SolicitudInsumos()
        {

        }

        public DO_SolicitudInsumos(int codigoSolicitud, string correoOperario, int codigoPedido, string correoAdministrador, string estado, DateTime fechaSolicitud, List<DO_InsumoEnBodega> listaConsumo, List<DO_InsumoEnBodega> listaDescarte, int codigoBodega)
        {
            this.codigoSolicitud = codigoSolicitud;
            this.correoOperario = correoOperario;
            this.codigoPedido = codigoPedido;
            this.correoAdministrador = correoAdministrador;
            this.estado = estado;
            this.fechaSolicitud = fechaSolicitud;
            this.listaConsumo = listaConsumo;
            this.listaDescarte = listaDescarte;
            this.codigoBodega = codigoBodega;
        }
    }
}
