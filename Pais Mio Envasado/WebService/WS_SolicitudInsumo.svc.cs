﻿using BL;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<DO_SolicitudInsumos> listarSolicitudes()
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.listaSolicitudes();
        }

        public bool ingresarSolicitud(DO_SolicitudInsumos solicitud)
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            solicitud.fechaSolicitud = DateTime.Now;
            solicitud.estado = "PENDIENTE";
            if (blSolicitud.crearNuevaSolicitud(solicitud))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DO_SolicitudInsumos solicitarSolicitud(int idSolicitud)
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.consultaSolicitud(idSolicitud);
        }

        public bool decision(DO_DecisionSolicitudInsumos rolUpgrade)
        {

            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.decisionAdmin(rolUpgrade.solicitud, rolUpgrade.admin, rolUpgrade.estado);
        }

        public List<DO_SolicitudInsumos> listarSolicitudesPorPedido(DO_Pedido pedido)
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.listaSolicitudesPorPedido(pedido.codigo);
        }

        public List<DO_SolicitudInsumos> listarSolicitudesPorOperario(DO_SolicitudInsumos operario)
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.listaSolicitudesPorOperario(operario);
        }
    }
}
