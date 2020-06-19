using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;

namespace BL
{
    public class BL_UnidadDeMedida
    {
        /// <summary>
        /// Método para agregar una nueva unidad de medida.
        /// </summary>
        /// <param name="unidad">(String) unidad de medidad a agregar</param>
        /// <returns>(True)si se registró el cambio. (False)si no se registró</returns>
        public bool agregarUnidad(String unidad)
        {
            DAO_UnidadDeMedida daoUnidad = new DAO_UnidadDeMedida();
            return daoUnidad.agregarUnidad(unidad);
        }

       /// <summary>
       /// Método para mostrar las unidades de medida disponibles
       /// </summary>
       /// <returns>(List<String> lista de las unidades de medida.)</returns>
        public List<DO_Unidad> listarUnidades()
        {
            DAO_UnidadDeMedida daoUnidad = new DAO_UnidadDeMedida();
            return daoUnidad.listaUnidades();
        }
    }
}
