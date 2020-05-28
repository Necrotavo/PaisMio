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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Cliente" in both code and config file together.
    [ServiceContract]
    public interface IWS_Cliente
    {


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "Modificar")]
        bool modificarCliente(DO_Cliente cliente);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "ListarClientesHabilitados")]
        List<DO_Cliente> listaClientesHabilitados();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "ListarClientes")]
        List<DO_Cliente> listaClientes();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "Buscar")]
        DO_Cliente buscarCliente(String cedula);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST", UriTemplate = "ModificarEstado")]
        bool modificarEstado(String estado, String cedula);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method ="POST", UriTemplate ="Agregar")]
        bool agregarCliente(DO_Cliente doCliente);

    }
}
