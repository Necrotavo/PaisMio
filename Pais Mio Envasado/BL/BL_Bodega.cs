using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// En esta clase se encuentra la logica de negocio de las Bodegas
    /// </summary>
    public class BL_Bodega
    {
        public BL_Bodega() { }

        /// <summary>
        /// Este método permite la entrada de insumos al inventario
        /// </summary>
        /// <param name="doBodega">Bodega con la lista de insumos entrantes</param> 
        /// <param name="correoOperario">Correo del operario que realiza la entrada</param>
        /// <returns>True si se ingresan los insumos en la bodega, false si sucede un error</returns>
        public bool entradaInsumos(DO_Bodega doBodega, String correoOperario) {
            DAO_Bodega daoBodega = new DAO_Bodega();
            return daoBodega.entradaInsumos(doBodega, correoOperario);
        }
    }
}
