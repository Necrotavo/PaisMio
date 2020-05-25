using BL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_SolicitudInsumo" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_SolicitudInsumo.svc or WS_SolicitudInsumo.svc.cs at the Solution Explorer and start debugging.
    public class WS_SolicitudInsumo : IWS_SolicitudInsumo
    {
        public List<DO_SolicitudInsumos> listarInsumos()
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.listaSolicitudes();
        }

        public bool ingresarSolicitud(string operadorId, int codigoPedido, int bodega, List<DO_InsumoEnBodega> consumidos, List<DO_Insumo> descartados)
        {
            DO_SolicitudInsumos solicitud = new DO_SolicitudInsumos(operadorId, bodega)
        }
    }
}
