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

            string pass = DAOoperario.agregarOperario(doOperario);


            if (!(pass is null)) {

                string subject = "Contraseña País Mío";

                string body = "<p>Su contraseña temporal es: " + pass + "</p><br>" +
                    "<a href =https://pais-mio-industria-artesanal.web.app/ >Click aquí para ir al sitio de País Mío</a><br>" +
                    "<p>Saludos!</p>";

                enviarCorreo(doOperario.correo,subject,body);

                return true;
            }

            return false;
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
            DAO_Operario DAOoperario = new DAO_Operario();
            if (DAOoperario.validarCorreo(correo)) {
                DAOoperario.generarToken(correo);

                string subject = "Recuperación de contraseña";
                string url = "https://spepaismio.tk/Admin/Recovery.aspx?token=" + DAOoperario.generarToken(correo);
                string body = "<p>¿Usted ha solicilitado la recuperación de contraseña?</p><br>" +
                "<p>De ser así, por favor haga click:" +
                "<a href='" + url + "'>Click aquí para continuar</a>" +
                "</p><br>" +
                "<p> Si usted no ha solicitado la recuperación de contraseña, ignore este correo</p>";

                enviarCorreo(correo,subject,body);
            
            }
        }

        public void enviarNuevaContrasena(string token)
        {
            DAO_Operario DAOoperario = new DAO_Operario();
            DO_Operario operario = DAOoperario.confirmacionContrasena(token);

            if (!operario.correo.Equals(""))
            {
                string subject = "Recuperación de contraseña";
                string url = "https://pais-mio-industria-artesanal.web.app/";
                string body = "<p>Su nueva contraseña es: " + operario.contrasena + "</p><br>" +
                "<p>:" +
                "<a href='" + url + "'>Click aquí para continuar</a>" +
                "</p><br>" +
                "<p> Gracias</p>";

                enviarCorreo(operario.correo, subject, body);
               
            }
        }

        public DO_Operario login(string correo, string pass) {

            DAO_Operario DAOoperario = new DAO_Operario();
            return DAOoperario.login(correo,pass);
        }

        public void enviarCorreo(string correoDestino, string subject, string body)
        {
            string correoOrigen = "passcontrolSPE@gmail.com";
            string contrasena = "Pepito123.";

            MailMessage message = new MailMessage(correoOrigen, correoDestino, subject,
                body);
            message.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(correoOrigen, contrasena);
            try
            {
                smtpClient.Send(message);
                smtpClient.Dispose();
            }
            catch (Exception ex) {
                Console.WriteLine("Excepción: "+ex);
            }
        }
        public bool upgradeRol(DO_Operario usuario, string rol)
        {

            DAO_Operario ope = new DAO_Operario();
            if (rol.Equals("SUPERVISOR"))
            {
                return ope.upOpToSup(usuario);
            }
            if (rol.Equals("ADMINISTRADOR"))
            {

                if (ope.upOpToSup(usuario))
                {
                    ope.upSupToAdm(usuario);
                }

            }
            return false;
        }

        public bool modificarUsuario(DO_Operario operario)
        {
            DAO_Operario daoOperario = new DAO_Operario();

            return daoOperario.modificarUsuario(operario);
        }

        /// <summary>
        /// Método para cambiar contraseña
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="newPass"></param>
        /// <param name="oldPass"></param>
        /// <returns></returns>
        public bool cambiarContraseña(string correo, string newPass, string oldPass) {
            DAO_Operario daoOperario = new DAO_Operario();
            return daoOperario.cambiarContrasena(correo, newPass, oldPass);
        }

    }
}
