using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace WebService
{
    /// <summary>
    /// Interfaz para los métodos correspondientes a los usuarios
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Usuario" in both code and config file together.
    [ServiceContract]
    public interface IWS_Usuario
    {

        /// <summary>
        /// Método para crear usuarios ya sea Operario, Supervisor o Administrador
        /// </summary>
        /// <param name="tipoUsuario"> tipo de usuario</param>
        /// <param name="correo"> correo del usuario</param>
        /// <param name="estado"> estado del usuario</param>
        /// <param name="nombre"> nombre del usuario</param>
        /// <param name="apellidos"> apellidos del usuario</param>
        /// <param name="contrasena"> contraseña del usuario</param>
        /// <returns>True si se agregó correctamente, false si no se agregó</returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool crearUsuario(string tipoUsuario,string correo, string estado, string nombre, string apellidos, string contrasena);
    }
}
