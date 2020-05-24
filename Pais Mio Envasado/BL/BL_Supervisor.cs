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
        /// Metodo para llamar al meétodo agregarOperario del DAO_Supervisor
        /// </summary>
        /// <param name="correo"> correo del supervisor</param>
        /// <param name="estado"> estado del supervisor, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del supervisor</param>
        /// <param name="apellidos"> apellidos del supervisor</param>
        /// <param name="contrasena"> contrasena del supervisor</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public bool agregarSupervisor(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {
            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();

            return DAOsupervisor.agregarSupervisor(correo, estado, nombre, apellidos, contrasena,DAOoperario.getQueryInsertar());
        }
    }
}
