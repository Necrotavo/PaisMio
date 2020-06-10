using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_PaisMio" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_PaisMio.svc or WS_PaisMio.svc.cs at the Solution Explorer and start debugging.
    public class WS_PaisMio : IWS_PaisMio
    {
        public DO_PaisMio consultarDatos(DO_PaisMio datos)
        {
            BL_Pais_Mio blPaisMio = new BL_Pais_Mio();
            return blPaisMio.obtenerDatos();
        }

        public void DoWork()
        {
        }

        public bool modificarDatos(DO_PaisMio datos)
        {
            BL_Pais_Mio blPaisMio = new BL_Pais_Mio();
            return blPaisMio.modificarDatos(datos);
        }
    }
}
