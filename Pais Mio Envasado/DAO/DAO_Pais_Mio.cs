using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DO;
namespace DAO
{
    /// <summary>
    /// Clase de acceso a datos de la información de País Mío.
    /// </summary>
    public class DAO_Pais_Mio
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ProductionConnection);

        /// <summary>
        /// Método para agregar los datos de país mío a la base de datos.
        /// </summary>
        /// <param name="datosPaisMio">Datos de País Mío a agregar</param>
        /// <returns>(True)si se registraron los cambios.(False)si se produjo un error.</returns>
        public bool agregarDatos(DO_PaisMio datosPaisMio)
        {
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO INFO_PAIS_MIO (IPM_NOMBRE, IPM_CEDULA_JURIDICA,IPM_CORREO," +
                "IPM_TELEFONO,IPM_DIRECCION,IPM_LOGO) VALUES (@nombre, @cedula,@correo,@telefono,@direccion,@logo)",conexion);

            comandoInsertar.Parameters.AddWithValue("@nombre", datosPaisMio.nombre);
            comandoInsertar.Parameters.AddWithValue("@cedula", datosPaisMio.cedula);
            comandoInsertar.Parameters.AddWithValue("@correo", datosPaisMio.correo);
            comandoInsertar.Parameters.AddWithValue("@telefono", datosPaisMio.telefono);
            comandoInsertar.Parameters.AddWithValue("@direccion", datosPaisMio.direccion);
            comandoInsertar.Parameters.AddWithValue("@logo", datosPaisMio.logo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comandoInsertar.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Método para consultar los datos de País Mío.
        /// </summary>
        /// <returns>Datos de Páis Mío (DO_PaisMio)</returns>
        public DO_PaisMio obtenerDatos()
        {
            DO_PaisMio paisMio = new DO_PaisMio();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM INFO_PAIS_MIO", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoBuscar.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {

                       paisMio.codigo = Convert.ToInt32(lector["IPM_CODIGO"]);
                       paisMio.nombre = (String)lector["IPM_NOMBRE"];
                       paisMio.cedula = (String)lector["IPM_CEDULA_JURIDICA"];
                       paisMio.correo = (String)lector["IPM_CORREO"];
                       paisMio.telefono = (String)lector["IPM_TELEFONO"];
                       paisMio.direccion = (String)lector["IPM_DIRECCION"];
                       paisMio.logo = (String)lector["IPM_LOGO"];



                    }
                }

                return paisMio;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Método para modificar los datos de País Mío.
        /// </summary>
        /// <param name="datosPaisMio">(DO_PaisMio) Datos de la empresa a modificar.</param>
        /// <returns>(True) si se modificaron los datos. (False) si no se modificaron.</returns>
        public bool modificarDatos(DO_PaisMio datosPaisMio)
        {
            SqlCommand comandoModificar = new SqlCommand("UPDATE INFO_PAIS_MIO SET " +
                "IPM_NOMBRE = @nombre " +
                ",IPM_CEDULA_JURIDICA = @cedula " +
                ",IPM_CORREO = @correo " +
                ",IPM_TELEFONO = @telefono " +
                ",IPM_DIRECCION = @direccion " +
                ",IPM_LOGO = @logo", conexion);

            comandoModificar.Parameters.AddWithValue("@nombre", datosPaisMio.nombre);
            comandoModificar.Parameters.AddWithValue("@cedula", datosPaisMio.cedula);
            comandoModificar.Parameters.AddWithValue("@correo", datosPaisMio.correo);
            comandoModificar.Parameters.AddWithValue("@telefono", datosPaisMio.telefono);
            comandoModificar.Parameters.AddWithValue("@direccion", datosPaisMio.direccion);
            comandoModificar.Parameters.AddWithValue("@logo", datosPaisMio.logo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoModificar.ExecuteNonQuery() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

        }

    }
}
