﻿using System;
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
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "Modificar")]
        bool modificarCliente(DO_Cliente cliente);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET", UriTemplate = "ListarClientesHabilitados")]
        List<DO_Cliente> listaClientesHabilitados();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "GET", UriTemplate = "ListarClientes")]
        List<DO_Cliente> listaClientes();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "Buscar")]
        DO_Cliente buscarCliente(DO_Cliente doCliente);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Bare, 
        Method = "POST", UriTemplate = "ModificarEstado")]
        bool modificarEstado(DO_Cliente doCliente);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, 
        Method ="POST", UriTemplate ="Agregar")]
        bool agregarCliente(DO_Cliente doCliente);

    }
}
