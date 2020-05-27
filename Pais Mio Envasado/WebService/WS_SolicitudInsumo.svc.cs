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
        public List<DO_SolicitudInsumos> listarInsumos()
        {
            BL_SolicitudInsumos blSolicitud = new BL_SolicitudInsumos();
            return blSolicitud.listaSolicitudes();
        }

        public string ingresarSolicitud(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            string res = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            return "Received: " + res;
        }
    }
}