import { Injectable } from '@angular/core';

import { HttpHeaders, HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

import { User } from '../models/user';
import { Input } from '../models/input';
import { InputRequest } from '../models/inputRequest';
import { Analysis } from '../models/analysis';
import { Product } from '../models/product';
import { ReportC } from '../models/reportC';
import { ReportM } from '../models/reportM';
import { Client } from '../models/client';
import { Order } from '../models/order';
import { Unit } from '../models/unit';


const HttpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
};

/*** Api URL constants */
/** Base API URL */
const apiURL = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientes';

/** Client API URLs */
const clientPOST = 'https://www.spepaismio.tk/WS_Cliente.svc/Agregar';
const clientGET = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientes';
const clientAGET = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientesHabilitados';
const clientUPDATE = 'https://www.spepaismio.tk/WS_Cliente.svc/Modificar';
const clientSTATUS = 'https://www.spepaismio.tk/WS_Cliente.svc/ModificarEstado';

/** User API URLs */
const userPOST = 'https://spepaismio.tk/WS_Usuario.svc/CrearOperario';
const userGET = 'https://www.spepaismio.tk/WS_Usuario.svc/Lista';

/** Input API URLs */
const inputPOST = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarInsumo';
const inputGET = 'https://www.spepaismio.tk/WS_Insumo.svc/obtenerListaInsumos';
const inputUPDATE = 'https://www.spepaismio.tk/WS_Insumo.svc/modificarInsumo';
const inputSEARCH = 'https://www.spepaismio.tk/WS_Insumo.svc/buscarInsumo';

const unitPOST = 'https://www.spepaismio.tk/WS_Insumo.svc/agregarUnidad';
const unitGET = 'https://www.spepaismio.tk/WS_Insumo.svc/listarUnidades';

const inputRequestPost = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';
const inputRequestGET = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';

/** Order API URLs */
const orderPost = 'https://www.spepaismio.tk/WS_Pedido.svc/agregarPedido';
const orderGET = 'https://www.spepaismio.tk/WS_Pedido.svc/listarPedidos';
const orderUPDATE = 'https://www.spepaismio.tk/WS_Pedido.svc/Modificar';
const orderDELETE = 'https://www.spepaismio.tk/WS_Pedido.svc/Eliminar';
const orderSEARCH = 'https://www.spepaismio.tk/WS_Pedido.svc/Consultar';
const orderPACKOFF = 'https://www.spepaismio.tk/WS_Pedido.svc/Despachar';


/** Product API URLs */
const productPost = 'https://www.spepaismio.tk/WS_Producto.svc/ingresarProducto';
const productGET = 'https://www.spepaismio.tk/WS_Producto.svc/listaProductos';

/** Reports API URLs */
const mReportPost = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';
const mReportGET = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';
const cReportPost = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';
const cReportGET = 'http://spepaismio-001-site1.itempurl.com/WS_Cliente.svc/listarClientes';

/** Analysis API URLs */
const analysisPost = 'https://www.spepaismio.tk/WS_Pedido.svc/AgregarAnalisisAA';
const analysisAASEARCH = 'https://www.spepaismio.tk/WS_Pedido.svc/BuscarAnalisisAA';
const analysisGET = 'https://www.spepaismio.tk/WS_Pedido.svc/Consultar';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleErrors<T>(operation = 'operation', result?: T){
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

  getOrderByClient(client: string): Observable<User> {
    const url = `${apiURL}/${client}`;
    return this.http.get<User>(url).pipe(
      tap(_ => console.log(`fetch order id=${client}`)),
      catchError(this.handleErrors<User>(`getOrderByClient id=${client}`))
    );
  }

  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(apiURL, order, HttpOptions).pipe(
      tap((i: Order) => console.log(`added order w/ id=${i.codigo}`)),
      catchError(this.handleErrors<Order>(`addOrder`))
    );
  }

  updateOrder(id: string, order: Order): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, order, HttpOptions).pipe(
      tap(_ => console.log(`updated order id=${id}`)),
      catchError(this.handleErrors<any>(`updateOrder`))
    );
  }

  deleteOrder(id: string): Observable<Order> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<Order>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted order id=${id}`)),
      catchError(this.handleErrors<Order>(`deletedOrder`))
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

  updateUser(correo: string, user: User): Observable<any> {
    const url = `${apiURL}/${correo}`;
    return this.http.put(url, user, HttpOptions).pipe(
      tap(_ => console.log(`updated user id=${correo}`)),
      catchError(this.handleErrors<any>(`updateUser`))
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

  updateInput(nombre: string, input: Input): Observable<any> {
    const url = `${clientUPDATE}/${nombre}`;
    return this.http.put(url, input, HttpOptions).pipe(
      tap(_ => console.log(`updated input id=${nombre}`)),
      catchError(this.handleErrors<any>(`updateInput`))
    );
  }

  deleteInput(id: string): Observable<Input> {
    const url = `${inputGET}/${id}`;
    return this.http.delete<Input>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted input id=${id}`)),
      catchError(this.handleErrors<Input>(`deletedInput`))
    );
  }

  /** Units CRUD */
  getUnits(): Observable<Unit[]> {
    return this.http.get<Unit[]>(`${unitGET}`)
    .pipe(
      tap(input => console.log(`fetch unit`)),
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
    return this.http.get<InputRequest[]>(`${inputGET}`)
    .pipe(
      tap(inputRequest => console.log(`fetch input request`)),
      catchError(this.handleErrors(`getInputRequest`, []))
    );
  }

  getInputRequestByID(id: string): Observable<InputRequest> {
    const url = `${apiURL}/${id}`;
    return this.http.get<InputRequest>(url).pipe(
      tap(_ => console.log(`fetch input request id=${id}`)),
      catchError(this.handleErrors<InputRequest>(`getInputRequestByID id=${id}`))
    );
  }

  addInputRequest(input: InputRequest): Observable<InputRequest> {
    return this.http.post<InputRequest>(apiURL, input, HttpOptions).pipe(
      tap((i: InputRequest) => console.log(`added input request w/ id=${i.codigo}`)),
      catchError(this.handleErrors<InputRequest>(`addInputRequest`))
    );
  }

  updateInputRequest(id: string, inputRequest: InputRequest): Observable<any> {
    const url = `${inputUPDATE}/${id}`;
    return this.http.put(url, inputRequest, HttpOptions).pipe(
      tap(_ => console.log(`updated input request id=${id}`)),
      catchError(this.handleErrors<any>(`updateInputRequest`))
    );
  }

  deleteInputRequest(id: string): Observable<InputRequest> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<InputRequest>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted input request id=${id}`)),
      catchError(this.handleErrors<InputRequest>(`deletedInputRequest`))
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

  updateProduct(id: string, product: Product): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, product, HttpOptions).pipe(
      tap(_ => console.log(`updated product id=${id}`)),
      catchError(this.handleErrors<any>(`updateProduct`))
    );
  }

  deleteProduct(id: string): Observable<Product> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<Product>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted product id=${id}`)),
      catchError(this.handleErrors<Product>(`deletedProduct`))
    );
  }

    /** Analysis CRUD */
    getAnalysis(): Observable<Analysis[]> {
      return this.http.get<Analysis[]>(`${apiURL}`)
      .pipe(
        tap(analysis => console.log(`fetch analysis`)),
        catchError(this.handleErrors(`getAnalysis`, []))
      );
    }

    getAnalysisByID(id: string): Observable<Analysis> {
      const url = `${apiURL}/${id}`;
      return this.http.get<Analysis>(url).pipe(
        tap(_ => console.log(`fetch analysis id=${id}`)),
        catchError(this.handleErrors<Analysis>(`getAnalysisByID id=${id}`))
      );
    }

    addAnalysis(analysis: Analysis): Observable<Analysis> {
      return this.http.post<Analysis>(apiURL, analysis, HttpOptions).pipe(
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

    /** Monthly Reports CRUD */
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
    }

    /** Comparative Reports CRUD */
    getReportC(): Observable<ReportC[]> {
      return this.http.get<ReportC[]>(`${apiURL}`)
      .pipe(
        tap(reportC => console.log(`fetch report comparative`)),
        catchError(this.handleErrors(`getReportC`, []))
      );
    }

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

  getClientByEmail(email: string): Observable<Client> {
    const url = `${apiURL}/${email}`;
    return this.http.get<Client>(url).pipe(
      tap(_ => console.log(`fetch client id=${email}`)),
      catchError(this.handleErrors<Client>(`getClientByEmail id=${email}`))
    );
  }

  addClient(client: Client): Observable<Client> {
    return this.http.post<Client>(clientPOST, client, HttpOptions).pipe(
      tap((i: Client) => console.log(`added client w/ id=${i.cedula}`)),
      catchError(this.handleErrors<Client>(`addClient`))
    );
  }

  updateClient(nombre: string, client: Client): Observable<any> {
    const url = `${apiURL}/${nombre}`;
    return this.http.put(url, client, HttpOptions).pipe(
      tap(_ => console.log(`updated client id=${nombre}`)),
      catchError(this.handleErrors<any>(`updateClient`))
    );
  }

  updateClientStatus(nombre: string, client: Client): Observable<any> {
    const url = `${apiURL}/${nombre}`;
    return this.http.put(url, client, HttpOptions).pipe(
      tap(_ => console.log(`updated client status id=${nombre}`)),
      catchError(this.handleErrors<any>(`updateClientStatus`))
    );
  }

  deleteClient(nombre: string): Observable<Client> {
    const url = `${apiURL}/${nombre}`;
    return this.http.delete<Client>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted client id=${nombre}`)),
      catchError(this.handleErrors<Client>(`deletedClient`))
    );
  }

}
