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
        public bool cambiarEstadoBodega(int codigoBodega, string estado)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.cambiarEstadoBodega(codigoBodega, estado);
        }

        public bool entradaInsumos(DO_Bodega doBodega, string correoAdministrador)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.entradaInsumos(doBodega, correoAdministrador);
        }

        public bool modificarBodega(DO_Bodega doBodega)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.modificarBodega(doBodega);
        }

        public DO_Bodega obtenerBodega(int codigoBodega)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.obtenerBodega(codigoBodega);
        }

        public List<DO_Bodega> obtenerListaBodegas()
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.obtenerListaBodegas();
        }

        public List<DO_Bodega> obtenerListaBodegasHabilitados()
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.obtenerListaBodegasHabilitados();
        }

        public bool registrarBodega(DO_Bodega doBodega)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.registrarBodega(doBodega);
        }

        public bool moverInsumoDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega, Int32 codigoInsumo, Int32 cantidad)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.moverInsumoDeBodega(codigoDesdeBodega, codigoHastaBodega, codigoInsumo, cantidad);
        }

        //public bool moverTodosInsumosDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega)
        //{
        //    BL_Bodega blBodega = new BL_Bodega();
        //    return blBodega.moverTodosInsumosDeBodega(codigoDesdeBodega, codigoHastaBodega);
        //}

        public List<DO_InsumoEnBodega> obtenerInsumosBodega(int codigoBodega)
        {
            BL_Bodega blBodega = new BL_Bodega();
            return blBodega.obtenerInsumosBodega(codigoBodega);
        }
    }
}
