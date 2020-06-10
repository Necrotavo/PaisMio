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
    /// Esta clase comprende el acceso y manejo a los datos correspondientes a los Operarios
    /// </summary>
    public class DAO_Operario
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private String queryInsertar = "";

        public DAO_Operario() {
            this.queryInsertar = "INSERT INTO OPERARIO (OPE_CORREO," + " EST_HAB_ESTADO,"
                + " OPE_NOMBRE,"
                + " OPE_APELLIDOS)"
                + "VALUES(@correo, @estado, @nombre, @apellidos)";
        }

        public String getQueryInsertar() {
            return this.queryInsertar;
        }

        public DO_Operario confirmacionContrasena(string token) {
            DO_Operario credenciales = new DO_Operario();

            credenciales.correo = validarToken(token);
            if (!credenciales.correo.Equals("")) {
                credenciales.contrasena = nuevaContrasena(credenciales.correo);
                eliminarToken(credenciales.correo);
            }
            return credenciales;
        }

        private void eliminarToken(string correo) {
            SqlCommand comando = new SqlCommand("UPDATE OPERARIO SET TOKEN = null WHERE OPE_CORREO = @correo", conexion);
            comando.Parameters.AddWithValue("@correo", correo);
           
            try
            {

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
     
            }
            finally
            {

                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        } 

        private string validarToken(string token) {
            SqlCommand comandoSelect = new SqlCommand("SELECT OPE_CORREO FROM OPERARIO WHERE TOKEN = @token", conexion);
            comandoSelect.Parameters.AddWithValue("@token", token);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                String exist = Convert.ToString(comandoSelect.ExecuteScalar());
 
                return exist;
                
            }
            catch (Exception)
            {

                return "";
            }
            finally
            {

                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        
        public string generarToken(string correo) {

            
            string token = Encrypt.GetSHA256(Guid.NewGuid().ToString());

            SqlCommand comando = new SqlCommand("UPDATE OPERARIO SET TOKEN = @token WHERE OPE_CORREO = @correo", conexion);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@token", token);
           
            try
            {
                
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();

                return token;
            }
            catch (Exception)
            {

                return "";
            }
            finally
            {

                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        public bool validarCorreo(string correo) {

            SqlCommand comandoSelect = new SqlCommand("SELECT OPE_CORREO FROM OPERARIO WHERE OPE_CORREO = @correo", conexion);
            comandoSelect.Parameters.AddWithValue("@correo", correo);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                String exist = Convert.ToString(comandoSelect.ExecuteScalar());

                if (!exist.Equals("")) {
                    return true;
                }
                
            }
            catch (Exception)
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
            return false;
        }

        /// <summary>
        /// Método que retorna la contraseña autogenerada
        /// </summary>
        /// <param name="correo"> correo de quien solicitó el cambio de contraseña</param>
        /// <returns> string con la contraseña autogenerada</returns>
        public string nuevaContrasena(string correo)
        {
            generarContrasena(correo);
            string newPass = getContrasena(correo);
            cambiarContrasenaAutogenerada(correo, newPass);
            return newPass;

        }

        /// <summary>
        /// Metodo que toma la oontraseña actual de un operario
        /// </summary>
        /// <param name="correo">Correo del operario</param>
        /// <returns></returns>
        private string getContrasena(string correo)
        {
            SqlCommand ejecutarProcedimiento = new SqlCommand("SELECT OPE_CONTRASENA FROM OPERARIO WHERE OPE_CORREO = @correo", conexion);
            ejecutarProcedimiento.Parameters.AddWithValue("@correo", correo);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                string contrasena = Convert.ToString(ejecutarProcedimiento.ExecuteScalar());

                return contrasena;
            }
            catch (Exception)
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
        /// Ejecuta el procedure de generar contraseña de la base de datos
        /// </summary>
        /// <param name="correo">correo del operario del cual se quiere generar una nueva contraseña</param>
        /// <returns></returns>
        private bool generarContrasena(string correo)
        {
            SqlCommand ejecutarProcedimiento = new SqlCommand("EXEC newPass @correo", conexion);
            ejecutarProcedimiento.Parameters.AddWithValue("@correo", correo);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                ejecutarProcedimiento.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
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
        /// Metodo para cambiar la contraseña de un operario
        /// </summary>
        /// <param name="correo">Correo del operario</param>
        /// <param name="contrasena">Contraseña nueva</param>
        /// <param name="contrasenaVieja">Contraseña vieja</param>
        /// <returns></returns>
        public bool cambiarContrasena(string correo, string contrasena, string contrasenaVieja)
        {
            SqlCommand comando = new SqlCommand("UPDATE OPERARIO SET OPE_CONTRASENA = @contrasena WHERE OPE_CORREO = @correo AND OPE_CONTRASENA = @oldPass", conexion);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@contrasena", Encrypt.GetSHA256(contrasena));
            comando.Parameters.AddWithValue("@oldPass", Encrypt.GetSHA256(contrasenaVieja));
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
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

        public bool cambiarContrasenaAutogenerada(string correo, string contrasena)
        {
            SqlCommand comando = new SqlCommand("UPDATE OPERARIO SET OPE_CONTRASENA = @contrasena WHERE OPE_CORREO = @correo", conexion);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@contrasena", Encrypt.GetSHA256(contrasena));
            
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
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
        /// Método encargado de insertar Operarios en la tabla OPERARIO de la base de datos
        /// </summary>
        /// <param name="correo"> correo del operario</param>
        /// <param name="estado"> estado del operario, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del operario</param>
        /// <param name="apellidos"> apellidos del operario</param>
        /// <param name="contrasena"> contrasena del operario</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public string agregarOperario(DO_Operario doOperario) {

            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION " + queryInsertar + " COMMIT", conexion);
      
            comandoInsertar.Parameters.AddWithValue("@correo", doOperario.correo);
            comandoInsertar.Parameters.AddWithValue("@estado", "HABILITADO");
            comandoInsertar.Parameters.AddWithValue("@nombre", doOperario.nombre);
            comandoInsertar.Parameters.AddWithValue("@apellidos", doOperario.apellidos);
            //comandoInsertar.Parameters.AddWithValue("@contrasena", Encrypt.GetSHA256(doOperario.contrasena));

            try {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comandoInsertar.ExecuteNonQuery();

                
                ///fALTA MANDAR LA CONTRASENA AUTOGENERADA AL CORREO DEL USUARIO
                return nuevaContrasena(doOperario.correo); ;
            }
            catch (Exception)
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

        public DO_Operario buscarOperario(String correo) {
            DO_Operario operario = new DO_Operario();

            try
            {
                SqlCommand comandoSelect = new SqlCommand("Select * from OPERARIO where OPE_CORREO = @correo", conexion);
                comandoSelect.Parameters.AddWithValue("@correo",correo);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoSelect.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        operario.correo = (String)lector["OPE_CORREO"];
                        operario.estado = (String)lector["EST_HAB_ESTADO"];
                        operario.nombre = (String)lector["OPE_NOMBRE"];
                        operario.apellidos = (String)lector["OPE_APELLIDOS"];
                        operario.contrasena = (String)lector["OPE_CONTRASENA"];
                    }
                    conexion.Close();
                    operario.rol = getRol(operario.correo);
                    return operario;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return operario;
        } 
        private string getRol(string correo)
        {
            string rol = "";
            try
            {
                SqlCommand comandoSelect = new SqlCommand("" +
                    "SELECT OPERARIO.OPE_CORREO as 'Operario', SUPERVISOR.OPE_CORREO as 'Supervisor', ADMINISTRADOR.OPE_CORREO as 'Administrador' " +
                    "FROM OPERARIO " +
                    "left join SUPERVISOR " +
                    "ON OPERARIO.OPE_CORREO = SUPERVISOR.OPE_CORREO " +
                    "left join ADMINISTRADOR " +
                    "ON OPERARIO.OPE_CORREO = ADMINISTRADOR.OPE_CORREO " +
                    "WHERE OPERARIO.OPE_CORREO = @correo", conexion);
                comandoSelect.Parameters.AddWithValue("@correo", correo);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoSelect.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        if (lector["Administrador"] is DBNull)
                        {
                            if (lector["Supervisor"] is DBNull)
                            {
                                rol = "OPERARIO";
                                break;
                            }
                            else
                            {
                                rol = "SUPERVISOR";
                                break;
                            }
                        }
                        else
                        {
                            rol = "ADMINISTRADOR";
                            break;
                        }
                    }

                }
                return rol;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        public List<DO_Operario> obtenerListaOperarios()
        {
            List<DO_Operario> lista = new List<DO_Operario>();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = new SqlCommand("Select * from OPERARIO",conexion);
            DataTable datatable = new DataTable();
            adaptador.Fill(datatable);

            foreach (DataRow row in datatable.Rows)
            {
                DO_Operario operario = new DO_Operario();
                operario.correo = (String)row["OPE_CORREO"];
                operario.estado = (String)row["EST_HAB_ESTADO"];
                operario.nombre = (String)row["OPE_NOMBRE"];
                operario.apellidos = (String)row["OPE_APELLIDOS"];
                operario.contrasena = (String)row["OPE_CONTRASENA"];
                lista.Add(operario);
            }

            return lista;
        }

        /// <summary>
        /// Método para cambiar el estado de un determinado usuario.
        /// </summary>
        /// <param name="estado">Nuevo estado del usuario(String)</param>
        /// <param name="correo">Correo del uusario a modificar(String)</param>
        /// <returns>(True) si se modificó correctamente. (False) si no se modificó.</returns>
        public bool modificarEstado(String estado, String correo)
        {
            SqlCommand comandoActualizar = new SqlCommand("UPDATE OPERARIO SET EST_HAB_ESTADO = @nuevoEstado WHERE OPE_CORREO = @correo", conexion);
            comandoActualizar.Parameters.AddWithValue("@nuevoEstado", estado );
            comandoActualizar.Parameters.AddWithValue("@correo", correo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoActualizar.ExecuteNonQuery() > 0)
                {
                    return true;
                }
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

            return false;

        }

        
    }
}
