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

        public bool agregarUnidadDeMedida(DO_Unidad unidad)
        {
            BL_UnidadDeMedida blUnidadDeMedida = new BL_UnidadDeMedida();
            return blUnidadDeMedida.agregarUnidad(unidad.unidad);
        }

        public DO_Insumo buscarInsumo(int codigoInsumo)
        {
            BL_Insumo blInsumo = new BL_Insumo();
            return blInsumo.buscarInsumo(codigoInsumo);
        }

        public List<string> listarUnidades()
        {
            BL_UnidadDeMedida blUnidadDeMedida = new BL_UnidadDeMedida();
            return blUnidadDeMedida.listarUnidades();
        }

        public bool modificarInsumo(DO_Insumo doInsumo)
        {
            BL_Insumo blInsumo = new BL_Insumo();
            return blInsumo.modificarInsumo(doInsumo);
        }

        public List<DO_Insumo> obtenerListaInsumos()
        {
            BL_Insumo blInsumo = new BL_Insumo();
            return blInsumo.obtenerListaIsumos();
        }

        public List<DO_Insumo> obtenerListaInsumosHabilitados()
        {
            BL_Insumo blInsumo = new BL_Insumo();
            return blInsumo.obtenerListaIsumosHabilitados();
        }
    }
}
