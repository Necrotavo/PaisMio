using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;

namespace BL
{
    public class BL_Pais_Mio
    {
        public BL_Pais_Mio()
        {

        }

        /// <summary>
        /// Método para obtener los datos generales de País Mío.
        /// </summary>
        /// <returns>(DO_PaisMio) objeto con los datos de la empresa.</returns>
        public DO_PaisMio obtenerDatos()
        {
            DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
            return daoPaisMio.obtenerDatos();
        }

        /// <summary>
        /// Método para modificar los datos existentes de País Mío.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool modificarDatos(DO_PaisMio datos)
        {
            DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
            return daoPaisMio.modificarDatos(datos);
        }
    }
}
