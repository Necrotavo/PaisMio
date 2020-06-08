using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;
using System.Net.Mail;

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
        public bool agregarOperario(DO_Operario doOperario) {

            DAO_Operario DAOoperario = new DAO_Operario();

            return DAOoperario.agregarOperario(doOperario);
        }

        /// <summary>
        /// Método para generar la contraseña y recibirla
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string generarContrasena(string correo)
        {
            DAO_Operario DAOoperario = new DAO_Operario();
            return DAOoperario.nuevaContrasena(correo);
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

        public void recuperacionContrasena(string correo) {
            // DAO_Operario se debe validar si existe el correo
            if (true) {
                //generar token y guardarlo
                //método void enviar correo(string token)
                //
            }
        }

        private void enviarCorreo(string token, string correoDestino) {
            //lógica 
            /*
             Origen
            Contrasena
            url tiene el token
            mail message 

            body 
             */
            MailMessage message = new MailMessage("correoOrigen",correoDestino,"Recuperación de contraseña","body");
        }

        /**
        apsx Cambio de contraseña
        
        primero mostrar un botón para cambiar la contraseña
          . cuando se genere la contraseña se debe poner el token en null
           y se le envía la contraseña nueva al correo

         */
    }
}
