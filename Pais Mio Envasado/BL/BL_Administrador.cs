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
        /// Metodo para llamar al meétodo agregarOperario del DAO_Aministrador
        /// </summary>
        /// <param name="correo"> correo del administrador</param>
        /// <param name="estado"> estado del administrador, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del administrador</param>
        /// <param name="apellidos"> apellidos del administrador</param>
        /// <param name="contrasena"> contrasena del administrador</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public bool agregarAdministrador(DO_Operario doOperario) {

            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();
            DAO_Administrador DAOadministrador = new DAO_Administrador();

            String supervisor = DAOoperario.getQueryInsertar() + DAOsupervisor.getQueryInsertar();

            string pass = DAOadministrador.agregarAdministrador(doOperario, supervisor);

            if (!(pass is null))
            {
                BL_Operario BLoperario = new BL_Operario();
                string subject = "Contraseña País Mío";

                string body = "<p>Su contraseña temporal es: " + pass + "</p><br>" +
                    "<a href =https://pais-mio-industria-artesanal.web.app/ >Click aquí para ir al sitio de País Mío</a><br>" +
                    "<p>Saludos!</p>";

                BLoperario.enviarCorreo(doOperario.correo, subject, body);
                return true;
            }

            return false;
        }
    }
}
