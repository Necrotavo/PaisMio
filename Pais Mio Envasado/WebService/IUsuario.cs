using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsuario" in both code and config file together.
    [ServiceContract]
    public interface IUsuario
    {
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> getLista();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string getNombre();

       
        /// <summary>
        ///
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="nombre"></param>
        /// <param name="apellidos"></param>
        /// <param name="contrasena"></param>
        /// <param name="tipo"></param>
        /// <returns> True si el usuario se insertó correctamente, false si no se insertó</returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Boolean crearUsuario(String correo,String nombre, String apellidos, String contrasena, String tipo);
    }
}
