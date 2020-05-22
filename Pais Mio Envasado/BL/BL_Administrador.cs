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
    /// Esta clase llama a los métodos del DAO_Adminstrador
    /// </summary>
    public class BL_Administrador
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
        public bool agregarAdministrador(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {

            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();
            DAO_Administrador DAOadministrador = new DAO_Administrador();

            String supervisor = DAOoperario.getQueryInsertar() + DAOsupervisor.getQueryInsertar();

            return DAOadministrador.agregarAdministrador(correo, estado, nombre, apellidos, contrasena, correo);
        }
    }
}
