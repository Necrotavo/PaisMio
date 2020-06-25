import { Injectable } from '@angular/core';

import { HttpHeaders, HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

import { User } from '../models/user';
import { Input } from '../models/input';
import { InputRequest } from '../models/inputRequest';
import { Analysis } from '../models/analysis';
import { Product } from '../models/product';
import { InputReport } from '../models/inputReport';
import { Client } from '../models/client';
import { Order } from '../models/order';
import { Unit } from '../models/unit';
import { InputQ } from 'src/models/inputQ';
import { Cellar } from 'src/models/cellar';
import { CellarAdmin } from 'src/models/cellarAdmin';
import { MoveInput } from 'src/models/moveInput';
import { InputRequestDesicion } from 'src/models/inputRequestDecision';
import { UserRolUpgrade } from 'src/models/userRolUpgrade';
import { LoginUser } from 'src/models/loginUser';
import { AnalysisPC } from 'src/models/analysisPC';
import { OrderReport } from 'src/models/orderReport';
import { InputComparativeReport } from 'src/models/inputComparativeReport';

const HttpOptions = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};

/*** Api URL constants */
/** Base API URL */
const apiURL = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientes';

/** Client API URLs */
const clientPOST = 'https://www.spepaismio.tk/WS_Cliente.svc/Agregar';
const clientGET = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientes';
const clientAGET = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientesHabilitados';
const clientUPDATE = 'https://www.spepaismio.tk/WS_Cliente.svc/Modificar';
/** Falta Client */
const clientSTATUS = 'https://www.spepaismio.tk/WS_Cliente.svc/ModificarEstado';
const clientSEARCH = 'https://www.spepaismio.tk/WS_Cliente.svc/Buscar';

/** User API URLs */
const userPOST = 'https://spepaismio.tk/WS_Usuario.svc/CrearOperario';
const userGET = 'https://www.spepaismio.tk/WS_Usuario.svc/Lista';
const userLoginPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/Login';
const userUpdatePOST = 'https://www.spepaismio.tk/WS_Usuario.svc/modificarUsuario';
/** Falta User */
const passwordRecoveryPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/RecuperarContrasena';
const generatePasswordPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/GenerarPass';
const searchUserPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/Consultar';
const modifyStatePOST = 'https://www.spepaismio.tk/WS_Usuario.svc/modificarEstado';
const upgradeSupervisorRolPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/supervisorRolUpgrade';
const upgradeOperatorRolPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/operarioRolUpgrade';

/** Input API URLs */
const inputPOST = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarInsumo';
const inputGET = 'https://www.spepaismio.tk/WS_Insumo.svc/obtenerListaInsumos';
const inputSEARCH = 'https://www.spepaismio.tk/WS_Insumo.svc/buscarInsumo';
const inputUPDATE = 'https://www.spepaismio.tk/WS_Insumo.svc/modificarInsumo';
const inputGetA = 'https://www.spepaismio.tk/WS_Insumo.svc/obtenerListaInsumosHabilitados';


/** InputQ API URLs */
const inputQPOST = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarInsumo';
const inputQGET = 'https://www.spepaismio.tk/WS_Insumo.svc/obtenerListaInsumosHabilitados';
const inputQUPDATE = 'https://www.spepaismio.tk/WS_Insumo.svc/modificarInsumo';
const inputQSEARCH = 'https://www.spepaismio.tk/WS_Insumo.svc/buscarInsumo';

/** Units Request URLs */
const unitPOST = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarUnidad';
const unitGET = 'https://www.spepaismio.tk/WS_Insumo.svc/listarUnidades';

/** Input Request URLs */
const inputRequestPost = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/ingresoSolicitud';
const inputRequestDESICION = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/decisionAdmin';
const inputRequestGET = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/listarSolicitudes';
const inputRequestGETBYUSER = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/solicitudPorOperario';
const inputRequestGETBYORDER = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/solicitudPorPedido';
const inputRequestSEARCH = 'https://www.spepaismio.tk/WS_SolicitudInsumo.svc/solicitudSingular';

/** Order API URLs */
const orderPOST = 'https://www.spepaismio.tk/WS_Pedido.svc/agregarPedido';
const orderGET = 'https://www.spepaismio.tk/WS_Pedido.svc/listarPedidos';
const orderGETtotalList = 'https://www.spepaismio.tk/WS_Pedido.svc/listarPedidosTotales';
/** Falta Order */
const orderUPDATE = 'https://www.spepaismio.tk/WS_Pedido.svc/Modificar';
const orderDELETE = 'https://www.spepaismio.tk/WS_Pedido.svc/Eliminar';
const orderSEARCH = 'https://www.spepaismio.tk/WS_Pedido.svc/Consultar';
const orderPACKOFF = 'https://www.spepaismio.tk/WS_Pedido.svc/Despachar';

/** Falta Analysis API URLs */
const analysisPost = 'https://www.spepaismio.tk/WS_Pedido.svc/AgregarAnalisisAA';
const analysisAASEARCH = 'https://www.spepaismio.tk/WS_Pedido.svc/BuscarAnalisisAA';
const analysisPQSEARCH = 'https://www.spepaismio.tk/WS_Pedido.svc/AnalisisFQs';

/** Product API URLs */
const productPost = 'https://www.spepaismio.tk/WS_Producto.svc/ingresarProducto';
const productGET = 'https://www.spepaismio.tk/WS_Producto.svc/listaProductos';
/** Falta producto */
const productGetA = 'https://www.spepaismio.tk/WS_Producto.svc/listaProductosHabilitados';
const productSEARCH = 'https://www.spepaismio.tk/WS_Producto.svc/buscarProducto';
const productUPDATE = 'https://www.spepaismio.tk/WS_Producto.svc/modificarProducto';

/** Reports API URLs */
const inputReportPOST = 'https://www.spepaismio.tk/WS_Reporte.svc/reporteInsumos';
const comparativeInputReportPOST = 'https://www.spepaismio.tk/WS_Reporte.svc/reporteInsumosComparativo';
const orderReportPOST = 'https://www.spepaismio.tk/WS_Reporte.svc/reportePedidos';



/** Cellar API URLs */
const cellarGET = 'https://www.spepaismio.tk/WS_Bodega.svc/obtenerListaBodegas';
const cellarAGET = 'https://www.spepaismio.tk/WS_Bodega.svc/obtenerListaBodegasHabilitados';
const cellarInputPUT = 'https://www.spepaismio.tk/WS_Bodega.svc/entradaInsumos';
const cellarPOST = 'https://www.spepaismio.tk/WS_Bodega.svc/registrarBodega';
const cellarUPDATE = 'https://www.spepaismio.tk/WS_Bodega.svc/modificarBodega';
const cellarSTATUS = 'https://www.spepaismio.tk/WS_Bodega.svc/cambiarEstadoBodega';
const cellarGetOne = 'https://www.spepaismio.tk/WS_Bodega.svc/obtenerBodega';
const cellarGetInputList = 'https://www.spepaismio.tk/WS_Bodega.svc/obtenerInsumosBodega';
const cellarMoveInput = 'https://www.spepaismio.tk/WS_Bodega.svc/moverInsumoDeBodega';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleErrors<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  /** Order CRUD */

  getOrder(): Observable<Order[]> {
    return this.http.get<Order[]>(`${orderGET}`)
      .pipe(
        tap(order => console.log('fetch order')),
        catchError(this.handleErrors(`getOrder`, []))
      );
  }

  getTOrder(): Observable<Order[]> {
    return this.http.get<Order[]>(`${orderGETtotalList}`)
      .pipe(
        tap(order => console.log('fetch order')),
        catchError(this.handleErrors(`getTOrder`, []))
      );
  }

  getOrderByClient(client: string): Observable<User> {
    const url = `${apiURL}/${client}`;
    return this.http.get<User>(url).pipe(
      tap(_ => console.log(`fetch order id=${client}`)),
      catchError(this.handleErrors<User>(`getOrderByClient id=${client}`))
    );
  }

  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(orderPOST, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  updateOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(orderUPDATE, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  deleteOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(orderDELETE, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  searchOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(orderSEARCH, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  packOff(order: Order): Observable<Order> {
    return this.http.post<Order>(orderPACKOFF, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  /** Users CRUD */
  getUser(): Observable<User[]> {
    return this.http.get<User[]>(`${userGET}`)
      .pipe(
        tap(user => console.log('fetch user')),
        catchError(this.handleErrors(`getUser`, []))
      );
  }

  getUserByEmail(email: string): Observable<User> {
    const url = `${apiURL}/${email}`;
    return this.http.get<User>(url).pipe(
      tap(_ => console.log(`fetch user id=${email}`)),
      catchError(this.handleErrors<User>(`getUserByEmail id=${email}`))
    );
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(userPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  passwordRecovery(user: User): Observable<User> {
    return this.http.post<User>(passwordRecoveryPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  generatePassword(user: User): Observable<User> {
    return this.http.post<User>(generatePasswordPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  searchUser(user: User): Observable<User> {
    return this.http.post<User>(searchUserPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  modifyStateUser(user: User): Observable<User> {
    return this.http.post<User>(modifyStatePOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  upgradeSupervisorRol(user: User): Observable<User> {
    return this.http.post<User>(upgradeSupervisorRolPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  upgradeOperatorRol(user: User): Observable<User> {
    return this.http.post<User>(upgradeOperatorRolPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  updateUser(user: User): Observable<User> {
    return this.http.post(userUpdatePOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`update user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`updateUser`))
    );
  }

  userLogin(user: LoginUser): Observable<User> {
    return this.http.post<User>(userLoginPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  deleteUser(id: string): Observable<User> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<User>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted user id=${id}`)),
      catchError(this.handleErrors<User>(`deletedUser`))
    );
  }

  /** Inputs CRUD */

  getInput(): Observable<Input[]> {
    return this.http.get<Input[]>(`${inputGET}`)
      .pipe(
        tap(input => console.log(`fetch input`)),
        catchError(this.handleErrors(`getInput`, []))
      );
  }
  getInputA(): Observable<Input[]> {
    return this.http.get<Input[]>(`${inputGetA}`)
      .pipe(
        tap(input => console.log(`fetch input`)),
        catchError(this.handleErrors(`getInput`, []))
      );
  }

  getInputByName(nombre: string): Observable<Input> {
    const url = `${inputSEARCH}/${nombre}`;
    return this.http.get<Input>(url).pipe(
      tap(_ => console.log(`fetch input id=${nombre}`)),
      catchError(this.handleErrors<Input>(`getInputByID id=${nombre}`))
    );
  }

  addInput(input: Input): Observable<Input> {
    return this.http.post<Input>(inputPOST, input, HttpOptions).pipe(
      tap((i: Input) => console.log(`added input w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Input>(`addInput`))
    );
  }

  updateInput(input: Input): Observable<Input> {
    return this.http.post<Input>(inputUPDATE, input, HttpOptions).pipe(
      tap((i: Input) => console.log(`added user w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Input>(`addUser`))
    );
  }

  deleteInput(id: string): Observable<Input> {
    const url = `${inputGET}/${id}`;
    return this.http.delete<Input>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted input id=${id}`)),
      catchError(this.handleErrors<Input>(`deletedInput`))
    );
  }

  /** Inputs quantity CRUD */
  getInputQ(): Observable<InputQ[]> {
    return this.http.get<InputQ[]>(`${inputQGET}`)
      .pipe(
        tap(inputQ => console.log(`fetch inputQ`)),
        catchError(this.handleErrors(`getInputQ`, []))
      );
  }

  getInputQByName(nombre: string): Observable<InputQ> {
    const url = `${inputQSEARCH}/${nombre}`;
    return this.http.get<InputQ>(url).pipe(
      tap(_ => console.log(`fetch input id=${nombre}`)),
      catchError(this.handleErrors<InputQ>(`getInputQByID id=${nombre}`))
    );
  }

  addInputQ(inputQ: InputQ): Observable<InputQ> {
    return this.http.post<InputQ>(inputQPOST, inputQ, HttpOptions).pipe(
      tap((i: InputQ) => console.log(`added input w/ id=${i.insumo.nombre}`)),
      catchError(this.handleErrors<InputQ>(`addInput`))
    );
  }

  updateInputQ(cantidad: number, inputQ: InputQ): Observable<any> {
    const url = `${inputQUPDATE}/${cantidad}`;
    return this.http.put(url, inputQ, HttpOptions).pipe(
      tap(_ => console.log(`updated input id=${cantidad}`)),
      catchError(this.handleErrors<any>(`updateInput`))
    );
  }

  deleteInputQ(id: string): Observable<Input> {
    const url = `${inputQGET}/${id}`;
    return this.http.delete<Input>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted input id=${id}`)),
      catchError(this.handleErrors<Input>(`deletedInput`))
    );
  }

  /** Units CRUD */
  getUnits(): Observable<Unit[]> {
    return this.http.get<Unit[]>(`${unitGET}`)
      .pipe(
        tap(_ => console.log(`fetch unit`)),
        catchError(this.handleErrors(`getUnit`, []))
      );
  }

  addUnit(unit: Unit): Observable<Unit> {
    return this.http.post<Input>(unitPOST, unit, HttpOptions).pipe(
      tap((i: Unit) => console.log(`added unit w/ id=${i.unidad}`)),
      catchError(this.handleErrors<Input>(`addUnit`))
    );
  }

  /** Input Requests CRUD */
  getInputRequest(): Observable<InputRequest[]> {
    return this.http.get<InputRequest[]>(`${inputRequestGET}`)
      .pipe(
        tap(inputRequest => console.log(`fetch input request`)),
        catchError(this.handleErrors(`getInputRequest`, []))
      );
  }

  getInputRequestByID(inputRequest: InputRequest): Observable<InputRequest> {
    return this.http.post<InputRequest>(inputRequestSEARCH, inputRequest, HttpOptions).pipe(
      tap((i: InputRequest) => console.log(`searched input request w/ id=${i.codigo}`)),
      catchError(this.handleErrors<InputRequest>(`getInputRequestByID`))
    );
  }

  getInputRequestByUser(user: User): Observable<InputRequest> {
    return this.http.post<InputRequest>(inputRequestGETBYUSER, user, HttpOptions).pipe(
      tap((i: InputRequest) => console.log(`searched input request w/ id=${i.codigo} by user`)),
      catchError(this.handleErrors<InputRequest>(`getInputRequestByUser`))
    );
  }

  getInputRequestByOrder(order: Order): Observable<InputRequest[]> {
    return this.http.post<InputRequest[]>(inputRequestGETBYORDER, order, HttpOptions).pipe(
      tap((i: InputRequest[]) => console.log(`searched input request w/ id=${i} by order`)),
      catchError(this.handleErrors<InputRequest[]>(`getInputRequestByOrder`))
    );
  }

  addInputRequest(inputRequest: InputRequest): Observable<InputRequest> {
    return this.http.post<InputRequest>(inputRequestPost, inputRequest, HttpOptions).pipe(
      tap((i: InputRequest) => console.log(`added input request w/ id=${i.codigo}`)),
      catchError(this.handleErrors<InputRequest>(`addInputRequest`))
    );
  }

  setInputRequestDecision(inputRequestDesicion: InputRequestDesicion): Observable<InputRequestDesicion> {
    return this.http.post<InputRequestDesicion>(inputRequestDESICION, inputRequestDesicion, HttpOptions).pipe(
      tap((i: InputRequestDesicion) => console.log(`searched input request w/ id=${i.solicitud} by order`)),
      catchError(this.handleErrors<InputRequestDesicion>(`getInputRequestByOrder`))
    );
  }

  /** Products CRUD */
  getProduct(): Observable<Product[]> {
    return this.http.get<Product[]>(`${productGET}`)
      .pipe(
        tap(product => console.log(`fetch producct`)),
        catchError(this.handleErrors(`getProdut`, []))
      );
  }

  getProductA(): Observable<Product[]> {
    return this.http.get<Product[]>(`${productGetA}`)
      .pipe(
        tap(product => console.log(`fetch producct`)),
        catchError(this.handleErrors(`getProdut`, []))
      );
  }

  searchProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(productSEARCH, product, HttpOptions).pipe(
      tap((i: Product) => console.log(`added product w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Product>(`addProduct`))
    );
  }

  getProductByID(id: string): Observable<Product> {
    const url = `${apiURL}/${id}`;
    return this.http.get<Product>(url).pipe(
      tap(_ => console.log(`fetch product id=${id}`)),
      catchError(this.handleErrors<Product>(`getProductByID id=${id}`))
    );
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(productPost, product, HttpOptions).pipe(
      tap((i: Product) => console.log(`added product w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Product>(`addProduct`))
    );
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(productUPDATE, product, HttpOptions).pipe(
      tap((i: Product) => console.log(`added product w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Product>(`addProduct`))
    );
  }

  deleteProduct(id: string): Observable<Product> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<Product>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted product id=${id}`)),
      catchError(this.handleErrors<Product>(`deletedProduct`))
    );
  }

  /** Analysis CRUD  */
  getAnalysis(): Observable<Analysis[]> {
    return this.http.get<Analysis[]>(`${apiURL}`)
      .pipe(
        tap(analysis => console.log(`fetch analysis`)),
        catchError(this.handleErrors(`getAnalysis`, []))
      );
  }

  getAnalysisByID(pedCodigo: number): Observable<Analysis> {
    console.log(`CODIGO PEDIDO ${pedCodigo}`);
    return this.http.post<Analysis>(analysisAASEARCH, pedCodigo, HttpOptions).pipe(
      tap((i: Analysis) => console.log(`added analysis w/ id=${i.pedCodigo}`)),
      catchError(this.handleErrors<Analysis>(`addAnalysis`))
    );
  }
  getPQAnalsis(): Observable<AnalysisPC[]> {
    return this.http.get<AnalysisPC[]>(`${analysisPQSEARCH}`)
      .pipe(
        tap(analysisPC => console.log(`fetch analysysPC`)),
        catchError(this.handleErrors(`getAnalysisPC`, []))
      );
  }
  addAnalysis(analysis: Analysis): Observable<Analysis> {
    console.log(`AQUI analysis w/ id=${analysis.pedCodigo}`);
    return this.http.post<Analysis>(analysisPost, analysis, HttpOptions).pipe(
      tap((i: Analysis) => console.log(`added analysis w/ id=${i.pedCodigo}`)),
      catchError(this.handleErrors<Analysis>(`addAnalysis`))
    );
  }

  updateAnalysis(id: string, analysis: Analysis): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, analysis, HttpOptions).pipe(
      tap(_ => console.log(`updated analysis id=${id}`)),
      catchError(this.handleErrors<any>(`updateAnalysis`))
    );
  }

  deleteAnalysis(id: string): Observable<Analysis> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<Analysis>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted analysis id=${id}`)),
      catchError(this.handleErrors<Analysis>(`deletedAnalysis`))
    );
  }

  /** Client CRUD */
  getClient(): Observable<Client[]> {
    return this.http.get<Client[]>(`${clientGET}`)
      .pipe(
        tap(user => console.log('fetch client')),
        catchError(this.handleErrors(`getClient`, []))
      );
  }

  getAClient(): Observable<Client[]> {
    return this.http.get<Client[]>(`${clientAGET}`)
      .pipe(
        tap(user => console.log('fetch client')),
        catchError(this.handleErrors(`getClient`, []))
      );
  }

  getClientByID(cedula: string): Observable<Client> {
    const url = `${clientSEARCH}/${cedula}`;
    return this.http.get<Client>(url).pipe(
      tap(_ => console.log(`fetch client id=${cedula}`)),
      catchError(this.handleErrors<Client>(`getClientByEmail id=${cedula}`))
    );
  }

  addClient(client: Client): Observable<Client> {
    return this.http.post<Client>(clientPOST, client, HttpOptions).pipe(
      tap((i: Client) => console.log(`added client w/ id=${i.cedula}`)),
      catchError(this.handleErrors<Client>(`addClient`))
    );
  }

  searchClient(client: Client): Observable<Client> {
    return this.http.post<Client>(clientSEARCH, client, HttpOptions).pipe(
      tap((i: Client) => console.log(`added client w/ id=${i.cedula}`)),
      catchError(this.handleErrors<Client>(`addClient`))
    );
  }

  updateClient(client: Client): Observable<Client> {
    return this.http.post<Client>(clientUPDATE, client, HttpOptions).pipe(
      tap((i: Client) => console.log(`updated client w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Client>(`addUser`))
    );
  }

  updateClientStatus(client: Client): Observable<Client> {
    return this.http.post<Client>(clientSTATUS, client, HttpOptions).pipe(
      tap((i: Client) => console.log(`added client w/ id=${i.nombre}`)),
      catchError(this.handleErrors<Client>(`addUser`))
    );
  }

  deleteClient(nombre: string): Observable<Client> {
    const url = `${apiURL}/${nombre}`;
    return this.http.delete<Client>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted client id=${nombre}`)),
      catchError(this.handleErrors<Client>(`deletedClient`))
    );
  }

  /** Cellar CRUD */
  getCellar(): Observable<Cellar[]> {
    return this.http.get<Cellar[]>(`${cellarGET}`)
      .pipe(
        tap(cellar => console.log('fetch Cellar')),
        catchError(this.handleErrors(`getCellar`, []))
      );
  }

  getACellar(): Observable<Cellar[]> {
    return this.http.get<Cellar[]>(`${cellarAGET}`)
      .pipe(
        tap(user => console.log('fetch cellar')),
        catchError(this.handleErrors(`getCellar`, []))
      );
  }

  getOneCellar(code: number): Observable<Cellar> {
    return this.http.post<Cellar>(cellarGetOne, code, HttpOptions).pipe(
      tap((i: Cellar) => console.log(`The cellar w/ id=${i} has returned`)),
      catchError(this.handleErrors<Cellar>(`getOneCellar`))
    );
  }

  getCellarInputList(cellar: Cellar): Observable<Cellar> {
    return this.http.post<Cellar>(cellarGetInputList, cellar, HttpOptions).pipe(
      tap((i: Cellar) => console.log(`The input list of the cellar w/ id=${i} has returned`)),
      catchError(this.handleErrors<Cellar>(`getCellarInputList`))
    );
  }

  updateCellar(cellar: Cellar): Observable<Cellar> {
    return this.http.post<Cellar>(cellarUPDATE, cellar, HttpOptions).pipe(
      tap((i: Cellar) => console.log(`updated cellar w/ id=${i}`)),
      catchError(this.handleErrors<Cellar>(`updateCellar`))
    );
  }

  /** Complex post */
  inputEntry(cellar: CellarAdmin): Observable<CellarAdmin> {
    return this.http.post<CellarAdmin>(cellarInputPUT, cellar, HttpOptions).pipe(
      tap((i: CellarAdmin) => console.log(`The input list has been added to the cellar w/ id=${i}`)),
      catchError(this.handleErrors<CellarAdmin>(`cellarInputPut`))
    );
  }

  /** Complex post */
  cellarMoveInput(moveInput: MoveInput): Observable<MoveInput> {
    return this.http.post<MoveInput>(cellarMoveInput, moveInput, HttpOptions).pipe(
      tap((i: MoveInput) => console.log(`The input has been moved to the cellar w/ id=${i}`)),
      catchError(this.handleErrors<MoveInput>(`cellarMoveInput`))
    );
  }

  cellarStatus(cellar: Cellar): Observable<any> {
    return this.http.post(cellarSTATUS, cellar, HttpOptions).pipe(
      tap((i: any) => console.log(`The status has been updated to the cellar w/ id=${i}`)),
      catchError(this.handleErrors<Cellar>(`cellarStatus`))
    );
  }

  addCellar(cellar: Cellar): Observable<Cellar> {
    return this.http.post<Cellar>(cellarPOST, cellar, HttpOptions).pipe(
      tap((i: Cellar) => console.log(`added cellar w/ id=${i}`)),
      catchError(this.handleErrors<Cellar>(`addCellar`))
    );
  }

    /** Reports CRUD */
    getInputReport(input: InputReport): Observable<InputReport> {
      return this.http.post<InputReport>(inputReportPOST, input, HttpOptions).pipe(
        tap((i: InputReport) => console.log(`added client w/ id=${i.fechaFinal}`)),
        catchError(this.handleErrors<InputReport>(`getInputReport`))
      );
    }

    getOrderReport(orderReport: OrderReport): Observable<OrderReport> {
      return this.http.post<OrderReport>(orderReportPOST, orderReport, HttpOptions).pipe(
        tap((i: OrderReport) => console.log(`id=${i}`)),
        catchError(this.handleErrors<OrderReport>(`getOrderReport`))
      );
    }

    getInputComparativeReport(getInputComparativeReport: InputComparativeReport): Observable<InputComparativeReport> {
      return this.http.post<InputComparativeReport>(orderReportPOST, getInputComparativeReport, HttpOptions).pipe(
        tap((i: InputComparativeReport) => console.log(`reporte=${i}`)),
        catchError(this.handleErrors<InputComparativeReport>(`getOrderReport`))
      );
    }

}




/** TODO */
/* const inputUPDATE = 'https://www.spepaismio.tk/WS_Insumo.svc/modificarInsumo';
const inputGetA = 'https://www.spepaismio.tk/WS_Insumo.svc/obtenerListaInsumosHabilitados';
const unitGet = 'https://www.spepaismio.tk/WS_Insumo.svc/listarUnidades';
const unitAdd = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarUnidades';
* /


  /* updateClientStatus(nombre: string, client: Client): Observable<any> {
     const url = `${apiURL}/${nombre}`;
     return this.http.put(url, client, HttpOptions).pipe(
       tap(_ => console.log(`updated client status id=${nombre}`)),
       catchError(this.handleErrors<any>(`updateClientStatus`))
     );
   } */

  /*


  getReportCByID(id: string): Observable<ReportC> {
    const url = `${apiURL}/${id}`;
    return this.http.get<ReportC>(url).pipe(
      tap(_ => console.log(`fetch report comparative id=${id}`)),
      catchError(this.handleErrors<ReportC>(`getReportCByID id=${id}`))
    );
  }

  addReportC(reportC: ReportC): Observable<ReportM> {
    return this.http.post<ReportC>(apiURL, reportC, HttpOptions).pipe(
      tap((i: ReportC) => console.log(`added reporth comparative w/ id=${i.id}`)),
      catchError(this.handleErrors<ReportC>(`addReportC`))
    );
  }

  updateReportC(id: string, reportC: ReportC): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, reportC, HttpOptions).pipe(
      tap(_ => console.log(`updated report comparative id=${id}`)),
      catchError(this.handleErrors<any>(`updateReportC`))
    );
  }

  deleteReportC(id: string): Observable<ReportC> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<ReportC>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted report comparative id=${id}`)),
      catchError(this.handleErrors<ReportC>(`deletedReportC`))
    );
  }
   */

    /* Monthly Reports CRUD
  getReportM(): Observable<ReportM[]> {
    return this.http.get<ReportM[]>(`${apiURL}`)
      .pipe(
        tap(reportM => console.log(`fetch report monthly`)),
        catchError(this.handleErrors(`getReportM`, []))
      );
  }

  getReportMByID(id: string): Observable<ReportM> {
    const url = `${apiURL}/${id}`;
    return this.http.get<ReportM>(url).pipe(
      tap(_ => console.log(`fetch report monthly id=${id}`)),
      catchError(this.handleErrors<ReportM>(`getReportMByID id=${id}`))
    );
  }

  addReportM(reportM: ReportM): Observable<ReportM> {
    return this.http.post<ReportM>(apiURL, reportM, HttpOptions).pipe(
      tap((i: ReportM) => console.log(`added reporth monthly w/ id=${i.id}`)),
      catchError(this.handleErrors<ReportM>(`addReportM`))
    );
  }

  updateReportM(id: string, reportM: ReportM): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, reportM, HttpOptions).pipe(
      tap(_ => console.log(`updated report Monthly id=${id}`)),
      catchError(this.handleErrors<any>(`updateReportM`))
    );
  }

  deleteReportM(id: string): Observable<ReportM> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<ReportM>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted report Monthly id=${id}`)),
      catchError(this.handleErrors<ReportM>(`deletedReportM`))
    );
  } */
