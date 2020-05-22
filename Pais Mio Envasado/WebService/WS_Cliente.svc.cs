using DO;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Cliente" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Cliente.svc or WS_Cliente.svc.cs at the Solution Explorer and start debugging.
    public class WS_Cliente : IWS_Cliente
    {
        /// <summary>
        /// WS para agregar clientes nuevos al sistema.
        /// </summary>
        /// <param name="cedula">Cédula del cliente (String)</param>
        /// <param name="estado">Estado del cliente (String)</param>
        /// <param name="nombre">Nombre del cliente (String)</param>
        /// <param name="telefono">Telefono del cliente (String)</param>
        /// <param name="correo">Correo del cliente (String)</param>
        /// <param name="direccion">Dirección del cliente (String)</param>
        /// <returns>(True) si se registró el cliente correctamente. (False) si no se registró</returns>
        public bool agregarCliente(String cedula, String estado, String nombre, String telefono, String correo, String direccion)
        {          
            DO_Cliente doCliente = new DO_Cliente(cedula,estado,nombre,telefono,correo,direccion);
            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.agregarCliente(doCliente);
        }
        /// <summary>
        /// WS para buscar clientes en el sistema.
        /// </summary>
        /// <param name="nombre">El nombre del cliente a buscar</param>
        /// <returns>El cliente con sus respectivos datos (DO_Cliente)</returns>
        public DO_Cliente buscarCliente(String nombre)
        {
            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.buscarCliente(nombre);
        }
        /// <summary>
        /// WS para listar los clientes totales en el sistema.
        /// </summary>
        /// <returns>Lista de clientes activos e inactivos (List<DO_Cliente>)</returns>
        public List<DO_Cliente> listaClientes()
        {
            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.mostrarClientes();
        }
        /// <summary>
        /// WS para listar los clientes activos en el sistema.
        /// </summary>
        /// <returns>Lista de clientes activos (List<DO_Cliente>)</returns>
        public List<DO_Cliente> listaClientesHabilitados()
        {

            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.mostrarClientesHabilitados();
        }
        /// <summary>
        /// WS para modificar los datos de un cliente.
        /// </summary>
        /// <param name="cedula">Cédula del cliente (String)</param>
        /// <param name="estado">Estado del cliente (String)</param>
        /// <param name="nombre">Nombre del cliente (String)</param>
        /// <param name="telefono">Telefono del cliente (String)</param>
        /// <param name="correo">Correo del cliente (String)</param>
        /// <param name="direccion">Dirección del cliente (String)</param>
        /// <returns>(True) si se modificar los datos. (False) si ocurrió algún error.</returns>
        public bool modificarCliente(String cedula, String estado, String nombre, String telefono, String correo, String direccion)
        {
           
            DO_Cliente doCliente = new DO_Cliente(cedula, estado, nombre, telefono, correo, direccion);
            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.modificarCliente(doCliente);
        }
        /// <summary>
        /// WS para modificar el estado actual del cliente.
        /// </summary>
        /// <param name="estado">El nuevo estado del cliente (String)</param>
        /// <param name="cedula">La cédula del cliente (String)</param>
        /// <returns>(True) si se modificó el estado. (False) si ocurrió algún error.</returns>
        public bool modificarEstado(String estado, String cedula)
        {
            BL_Cliente blCliente = new BL_Cliente();

            return blCliente.modificarEstado(estado, cedula);
        }
    }
}
