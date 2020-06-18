using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using DO;

namespace WebService
{
    /// <summary>
    /// Interfaz para los métodos correspondientes a los usuarios
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Usuario" in both code and config file together.
    [ServiceContract]
    public interface IWS_Usuario
    {


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST", UriTemplate = "Login")]
        DO_Operario login(DO_Operario usuario);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST", UriTemplate = "RecuperarContrasena")]
        void recuperarContrasena(string correo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST", UriTemplate = "GenerarPass")]
        bool generarContrasena(string correo);

        /// <summary>
        /// Método para crear usuarios, ya sea Operario, Supervisor o Administrador
        /// </summary>
        /// <param name="usuario">Objeto usuario</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "POST", 
            UriTemplate = "CrearOperario")]
        bool crearUsuario(DO_Operario usuario);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "POST", 
            UriTemplate = "Consultar")]
        DO_Operario consultarUsuario(string correo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "GET", 
            UriTemplate = "Lista")]
        List<DO_Operario> obtenerListaOperario();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "POST", 
            UriTemplate = "modificarEstado")]
        bool modificarEstado(DO_Operario doUsuario);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "operarioRolUpgrade")]
        bool opeRolUpgrade(DO_OpeRolUpgradeUsuario rolUpgrade);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            UriTemplate = "supervisorRolUpgrade")]
        bool supRolUpgrade(DO_OpeRolUpgradeUsuario rolUpgrade);
    }
}
