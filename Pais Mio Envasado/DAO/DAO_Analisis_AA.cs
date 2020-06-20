using DO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAO
{
    /// <summary>
    /// Esta clase permite el acceso a base de datos relacionado al análisis de aguardiente
    /// </summary>
    public class DAO_Analisis_AA
    {
        
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private String queryInsertar = "";
        public DAO_Analisis_AA() {
            this.queryInsertar = "INSERT INTO ANALISIS_AGUARDIENTE " +
              "(ANA_ARMONIA_SENSORIAL, ANA_EXAMEN_GUSTATIVO, ANA_EXAMEN_OLFATIVO, ANA_EXAMEN_VISUAL, ANA_FECHA_EMISION, " +
              "ANA_FECHA_VIGENCIA, ANA_NOMBRE_PRODUCTO,ANA_NOTAS,IPM_CODIGO, PED_CODIGO) " +
              "VALUES ( @armoniaS, @examenG, @examenO, @examenV, @fechaEmision, " +
              "@fechaVigencia, @nombreProducto, @notas, (select top 1 IPM_CODIGO from INFO_PAIS_MIO), @pedCodigo) ";
        }

        public String getQueryInsertar() {
            return queryInsertar;
        }

        /// <summary>
        /// Método para consruir un insert múltiple para los análisis fisico químicos
        /// </summary>
        /// <param name="analisisAA"> objeto DO_Analisis_AA que contiene una lista de análisis fisicoquímicos</param>
        /// <returns> string con el insert múltiple</returns>
        public String analisisFQconstructor(DO_Analisis_AA analisisAA)
        {

            String insertAnalisisFQs = "";

            if (analisisAA.analisisFQs.Count == 0)
            {
                return insertAnalisisFQs;
            }
            else
            {
                insertAnalisisFQs = "INSERT INTO ANALISIS_FISICOQUIMICO " +
                    "(PED_CODIGO, TAF_TIPO_ANALISIS_FQ, AFQ_MEDICION_RESULTADO, AFQ_UNIDAD_CONDICION) VALUES ";
            }

             
            foreach (DO_Analisis_FQ analisisFQ in analisisAA.analisisFQs) {
                insertAnalisisFQs += "(" +analisisFQ.pedCodigo+", " + "'"+analisisFQ.tipoAnalisisFQ+"'" + ", " + "'"+analisisFQ.medicionResultado+"'" + ", " + "'"+analisisFQ.unidadCondicion+"'" + "),";
            }

            return insertAnalisisFQs.Substring(0, insertAnalisisFQs.Length - 1);
        }

        /// <summary>
        /// Método para insertar Análisis de aguardiente
        /// </summary>
        /// <param name="analisisAA"> objeto DO_Análisis_AA que contiene toda la información de un análisis de aguardiente </param>
        /// <returns> true si se ejecuta la transacción correctamente, false si ocurre algún error</returns>
        public bool agregarAnalisisAA(DO_Analisis_AA analisisAA) {
          
            String queryInsertarCompleto = "BEGIN TRANSACTION BEGIN TRY " + queryInsertar + analisisFQconstructor(analisisAA) + " COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH";
            //String queryInsertarCompleto = "BEGIN TRANSACTION " + analisisFQconstructor(analisisAA) + " COMMIT";
            //String queryInsertarCompleto = "BEGIN TRANSACTION " + queryInsertar  + " COMMIT";
            SqlCommand comandoInsertar = new SqlCommand(queryInsertarCompleto, conexion);

            comandoInsertar.Parameters.AddWithValue("@armoniaS", analisisAA.aSensorial);
            comandoInsertar.Parameters.AddWithValue("@examenG", analisisAA.exGustativo);
            comandoInsertar.Parameters.AddWithValue("@examenO", analisisAA.exOlfativo);
            comandoInsertar.Parameters.AddWithValue("@examenV", analisisAA.exVisual);
            comandoInsertar.Parameters.AddWithValue("@fechaEmision", System.DateTime.Now);
            comandoInsertar.Parameters.AddWithValue("@fechaVigencia", System.DateTime.Now.AddDays(100));
            comandoInsertar.Parameters.AddWithValue("@nombreProducto", analisisAA.nombreProducto);
            comandoInsertar.Parameters.AddWithValue("@notas", analisisAA.notas);
            comandoInsertar.Parameters.AddWithValue("@pedCodigo", analisisAA.pedCodigo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                Int32 rowsAffected = comandoInsertar.ExecuteNonQuery();

                if (rowsAffected < 2)
                {

                    return false;
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally {
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// Método para buscar Análisis de aguardiente por código
        /// </summary>
        /// <param name="pedCodigo"></param>
        /// <returns> objeto DO_Analisis_AA con la información de un análisis de aguardiente, null si no se encuentra</returns>
        public DO_Analisis_AA buscarAnalisisAAporPedCodigo(int pedCodigo) {

            String query = "SELECT ANA_ARMONIA_SENSORIAL,ANA_EXAMEN_GUSTATIVO,ANA_EXAMEN_OLFATIVO,"
                            +"ANA_EXAMEN_VISUAL,ANA_FECHA_EMISION,ANA_FECHA_VIGENCIA,ANA_NOMBRE_PRODUCTO,"
                            +"ANA_NOTAS,IPM_CODIGO FROM ANALISIS_AGUARDIENTE WHERE PED_CODIGO = @pedCodigo";
            SqlCommand comandoSelect = new SqlCommand(query,conexion);
            comandoSelect.Parameters.AddWithValue("@pedCodigo",pedCodigo);

            DO_Analisis_AA analisisAA = new DO_Analisis_AA();

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoSelect.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        analisisAA.aSensorial = Convert.ToInt32(lector["ANA_ARMONIA_SENSORIAL"]);
                        analisisAA.exGustativo = Convert.ToInt32(lector["ANA_EXAMEN_GUSTATIVO"]);
                        analisisAA.exOlfativo = Convert.ToInt32(lector["ANA_EXAMEN_OLFATIVO"]);
                        analisisAA.exVisual = Convert.ToInt32(lector["ANA_EXAMEN_VISUAL"]);
                        analisisAA.fechaEmision = Convert.ToDateTime(lector["ANA_FECHA_EMISION"]).ToString();
                        analisisAA.fechaVigencia = Convert.ToDateTime(lector["ANA_FECHA_VIGENCIA"]).ToString();
                        analisisAA.nombreProducto = (String)lector["ANA_NOMBRE_PRODUCTO"];
                        analisisAA.notas = (String)lector["ANA_NOTAS"];
                        analisisAA.ipmCodigo = Convert.ToInt32(lector["IPM_CODIGO"]);
                        analisisAA.pedCodigo = pedCodigo;
                       
                    }
                }

            } catch {
                return null;
            } finally {
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
            }
            analisisAA.analisisFQs = getListaAnalisisFQ(pedCodigo);
            return analisisAA;
        }

        /// <summary>
        /// Método para obtener los análisis fisicoquímicos pertenecientes a un Analisis de aguardiente
        /// </summary>
        /// <param name="pedCodigo"> código del pedido</param>
        /// <returns>Lista de los análisis de fisico químicos de un análisis de aguardiente</returns>
        public List<DO_Analisis_FQ> getListaAnalisisFQ(int pedCodigo) {
            List<DO_Analisis_FQ> lista = new List<DO_Analisis_FQ>();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("SELECT TAF_TIPO_ANALISIS_FQ, AFQ_MEDICION_RESULTADO, " +
                    "AFQ_UNIDAD_CONDICION FROM ANALISIS_FISICOQUIMICO WHERE PED_CODIGO = @pedCodigo", conexion);
                adapter.SelectCommand.Parameters.AddWithValue("@pedCodigo", pedCodigo);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    DO_Analisis_FQ analisisFQ = new DO_Analisis_FQ();
                    analisisFQ.pedCodigo = pedCodigo;
                    analisisFQ.tipoAnalisisFQ = (String)row["TAF_TIPO_ANALISIS_FQ"];
                    analisisFQ.medicionResultado = (String)row["AFQ_MEDICION_RESULTADO"];
                    analisisFQ.unidadCondicion = (String)row["AFQ_UNIDAD_CONDICION"];
                    lista.Add(analisisFQ);
                }
            }
            catch {
                return null;
            }

            return lista;
        }
    }
}
