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
            if (doInsumo is null || insumoEstaVacio(doInsumo))
            {
                return false;
            }
            else {
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
        }

        /// <summary>
        /// Determina si los atributos del insumo  están vacíos o son inválidos
        /// </summary>
        /// <param name="doInsumo">Insumo a verificar</param>
        /// <returns>True si está vacío false, si no lo está</returns>
        private bool insumoEstaVacio(DO_Insumo doInsumo) {
            if (doInsumo.nombre is null || doInsumo.unidad is null ||
                doInsumo.nombre == "" || doInsumo.unidad == "" || doInsumo.cantMinStock <= 0)
            {
                return true;
            }
            else {
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

        /// <summary>
        /// Permite obtener la lista de insumos habilitados
        /// </summary>
        /// <returns>Lista de insumos habilitados</returns>
        public List<DO_Insumo> obtenerListaIsumosHabilitados()
        {
            DAO_Insumo daoInsumo = new DAO_Insumo();
            return daoInsumo.obtenerListaIsumosHabilitados();
        }

        /// <summary>
        /// Permite modificar los datos de un insumo
        /// </summary>
        /// <param name="doInsumo">Insumo con los datos a modificar</param>
        /// <returns>True si se modifican y false si sucede un error</returns>
        public bool modificarInsumo(DO_Insumo doInsumo)
        {
            if (doInsumo.cantMinStock <= 0 || doInsumo.codigo <= 0)
            {
                return false;
            }
            else {
                DAO_Insumo daoInsumo = new DAO_Insumo();
                return daoInsumo.modificarInsumo(doInsumo);
            }
        }

        /// <summary>
        /// Mediante el código de un insumo, recupera los datos del mismo
        /// </summary>
        /// <param name="codigoInsumo">Codigo del insumo buscado</param>
        /// <returns>El insumo con todos sus datos, null si no lo encuentra</returns>
        public DO_Insumo buscarInsumo(Int32 codigoInsumo) {
            if (codigoInsumo <= 0)
            {
                return null;
            }
            else
            {
                DAO_Insumo daoInsumo = new DAO_Insumo();
                return daoInsumo.buscarInsumoPorCódigo(codigoInsumo);
            }
        }
    }
}
