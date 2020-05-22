using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;

namespace BL
{
    /// <summary>
    /// Esta clase llama a los métodos del DAO_Supervisor
    /// </summary>
    public class BL_Supervisor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="estado"></param>
        /// <param name="nombre"></param>
        /// <param name="apellidos"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public bool agregarSupervisor(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {
            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();

            return DAOsupervisor.agregarSupervisor(correo, estado, nombre, apellidos, contrasena,DAOoperario.getQueryInsertar());
        }
    }
}
