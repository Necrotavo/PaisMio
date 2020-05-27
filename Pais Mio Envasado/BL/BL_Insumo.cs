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
    /// En esta clase se encuentra la logica de negocio de los insumos
    /// </summary>
    public class BL_Insumo
    {
        /// <summary>
        /// Con este método se puede guardar un insumo en la base de datos
        /// </summary>
        /// <param name="doInsumo">El insumo que se va a guardar</param>
        /// <returns></returns>
        public bool guardarInsumo(DO_Insumo doInsumo)
        {
            DAO_Insumo daoInsumo = new DAO_Insumo();
            if (daoInsumo.guardarInsumo(doInsumo) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Con este método se perminte sacar toda la lista de insumos
        /// </summary>
        /// <returns>La lista de insumos existentes</returns>
        public List<DO_Insumo> obtenerListaIsumos() {
            DAO_Insumo daoInsumo = new DAO_Insumo();
            return daoInsumo.obtenerListaIsumos();
        }
    }
}
