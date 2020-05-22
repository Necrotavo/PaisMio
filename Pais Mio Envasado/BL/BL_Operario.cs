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
    /// Esta clase llama a los métodos del DAO_Operario
    /// </summary>
    public class BL_Operario
    {
        public bool agregarOperario(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {

            DAO_Operario DAOoperario = new DAO_Operario();

            return DAOoperario.agregarOperario(correo, estado, nombre, apellidos, contrasena);
        }
    }
}
