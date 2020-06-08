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
            BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "POST", UriTemplate = "RecuperarContrasena")]
        void recuperarContrasena(string correo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            Method = "POST", UriTemplate = "GenerarPass")]
        bool generarContrasena(string correo);

        //[OperationContract]
        //[WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate ="Crear")]
        //bool crearUsuarioP(string tipoUsuario, string correo, string estado, string nombre, string apellidos, string contrasena);

        /// <summary>
        /// Método para crear usuarios, ya sea Operario, Supervisor o Administrador
        /// </summary>
        /// <param name="usuario">Objeto usuario</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, 
            Method = "POST", 
            UriTemplate = "CrearOperario")]
        bool crearUsuario(DO_Operario usuario);

        //[OperationContract]
        //[WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", UriTemplate = "CrearOperario2")]
        //bool crearUsuario2(DO_Operario usuario);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, 
            Method = "POST", 
            UriTemplate = "Consultar")]
        DO_Operario consultarUsuario(String correo);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, 
            Method = "GET", 
            UriTemplate = "Lista")]
        List<DO_Operario> obtenerListaOperario();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            Method = "POST", 
            UriTemplate = "ListaOperarios")]
        List<DO_Operario> recibirLista(List<DO_Operario> lista);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            Method = "POST", 
            UriTemplate = "modificarEstado")]
        bool modificarEstado(string correo, String estado);
    }
}
