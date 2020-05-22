using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Representa a las solicitudes de insumo que realizan los operarios
    /// </summary>
    public class DO_SolicitudInsumos
    {
        private readonly string[] estados = new string[] { "En proceso", "Rechazada", "Aprobada" };
        public int codigoSolicitud { get; set; }
        public string correoOperario { get; set; }
        public int codigoPedido { get; set; }
        public string correoAdministrador { get; set; }
        public string estado { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public List<DO_InsumoEnBodega> listaConsumo { set; get; }
        public List<DO_InsumoEnBodega> listaDescarte { set; get; }
        public int codigoBodega { get; set; }
        DO_SolicitudInsumos(string operarioId, int bodega)
        {
            fechaSolicitud = System.DateTime.Now;
            correoOperario = operarioId;
            estado = estados[0];
            codigoBodega = bodega;
        }



        /// <summary>
        /// Metodo para cambiar el estado de la solicitud
        /// </summary>
        /// <param name="adminId">Es el Id del administrador</param>
        /// <param name="estadoIndex">0 para en proceso, 1 para Rechazada, 2 para aprobada</param>
        public void cambiarEstado(string adminId, int estadoIndex)
        {
            estado = estados[estadoIndex];
            correoAdministrador = adminId;
        }


    }
}
