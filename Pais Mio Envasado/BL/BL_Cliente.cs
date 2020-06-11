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
    /// En esta clase se encuentra la logica de negocio de los clientes
    /// </summary>
    public class BL_Cliente
    {
        private DO_Cliente cliente;

        public BL_Cliente(DO_Cliente cliente)
        {
            this.cliente = cliente;
        }

        public BL_Cliente()
        {
        }

        /// <summary>
        /// Envía un cliente a la capa de datos para ser almacenado en la base de datos.
        /// </summary>
        /// <param name="nuevoCliente">El cliente ha agregar</param>
        /// <returns>(True) si el cliente se registró. (False)si el cliente no se registró</returns>
        public bool agregarCliente (DO_Cliente nuevoCliente)
        {
            DAO_Cliente daoCliente = new DAO_Cliente();
            return daoCliente.agregarCliente(nuevoCliente);
        }

        /// <summary>
        /// Envía un cliente con datos modificados para que se la capa de datos lo actualice
        /// </summary>
        /// <param name="cliente">Clientes con los datos a modificar</param>
        /// <returns>(True) si se registró el cambio. (False)si el cambio no se registró</returns>
        public bool modificarCliente (DO_Cliente cliente)
        {
            DAO_Cliente dao_Cliente = new DAO_Cliente();
            return dao_Cliente.modificarCliente(cliente);
        }

        /// <summary>
        /// Muestra la lista de los lientes habilitados en el sistema.
        /// </summary>
        /// <returns>Lista de los clientes habilitados(List<DO_Cliente>)</returns>
        public List<DO_Cliente> mostrarClientesHabilitados()
        {
            DAO_Cliente daoCliente = new DAO_Cliente();
            return daoCliente.listarClientesHabilitados();
        }

        /// <summary>
        /// Muestra la lista con todos los clientes registrados en el sistema.
        /// </summary>
        /// <returns>Lista de los clientes habilitados(List<DO_Cliente>)</returns>
        public List<DO_Cliente> mostrarClientes()
        {
            DAO_Cliente daoCliente = new DAO_Cliente();
            return daoCliente.listarTodosLosClientes();
        }

        /// <summary>
        /// Muestra los datos de un cliente específico.
        /// </summary>
        /// <param name="cedula">Cdula del cliente a buscar</param>
        /// <returns>Cliente con sus respectivos datos(DO_Cliente).(Null) si no se encontro ninguna coincidencia.</returns>
        public DO_Cliente buscarCliente(String cedula)
        {
            DAO_Cliente daoCliente = new DAO_Cliente();
            return daoCliente.buscarCliente(cedula);
        }

        /// <summary>
        /// Permite modificar el estado actual del cliente.
        /// </summary>
        /// <param name="estado">El nuevo estado (String)</param>
        /// <param name="cedula">La cédula del cliente (String)</param>
        /// <returns>(True) si se actualizó el estado.(False)si no se actualizó</returns>
        public bool modificarEstado(String estado, String cedula)
        {
            DAO_Cliente daoCliente = new DAO_Cliente();
            return daoCliente.modificarEstado(estado, cedula);
        }
    }
}
