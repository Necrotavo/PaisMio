using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class BL_SolicitudInsumos
    {
        /// <summary>
        /// Metodo para crear una nueva solicitud de insumos.
        /// </summary>
        /// <param name="nuevaSolicitud">La solicitud de insumos</param>
        /// <returns></returns>
        public bool crearNuevaSolicitud(DO_SolicitudInsumos nuevaSolicitud)
        {
            DAO_SolicitudInsumos dao_Solicitud = new DAO_SolicitudInsumos();
            return dao_Solicitud.guardarSolicitudInsumos(nuevaSolicitud);

        }
        /// <summary>
        /// Metodo para decidir si rechazar o aceptar la solicitud.
        /// </summary>
        /// <param name="solicitud">La solicitud de insumos</param>
        /// <param name="admin">El encargado de tomar la decisión</param>
        /// <param name="estado">El estado al cual pasa la solicitud (aceptada o rechazada)</param>
        /// <returns></returns>
        public bool decisionAdmin(DO_SolicitudInsumos solicitud, DO_Administrador admin, string estado)
        {
            DAO_SolicitudInsumos dao_Solicitud = new DAO_SolicitudInsumos();
            if (dao_Solicitud.decisionSolicitud(admin,estado,solicitud))
            {
                return (dao_Solicitud.reducirInsumos(solicitud));
            }
            else
            {
                return false;
            }
        }
    }
}
