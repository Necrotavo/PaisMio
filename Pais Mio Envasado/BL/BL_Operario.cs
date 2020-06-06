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
    /// Esta clase llama a los métodos del DAO_Operario
    /// </summary>
    public class BL_Operario
    {
        /// <summary>
        /// Metodo para llamar al meétodo agregarOperario del DAO_Operario
        /// </summary>
        /// <param name="correo"> correo del operario</param>
        /// <param name="estado"> estado del operario, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del operario</param>
        /// <param name="apellidos"> apellidos del operario</param>
        /// <param name="contrasena"> contrasena del operario</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public bool agregarOperario(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {

            DAO_Operario DAOoperario = new DAO_Operario();

            return DAOoperario.agregarOperario(correo, estado, nombre, apellidos, contrasena);
        }

        public bool generarContrasena(string correo)
        {
            DAO_Operario DAOoperario = new DAO_Operario();
            return DAOoperario.generarContrasena(correo);
        }

        public DO_Operario buscarOperario(String correo) {
            DAO_Operario DAOoperario = new DAO_Operario();

            return DAOoperario.buscarOperario(correo);
        }

        public List<DO_Operario> obtenerListaOperario() {

            DAO_Operario  DAOoperario = new DAO_Operario();
            return DAOoperario.obtenerListaOperarios();
        }
        /// <summary>
        /// Método para modificar el estado de un usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario a modificar</param>
        /// <param name="estado">Nuevo estado del usuario</param>
        /// <returns>(True) si se modificó correctamente. (False) si no se modificó.</returns>
        public bool modificarEstadoUsuario(String correo, String estado)
        {
            DAO_Operario daoOperario = new DAO_Operario();
            return daoOperario.modificarEstado(estado,correo);
        }
    }
}
