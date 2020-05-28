using DO;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Insumo" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Insumo.svc or WS_Insumo.svc.cs at the Solution Explorer and start debugging.
    public class WS_Insumo : IWS_Insumo
    {
        public bool agregarInsumo(DO_Insumo doInsumo)
        {
            BL_Insumo blInsumo = new BL_Insumo();

            return blInsumo.guardarInsumo(doInsumo);
        }

        public bool entradaInsumos(DO_Bodega doBodega, string correoAdministrador)
        {
            BL_Bodega blBodega = new BL_Bodega();

            return blBodega.entradaInsumos(doBodega, correoAdministrador);
        }

        public List<DO_Insumo> obtenerListaInsumos()
        {
            BL_Insumo blInsumo = new BL_Insumo();

            return blInsumo.obtenerListaIsumos();
        }
    }
}
