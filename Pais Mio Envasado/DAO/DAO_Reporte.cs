using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAO
{
    /// <summary>
    /// En esta clase se trabaja el acceso a base de datos para la creaci[on de reportes
    /// </summary>
    public class DAO_Reporte
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);
        
        public List<DO_InsumoReportable> reporteInsumos(String inicio, String final) {
            try
            {
                //Formato del string para la fecha 2020-05-30
                SqlDataAdapter adaptador = new SqlDataAdapter();
                DataTable datatable = new DataTable();
                List<DO_InsumoReportable> listaReportados = new List<DO_InsumoReportable>();

                adaptador.SelectCommand = new SqlCommand("SELECT * FROM (Select INS_CODIGO AS INS_CODIGO_CONSUMIR, SUM(ACS_CANTIDAD) AS TOTAL_CONSUMIDO from SOL_A_CONSUMIR_INS " +
                    "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
                    "ON SOL_A_CONSUMIR_INS.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO) AS CONSUMO " +
                    "FULL JOIN " +
                    "(Select INS_CODIGO AS INS_CODIGO_DESCARTE, SUM(PDS_CANTIDAD) AS TOTAL_DESCARTADO from POR_DESCARTE " +
                    "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
                    "ON POR_DESCARTE.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO) AS DESCARTE ON CONSUMO.INS_CODIGO_CONSUMIR = DESCARTE.INS_CODIGO_DESCARTE", conexion);

                adaptador.SelectCommand.Parameters.AddWithValue("@fechaInicio", inicio);
                adaptador.SelectCommand.Parameters.AddWithValue("@fechaFinal", final);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);

                foreach (DataRow fila in datatable.Rows)
                {
                    DO_InsumoReportable insumoConsumido = new DO_InsumoReportable();
                    insumoConsumido.insumo = new DO_Insumo();
                    Object codigoConsumir = fila["INS_CODIGO_CONSUMIR"];
                    if (!(codigoConsumir is DBNull))
                    {
                        insumoConsumido.insumo.codigo = Convert.ToInt32(codigoConsumir);
                    }
                    else {
                        insumoConsumido.insumo.codigo = Convert.ToInt32(fila["INS_CODIGO_DESCARTE"]);
                    }
                    Object cantConsumida = fila["TOTAL_CONSUMIDO"];
                    if (!(cantConsumida is DBNull))
                    {
                        insumoConsumido.cantidadConsumida = Convert.ToInt32(cantConsumida);
                    }
                    Object cantDescartada = fila["TOTAL_DESCARTADO"];
                    if (!(cantDescartada is DBNull))
                    {
                        insumoConsumido.cantidadDescartada = Convert.ToInt32(cantDescartada);
                    }

                    DAO_Insumo daoInsumo = new DAO_Insumo();
                    insumoConsumido.insumo = daoInsumo.buscarInsumoPorCódigo(insumoConsumido.insumo.codigo);
                    insumoConsumido.total = insumoConsumido.cantidadConsumida + insumoConsumido.cantidadDescartada;

                    listaReportados.Add(insumoConsumido);
                }
                return listaReportados;
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
        /// Método para obtener los pedidos despachados en un mes determinado.
        /// </summary>
        /// <param name="mes">Mes de los pedidos a buscar (int)</param>
        /// <param name="anho">Año del pedido a buscar(int)</param>
        /// <returns>Lista de pedidos en el mes especificado</returns>
        public DO_ReportePedido reportePedidos(String inicio, String final)
        {

            DO_ReportePedido reportePedido = new DO_ReportePedido();
            reportePedido.listaPedidos = new List<DO_Pedido>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT PEDIDO.PED_CODIGO, CLIENTE.CLI_NOMBRE, " +
                "PEDIDO.ESTADO, PEDIDO.OPE_CORREO,PEDIDO.ADM_OPE_CORREO," +
                "PEDIDO.PED_FECHA_INGRESO,PEDIDO.PED_FECHA_DESPACHO FROM PEDIDO, CLIENTE " +
                "WHERE PEDIDO.CLI_CEDULA = CLIENTE.CLI_CEDULA " +
                "AND PEDIDO.PED_FECHA_DESPACHO BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)", conexion);

            comandoBuscar.Parameters.AddWithValue("@fechaInicio", inicio);
            comandoBuscar.Parameters.AddWithValue("@fechaFinal", final);

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
                        DO_Pedido doPedido = new DO_Pedido();

                        doPedido.codigo = Convert.ToInt32(lector["PED_CODIGO"]);
                        doPedido.cliente = new DO_Cliente();
                        doPedido.cliente.nombre = (string)lector["CLI_NOMBRE"];
                        doPedido.correoAdminIngreso = (string)lector["OPE_CORREO"];
                        doPedido.correoAdminDespacho = (string)lector["ADM_OPE_CORREO"];
                        doPedido.fechaIngreso = (DateTime)lector["PED_FECHA_INGRESO"];
                        doPedido.fechaDespacho = (DateTime)lector["PED_FECHA_DESPACHO"];
                        doPedido.estado = (String)lector["ESTADO"];

                        reportePedido.listaPedidos.Add(doPedido);
                    }
                }
                
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
            DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
            reportePedido.infoPaisMio = daoPaisMio.obtenerDatos();

            obtenerProductos(reportePedido.listaPedidos); //Se envía la lista de pedidos al método encargado de asignar los respectivos productos con los pedidos.

            return reportePedido;
        }

        /// <summary>
        /// Método para obtener agregar los productos en la lista de pedidos de reporte
        /// </summary>
        /// <param name="listaPedidos"></param>
        public void obtenerProductos(List<DO_Pedido> listaPedidos)
        {
                      
            foreach (DO_Pedido pedido in listaPedidos)
            {
                SqlCommand comandoBuscarProductos = new SqlCommand("SELECT PRODUCTO.PRO_NOMBRE, PRODUCTO.PRO_CODIGO,PRODUCTO.PRO_DESCRIPCION,PRODUCTO.EST_HAB_ESTADO, PED_POSEE_PRO.PPP_CANTIDAD " +
                "FROM PED_POSEE_PRO,PRODUCTO WHERE PED_POSEE_PRO.PRO_CODIGO = PRODUCTO.PRO_CODIGO AND (PED_POSEE_PRO.PED_CODIGO = @codigoPedido)", conexion);
                comandoBuscarProductos.Parameters.AddWithValue("@codigoPedido", pedido.codigo);
                List<DO_ProductoEnPedido> listaProductos = new List<DO_ProductoEnPedido>();

                try
                {
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    SqlDataReader lector = comandoBuscarProductos.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            DO_ProductoEnPedido productoEnPedido = new DO_ProductoEnPedido();
                            DO_Producto producto = new DO_Producto();
                            

                            producto.codigo = Convert.ToInt32(lector["PRO_CODIGO"]);
                            producto.nombre = (String)lector["PRO_NOMBRE"];
                            producto.descripcion = (String)lector["PRO_DESCRIPCION"];
                            producto.estado = (String)lector["EST_HAB_ESTADO"];

                            productoEnPedido.producto = producto;
                            productoEnPedido.cantidad = Convert.ToInt32(lector["PPP_CANTIDAD"]);

                            listaProductos.Add(productoEnPedido);
                        }
                        pedido.listaProductos = listaProductos;
                    }
                }
                catch (SqlException){}
                finally
                {
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public DO_ReporteEntradaInsumos reporteEntradas(String inicio, String final) {
            try
            {
                SqlDataAdapter adaptador = new SqlDataAdapter();
                DataTable datatable = new DataTable();
                DO_ReporteEntradaInsumos reporteEntradas = new DO_ReporteEntradaInsumos();
                reporteEntradas.listaEntradas = new List<DO_EntradaReportable>();

                adaptador.SelectCommand = new SqlCommand("SELECT * FROM ENTRADA_INSUMO " +
                    "WHERE ENI_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)", conexion);

                adaptador.SelectCommand.Parameters.AddWithValue("@fechaInicio", inicio);
                adaptador.SelectCommand.Parameters.AddWithValue("@fechaFinal", final);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);

                DAO_Pais_Mio daoPaisMio = new DAO_Pais_Mio();
                reporteEntradas.infoPaisMio = daoPaisMio.obtenerDatos();

                foreach (DataRow fila in datatable.Rows)
                {
                    DO_EntradaReportable entradaInsumo = new DO_EntradaReportable();
                    entradaInsumo.listaInsumos = new List<DO_InsumoEntrante>();

                    entradaInsumo.codigo = Convert.ToInt32(fila["ENI_CODIGO"]);
                    entradaInsumo.fecha = Convert.ToString(fila["ENI_FECHA"]);
                    entradaInsumo.correoAdministrador = (String)(fila["OPE_CORREO"]);

                    entradaInsumo.listaInsumos = obtenerListaInsumosEntrante(entradaInsumo.codigo);

                    reporteEntradas.listaEntradas.Add(entradaInsumo);
                }
                return reporteEntradas;
            }
            catch(SqlException)
            {
                return null;
            }
            finally {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        private List<DO_InsumoEntrante> obtenerListaInsumosEntrante(Int32 codigoEntrada) {
            try
            {
                SqlDataAdapter adaptadorInsumos = new SqlDataAdapter();
                DataTable datatableInsumos = new DataTable();
                List<DO_InsumoEntrante> listaInsumos = new List<DO_InsumoEntrante>();

                adaptadorInsumos.SelectCommand = new SqlCommand("SELECT * FROM INSUMO_ENTRANTE WHERE ENI_CODIGO = @codigoEntrada", conexion);
                adaptadorInsumos.SelectCommand.Parameters.AddWithValue("@codigoEntrada", codigoEntrada);

                adaptadorInsumos.Fill(datatableInsumos);

                foreach (DataRow filaInsumos in datatableInsumos.Rows)
                {
                    DO_InsumoEntrante insumoEntrante = new DO_InsumoEntrante();
                    insumoEntrante.doBodega = new DO_Bodega();
                    insumoEntrante.insumo = new DO_InsumoEnBodega();
                    insumoEntrante.insumo.insumo = new DO_Insumo();

                    insumoEntrante.doBodega.codigo = Convert.ToInt32(filaInsumos["BOD_CODIGO"]);
                    DAO_Bodega daoBodega = new DAO_Bodega();
                    insumoEntrante.doBodega.nombre = daoBodega.obtenerNombreBodega(insumoEntrante.doBodega.codigo);

                    insumoEntrante.insumo.insumo.codigo = Convert.ToInt32(filaInsumos["INS_CODIGO"]);
                    DAO_Insumo daoInsumo = new DAO_Insumo();
                    insumoEntrante.insumo.insumo = daoInsumo.buscarInsumoPorCódigo(insumoEntrante.insumo.insumo.codigo);

                    insumoEntrante.insumo.cantidadDisponible = Convert.ToInt32(filaInsumos["IENT_CANTIDAD"]);

                    listaInsumos.Add(insumoEntrante);
                }
                return listaInsumos;
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
