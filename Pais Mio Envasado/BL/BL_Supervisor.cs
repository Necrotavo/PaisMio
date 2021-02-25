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
        public bool agregarSupervisor(DO_Operario doOperario) {
            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();
            

            string pass = DAOsupervisor.agregarSupervisor(doOperario, DAOoperario.getQueryInsertar());


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
        public bool upgradeRol(DO_Operario usuario, string rol)
        {
            DAO_Operario sup = new DAO_Operario();
            if (rol.Equals("ADMINISTRADOR"))
            {
                return sup.upSupToAdm(usuario);
            }
            return false;
        }
    }
}
