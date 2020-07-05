using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DO;
using BL;

namespace WebService
{
    /// <summary>
    /// Clase que implementa los métodos de la interfaz IWS_Usuario.cs
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Usuario.svc or WS_Usuario.svc.cs at the Solution Explorer and start debugging.
    public class WS_Usuario : IWS_Usuario
    {
        public DO_Operario consultarUsuario(string correo)
        {
            BL_Operario BLoperario = new BL_Operario();
            return BLoperario.buscarOperario(correo);
        }

        /// <summary>
        /// Método para crear usuarios, ya sea Operario, Supervisor o Administrador
        /// </summary>
        /// <param name="usuario">Objeto usuario <param>
        /// <param name="tipo"> tipo de usuario</param>
        /// <returns></returns>
        public bool crearUsuario(DO_Operario usuario)
        {

            if (usuario.correo is null || usuario.nombre is null || usuario.apellidos is null
                || usuario.correo.Equals("") || usuario.nombre.Equals("") || usuario.apellidos.Equals(""))
            {
                return false;
            }

            if (usuario.rol.Equals("OPERARIO")) {

                BL_Operario BLoperario = new BL_Operario();
                return BLoperario.agregarOperario(usuario);
            }

            if (usuario.rol.Equals("SUPERVISOR"))
            {
                BL_Supervisor BLsupervisor = new BL_Supervisor();
                return BLsupervisor.agregarSupervisor(usuario);
            }

            if (usuario.rol.Equals("ADMINISTRADOR"))
            {
                BL_Administrador BLadministrador = new BL_Administrador();
                return BLadministrador.agregarAdministrador(usuario);
            }
            return false;
        }

        public List<DO_Operario> obtenerListaOperario()
        {
            BL_Operario BLoperario = new BL_Operario();
            return BLoperario.obtenerListaOperario();
        }

        public List<DO_Operario> recibirLista(List<DO_Operario> lista)
        {

            return lista;
        }

        /// <summary>
        /// Método para modificar el estado de un usuario
        /// </summary>
        /// <param name="correo">(String) Correo del usuario a modificar</param>
        /// <param name="estado">(String) Nuevo estado del usuario</param>
        /// <returns></returns>
        public bool modificarEstado(DO_Operario doUsuario)
        {
            BL_Operario blOperario = new BL_Operario();
            return blOperario.modificarEstadoUsuario(doUsuario.correo, doUsuario.estado);
        }

        public bool generarContrasena(string correo)
        {
            BL_Operario blOperario = new BL_Operario();
           String newPass = blOperario.generarContrasena(correo);
            return true;
        }

        public string recuperarContrasena(DO_Operario usuario)
        {
            BL_Operario BLoperario = new BL_Operario();
            BLoperario.recuperacionContrasena(usuario.correo);
            return usuario.correo;
        }

        public bool opeRolUpgrade(DO_OpeRolUpgradeUsuario rolUpgrade)
        {
            BL_Operario ope = new BL_Operario();
            return ope.upgradeRol(rolUpgrade.usuario, rolUpgrade.rolNuevo);
        }

        public bool supRolUpgrade(DO_OpeRolUpgradeUsuario rolUpgrade)
        {
            BL_Supervisor sup = new BL_Supervisor();
            return sup.upgradeRol(rolUpgrade.usuario, rolUpgrade.rolNuevo);
        }

        public DO_Operario login(DO_Operario doUsuario)
        {
            if (doUsuario.correo.Trim().Equals("") || doUsuario.contrasena.Trim().Equals("")) {
                return null;
            }

            BL_Operario BLoperario = new BL_Operario();

            return BLoperario.login(doUsuario.correo, doUsuario.contrasena);
        }

        public bool modificarUsuario(DO_Operario doUsuario)
        {
            BL_Operario BLoperario = new BL_Operario();

            return BLoperario.modificarUsuario(doUsuario);
        }

        public bool cambiarContrasena(string correo, string newPass, string oldPass)
        {
            BL_Operario BLoperario = new BL_Operario();
            return BLoperario.cambiarContraseña(correo, newPass, oldPass);
        }
    }
}
