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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Bodega" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Bodega.svc or WS_Bodega.svc.cs at the Solution Explorer and start debugging.
    public class WS_Bodega : IWS_Bodega
    {
        public bool entradaInsumos(DO_Bodega doBodega, string correoAdministrador)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.entradaInsumos(doBodega, correoAdministrador);
        }

        public DO_Bodega obtenerBodega(int codigoBodega)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.obtenerBodega(codigoBodega);
        }
    }
}
