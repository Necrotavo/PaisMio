import { Component, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';

import { ApiService } from '../api.service';
import { Client } from 'src/models/client';
import { User } from 'src/models/user';
import { Input } from 'src/models/input';
import { InputQ } from 'src/models/inputQ';
import { Product } from 'src/models/product';
import { Order } from 'src/models/order';
import { Unit } from 'src/models/unit';
import { ProductInOrder } from 'src/models/productInOrder';
import { Cellar } from 'src/models/cellar';
import { CellarAdmin } from 'src/models/cellarAdmin';
import { MoveInput } from 'src/models/moveInput';
import { InputRequest } from 'src/models/inputRequest';
import { InputRequestDesicion } from 'src/models/inputRequestDecision';
import { AnalysisPC } from 'src/models/analysisPC';
import { Analysis } from 'src/models/analysis';
import Swal from 'sweetalert2';
import { DataService } from '../data.service';


@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', display: 'none' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class AdminViewComponent implements OnInit {

  constructor(private apiService: ApiService, private data: DataService) { }

  /** Declarations for Post and Get web services */
  /** Object declarations */
  order: Order;
  input: Input;
  user: User;
  client: Client;
  product: Product;
  cellar: Cellar;
  moveInput: MoveInput;
  cellarAdmin: CellarAdmin;
  inputRequest: InputRequest;
  inputRequestDesicion: InputRequestDesicion;
  clientOrder: Client;

  /** Object Lists */
  orderList: Order[];
  orderTlist: Order[];
  inputList: Input[];
  userList: Array<User> = [];
  clientList: Client[];
  clientAList: Client[];
  productList: Product[];
  productAList: Product[];
  unitList: Unit[];
  productInOrderList: ProductInOrder[];
  inputQList: InputQ[];
  inputQListDiscard: InputQ[];
  cellarList: Cellar[];
  cellarList2: Cellar[];
  inputRequestList: InputRequest[];
  productEntryList: Array<ProductInOrder> = [];
  pqsAnalysisList: Array<AnalysisPC> = [];


  /** Data return objects */
  objOrder: boolean;
  objInput: Input;
  objUser: boolean;
  objClient: Client;
  objProduct: boolean;
  objCellar: Cellar;
  objMoveInput: MoveInput;
  objCellarAdmin: CellarAdmin;
  objInputRequest: InputRequest;
  objInputRequestDesicion: InputRequestDesicion;
  objUnit: Unit;
  objAnalysis: Analysis;


  /** Filter terms */
  termO: string; // for Orders
  termO2: string; // for Orders by Clients
  termI: string; // for Inputs
  termU: string; // for Users
  termUt: string; // for Users
  termC: string; // for Clients
  termP: string; // for Products
  termB: string; // for Bodegas
  termUn: string; // for Units

  /** Combo validations */
  rolHasError = true;
  unitHasError = true;
  clientHasError = true;
  cellarHasError = true;
  statusHasError = true;
  statusUHasError = true;

  /** Input list validations */

  productExist = false;
  unitExist = false;
  analysisExist = false;
  listIsNotEmpty = false;
  inputCodeExist = false;
  userEmailExist = false;
  cellarNameExist = false;
  clientNameExist = false;

  productIdExist = false;
  /** Aux variables */
  auxQ = 1;

  /** Models */
  clientModel = new Client('', '', '', '', '', '');
  clientUpdateModel = new Client('', '', '', '', '', '');
  userModel = new User('', '', '', '', '', 'OPERARIO');
  userUpdateModel = new User('', '', '', '', '', 'default');
  inputModel = new Input(0, '', 0, '', '', '');
  inputUpdateModel = new Input(0, '', 0, '', '', '');
  productModel = new Product(0, ' ', ' ', ' ', ' ');
  productUpdateModel = new Product(0, '', '', '', '');
  clientEntryModel = new Client('', '', '', '', '', '');
  orderModel = new Order(0, this.clientEntryModel, '', this.productEntryList);
  orderDetailModel = new Order(0, this.clientEntryModel, '', this.productEntryList);
  cellarModel = new Cellar(0, '', '', '', '', this.inputQList);
  cellarUpdateModel = new Cellar(0, '', '', '', '', this.inputQList);
  cellarAdminModel = new CellarAdmin(this.cellar, '');
  moveInputModel = new MoveInput(0, 0, 0, 0);
  inputRequestModel = new InputRequest(0, 0, 0, this.inputQList, this.inputQListDiscard, '', '', '', '', '');
  inputRequestDesicionModel = new InputRequestDesicion(this.inputRequest, this.user, '');
  unitModel = new Unit('');
  localUser = new User('', '', '', '', '', '');
  analysisModel = new Analysis(0, 0, 0, 0, 0, 0, '', '', '', '', Array<AnalysisPC>());
  productEntryModel = new ProductInOrder(this.product, 0);
  searchProductModel = new Product(0, ' ', ' ', ' ', ' ');
  searchProductModel2 = new Product(0, '', '', '', '');
  userIn = new User('', '', '', '', '', '');

  Swal = ('sweetalert2');

  /** Variables for autocomplete  */
  public keyword = 'nombre';
  autoCompleteInput;
  autoCompleteProduct;
  autoCompleteAProduct;
  productAlreadyAdded = false;

  ngOnInit(): void {

    this.localUser = JSON.parse(localStorage.getItem('user logged'));
    
    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    /** Get Total Orders on Init */
    this.getTorder();

    /** Gets all Inputs on Init */
    this.getInput();

    /** Gets all Users on Init */
    this.getUser();

    /** Gets all clients on Init */
    this.getClient();

    /** Gets all available clients on Init */
    this.apiService.getAClient().subscribe(
      data => {
        this.clientAList = data;
      }
    );

    /** Gets all products on Init */
    this.getProduct();
    this.getProductA();

    /** Gets all unit types on Init */
    this.getUnits();
    this.getCellar();


  }

  /** Units */
  /** Used to post a new unit using the API service */
  postUnit() {
    this.apiService.addUnit(this.unitModel).subscribe(
      data => {
        this.objUnit = data;
        this.getUnits();
        Swal.fire({
          icon: 'success',
          title: '!Listo!',
          text: 'Unidad agregada con éxito',
          showConfirmButton: false,
          timer: 1500
        });
      }
    );

  }

  /** Used to validate the entry quantity */
  validateEntryQuantity() {
    if (this.auxQ < 0) {
      this.auxQ = 0;
    }
  }

  /** used to reset an order manually */
  orderCustomReset() {
    this.productEntryList.length = 0;
    this.productExist = false;
    this.searchProductModel = new Product(0, '', '', '', '');
    this.listIsNotEmpty = false;
  }

  /** Used to get all units using the API service */
  getUnits() {
    this.apiService.getUnits().subscribe(
      data => {
        this.unitList = data;
      }
    );

  }

  /** Used to search units using the API service */
  searchUnit() {
    for (const i of this.unitList) {
      if (this.unitModel.unidad.toUpperCase() === i.unidad.toUpperCase()) {
        this.unitExist = true;
        return;
      }
    }
    this.unitExist = false;
  }

  closeUnitExist(){
    this.unitExist = false;
  }

  /** Used to get all clients using the API service */
  getAllClient() {

    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );
  }

  /** Used to post an order using the API service */
  postOrder() {

    this.apiService.addOrder(this.orderModel).subscribe(
      data => {
        this.objOrder = data;
      }
    );
    this.getTorder();
    this.newOrder();
  }

  /** Used to reaload the order list manually */
  newOrder() {
    // window.location.reload();
    this.data.reloadOrderList(true);
  }

  /** Used to change the analysis from a current order */
  changeAnalysis(currentOrder: Order) {

    if (currentOrder !== null) {
      this.apiService.getAnalysisByID(currentOrder.codigo).subscribe(
        data => {
          this.analysisModel = data;
          this.analysisModel.pedCodigo = currentOrder.codigo;
          this.orderDetailModel = currentOrder;
          this.validateAnalysisExistance();
        }
      );
    }
  }

  /** Used to validate the existance of an analysis on an order */
  validateAnalysisExistance() {

    if (this.analysisModel.fechaEmision !== null) {
      this.analysisExist = true;
    } else {
      this.analysisExist = false;
    }
    console.log(`${this.analysisExist}`);
    return this.analysisExist;
  }

  /** Used to get the T orders using the API service */
  getTorder() {
    this.apiService.getTOrder().subscribe(
      data => {
        this.orderTlist = data;
      }
    );
  }

  /** Used to post an input using the API service */
  postInput() {

    this.apiService.addInput(this.inputModel).subscribe(
      data => {
        this.objInput = data;
        this.getInput();
        this.objInput = new Input(0, '', 0, '', '', '');
        Swal.fire({
          icon: 'success',
          title: '!Listo!',
          text: 'Insumo agregado con éxito',
          showConfirmButton: false,
          timer: 1500
        });
      }
    );
  }

  /** Used to get all inputs using the API service */
  getInput() {

    this.apiService.getInput().subscribe(
      data => {
        this.inputList = data;
        this.autoCompleteInput = this.inputList;
      }
    );
  }

  /** Used to post an user using the API service */
  postUser() {

    this.userModel.estado = 'HABILITADO';
    this.apiService.addUser(this.userModel).subscribe(
      data => {
        this.objUser = data;
        this.getUser();
        document.getElementById('btnClose').click();
        if (this.objUser) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Usuario agregado con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
  }

  /** Used to reset the drop down of user model */
  dropdownReset() {
    (document.getElementById('rolU') as HTMLSelectElement).value = 'OPERARIO';
    this.userModel.rol = 'OPERARIO';
  }

  /** Used to check for email existance to avoid duplication */
  checkEmailExist(){
   if (this.userList.length > 0){
    for (const entry of this.userList) {

      if (entry.correo.toUpperCase() === this.userModel.correo.toUpperCase()){
        this.userEmailExist = true;
        return;
      }
    }
   }
   this.userEmailExist = false;

  }

  /** Used to check for client existance to avoid duplication */
  checkClientExist(){
    if (this.clientList.length > 0){
     for (const entry of this.clientList) {
       if (entry.cedula === this.clientModel.cedula){
         this.clientNameExist = true;
         return;
       }
     }
    }
    this.clientNameExist = false;
   }

  closeUserEmailExist(){
    this.userEmailExist = false;
  }
  closeClientExist(){
    this.clientNameExist = false;
  }

  /** Used to get all users from API service */
  getUser() {

    this.apiService.getUser().subscribe(
      data => {
        this.userList = data;
      }
    );
  }

  /** Used to post a client using the API service */
  postClient() {

    this.clientModel.estado = 'HABILITADO';
    console.log(this.clientModel);
    this.apiService.addClient(this.clientModel).subscribe(
      data => {
        this.objClient = data;
        this.getClient();
        if (this.objClient) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Cliente agregado con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
    this.objClient = new Client('', '', '', '', '', '');
    this.getClient();
  }

  /** Used to get all clients using the API service */
  getClient() {
    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );
  }

  /** Used to post a product using the API service */
  postProduct() {

    this.apiService.addProduct(this.productModel).subscribe(
      data => {
        this.objProduct = data;
        this.getProduct();

        if (this.objProduct) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Producto agregado con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
  }

  /** Used to get all products using the API service */
  getProduct() {
    this.apiService.getProduct().subscribe(
      data => {
        this.productList = data;
        this.autoCompleteProduct = this.productList;
      }
    );
  }

  /** Used to get all products using the API service */
  getProductA() {
    this.apiService.getProductA().subscribe(
      data => {
        this.productAList = data;
        this.autoCompleteAProduct = this.productAList;
      }
    );
  }

  /** Used to post a cellar and refresh the cellar list */
  postGetCellar() {
    this.postCellar();
    this.getCellar();
  }

  /** Used to post a cellar using the API service */
  postCellar() {

    this.apiService.addCellar(this.cellarModel).subscribe(
      data => {
        let auxBool: Cellar;
        auxBool = data;
        this.getCellar();
        if (auxBool){
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Bodega agregada con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        }else{
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
  }

  /** Used to get all cellar using the API service */
  getCellar() {

    this.apiService.getCellar().subscribe(
      data => {
        this.cellarList = data;
      }
    );
  }

  /** Used to get all the avaliable cellars using the API service */
  getACellar() {

    this.apiService.getACellar().subscribe(
      data => {
        this.cellarList2 = data;
      }
    );
  }

  /** Used to get a cellar by ID using the API service */
  getOneCellar() {

    this.apiService.getOneCellar(this.cellarModel.codigo).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  /** Used to update a cellar using the API service */
  updateCellar() {

    this.apiService.updateCellar(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  /** Used to get a cellar status using the API service */
  cellarStatus() {

    this.apiService.cellarStatus(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  /** Used to move an input from a cellar to another using the API service */
  cellarMoveInput() {

    this.apiService.cellarMoveInput(this.moveInputModel).subscribe(
      data => {
        this.objMoveInput = data;
      }
    );
  }

  /** Used to get an input list from a cellar using the API service */
  cellarGetInputList() {

    this.apiService.getCellarInputList(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }


  /** Used to validate combo on user rol */
  validateRol(value) {
    if (value === 'default') {
      this.rolHasError = true;
    } else {
      this.rolHasError = false;
    }
  }

  /** Used to validate combo on input unit */
  validateUnit(value) {
    if (value === '') {
      this.unitHasError = true;
    } else {
      this.unitHasError = false;
    }
  }

  /** Used to validate combo on input update status */
  validateStatus(value) {
    if (value === 'default') {
      this.statusHasError = true;
    } else {
      this.statusHasError = false;
    }
  }

  /** Used to validate combo on input update status */
  validateUserStatus(value) {
    if (value === 'default') {
      this.statusUHasError = true;
    } else {
      this.statusUHasError = false;
    }
  }


  /** Used to validate combo on user rol */
  validateClient(value) {
    if (value === 'default') {
      this.clientHasError = true;
    } else {
      this.clientHasError = false;
    }
  }

  /** Used to validate if a cellar has an error */
  validateCellar(value) {
    if (value === 'default') {
      this.cellarHasError = true;
    } else {
      this.cellarHasError = false;
    }
  }

  /** Used to add a product entry */
  orderEntry() {

    for (const i of this.clientAList) {
      if (this.clientEntryModel.cedula.toUpperCase() === i.cedula.toUpperCase()) {
        this.orderModel.cliente = i;
      }
    }

    this.orderModel.correoAdminIngreso = this.localUser.correo;
    this.apiService.addOrder(this.orderModel).subscribe(
      data => {
        this.objOrder = data;
        this.getTorder();
        this.newOrder();
        if (this.objOrder) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Se ingresó el pedido con éxito!',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
  }

  /** Used to push a product into the product entry list */
  pushIntoEntryList() {
    this.productExist = false;
    this.productEntryModel.cantidad = this.auxQ;
    this.productEntryModel.producto = this.searchProductModel2;
    this.productEntryList.push(this.productEntryModel);
    this.productEntryModel = new ProductInOrder(this.product, 0);
    this.auxQ = 1;
    this.searchProductModel2 = new Product(0, '', '', '', '');
    this.searchProductModel = new Product(0, '', '', '', '');
    this.validateList();
    this.productExist = false;
  }

  /** Used to search a product */
  searchProduct() {
    for (const i of this.productList) {
      if (this.searchProductModel.nombre.toUpperCase() === i.nombre.toUpperCase()) {
        this.productExist = true;
        this.searchProductModel2 = i;
        return;
      } else {
        this.productExist = false;
      }
    }
  }

  /** Used to validate if a product already exists in the product entry list */
  selectedProduct(item){
      console.log(item);
      for (const i of this.productEntryList){
        console.log(i.producto.nombre);
        if (item.nombre === i.producto.nombre) {
          this.productAlreadyAdded = true;
        } else {
          this.productAlreadyAdded = false;
        }
      }
      this.productExist = true;
      this.searchProductModel2 = item;
      return;
  }

  /** Validations for inputs in order */
  validateList() {
    if (this.productEntryList.length > 0) {
      this.listIsNotEmpty = true;
    } else {
      this.listIsNotEmpty = false;
    }
  }

  /** Used to remove a product from the product entry list */
  removeFromList(i: number) {
    this.productEntryList.splice(i, 1);
    this.validateList();
  }

  /** Update methods */

  /** Used to set an input to update */
  chargeInputToUpdate(inputToUpdate: Input) {
    this.inputUpdateModel.id = inputToUpdate.id;
    this.inputUpdateModel.codigo = inputToUpdate.codigo;
    this.inputUpdateModel.estado = inputToUpdate.estado;
    this.inputUpdateModel.nombre = inputToUpdate.nombre;
    this.inputUpdateModel.unidad = inputToUpdate.unidad;
    this.inputUpdateModel.cantMinStock = inputToUpdate.cantMinStock;
  }

  /** Used to update an input using the API service */
  updateInput() {
    this.apiService.updateInput(this.inputUpdateModel).subscribe(
      data => {
        this.objInput = data;
        this.getInput();
        const Toast = Swal.mixin({
          toast: true,
          position: 'center',
          showConfirmButton: false,
          timer: 1000,
          onOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
          }
        });
        Toast.fire({
          icon: 'success',
          title: 'Insumo actualizado'
        });
      }
    );
  }

  /** User */

  /** Used to set an user to update */
  chargeUserToUpdate(userToUpdate: User) {
    this.userUpdateModel.rol = userToUpdate.rol;
    this.userUpdateModel.nombre = userToUpdate.nombre;
    this.userUpdateModel.estado = userToUpdate.estado;
    this.userUpdateModel.correo = userToUpdate.correo;
    this.userUpdateModel.contrasena = userToUpdate.contrasena;
    this.userUpdateModel.apellidos = userToUpdate.apellidos;
  }

  /** Used to update an user using the API service */
  updateUser() {

    this.apiService.updateUser(this.userUpdateModel).subscribe(
      data => {
        this.objUser = data;
        this.getUser();
        const Toast = Swal.mixin({
          toast: true,
          position: 'center',
          showConfirmButton: false,
          timer: 1000,
          onOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
          }
        });
        Toast.fire({
          icon: 'success',
          title: 'Usuario actualizado'
        });
      }
    );
  }

  /** Client */

  /** Used to set a client to update */
  chargeClientToUpdate(clientToUpdate: Client) {
    this.clientUpdateModel.cedula = clientToUpdate.cedula;
    this.clientUpdateModel.correo = clientToUpdate.correo;
    this.clientUpdateModel.direccion = clientToUpdate.direccion;
    this.clientUpdateModel.estado = clientToUpdate.estado;
    this.clientUpdateModel.nombre = clientToUpdate.nombre;
    this.clientUpdateModel.telefono = clientToUpdate.telefono;
  }

  /** Used to update a client using the API service */
  updateClient() {

    this.apiService.updateClient(this.clientUpdateModel).subscribe(
      data => {
        this.objClient = data;
        this.getClient();
        const Toast = Swal.mixin({
          toast: true,
          position: 'center',
          showConfirmButton: false,
          timer: 1000,
          onOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
          }
        });
        Toast.fire({
          icon: 'success',
          title: 'Cliente actualizado'
        });
      }
    );
  }

  /** Product */

  /** Used to set a product to update */
  chargeProductToUpdate(productToUpdate: Product) {
    this.productUpdateModel.codigo = productToUpdate.codigo;
    this.productUpdateModel.descripcion = productToUpdate.descripcion;
    this.productUpdateModel.estado = productToUpdate.estado;
    this.productUpdateModel.id = productToUpdate.id;
    this.productUpdateModel.nombre = productToUpdate.nombre;
  }

  /** Used to check a product code before creation */
  productCodeExist(forCreation: boolean){
    for (const i of this.productList) {
      if (forCreation) {
        if (this.productModel.id.toUpperCase() === i.id.toUpperCase()) {
          this.productIdExist = true;
          return;
        } else {
          this.productIdExist = false;
        }
      } else {
        if (this.productUpdateModel.id.toUpperCase() === i.id.toUpperCase()) {
          this.productIdExist = true;
          return;
        } else {
          this.productIdExist = false;
        }
      }
    }
  }

  /** Used to update a product using the API service */
  updateProduct() {

    this.apiService.updateProduct(this.productUpdateModel).subscribe(
      data => {
        this.objProduct = data;
        if (this.objProduct) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Producto actualizado con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
        this.getProduct();
        this.getProductA();
      });
  }

  /** Cellar */

  /** Used to set a cellar to update */
  chargeCellarToUpdate(cellarToUpdate: Cellar) {
    this.cellarUpdateModel.codigo = cellarToUpdate.codigo;
    this.cellarUpdateModel.direccion = cellarToUpdate.direccion;
    this.cellarUpdateModel.estado = cellarToUpdate.estado;
    this.cellarUpdateModel.listaInsumosEnBodega = cellarToUpdate.listaInsumosEnBodega;
    this.cellarUpdateModel.nombre = cellarToUpdate.nombre;
    this.cellarUpdateModel.telefono = cellarToUpdate.telefono;
  }

  /** Used to update a cellar using the API service */
  updateCellars() {

    this.apiService.updateCellar(this.cellarUpdateModel).subscribe(
      data => {
        this.objCellar = data;
        this.getCellar();
        const Toast = Swal.mixin({
          toast: true,
          position: 'center',
          showConfirmButton: false,
          timer: 1000,
          onOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
          }
        });
        Toast.fire({
          icon: 'success',
          title: 'Bodega actualizada'
        });
      }
    );
  }

  /** Used to validate the unique name of a cellar before creation */
  validateCeNameUniqueness(forCreation: boolean) {
    for (const i of this.cellarList) {
      if (forCreation){
        if (this.cellarModel.nombre.toUpperCase() === i.nombre.toUpperCase()) {
          this.cellarNameExist = true;
          return;
        } else {
          this.cellarNameExist = false;
        }
      } else {
        if (this.cellarUpdateModel.nombre.toUpperCase() === i.nombre.toUpperCase()) {
          this.cellarNameExist = true;
          return;
        } else {
          this.cellarNameExist = false;
        }
      }
    }
  }

  /** Used to reset the cellar name existance variable */
  resetCellarNameExist(){
    this.cellarNameExist = false;
  }

  /** Input validations */
  validateCodeUniqueness(forCreation: boolean) {
    for (const i of this.inputList) {
      if (forCreation) {
        if (this.inputModel.id.toUpperCase() === i.id.toUpperCase()) {
          this.inputCodeExist = true;
          return;
        } else {
          this.inputCodeExist = false;
        }
      } else {
        if (this.inputUpdateModel.id.toUpperCase() === i.id.toUpperCase()) {
          this.inputCodeExist = true;
          return;
        } else {
          this.inputCodeExist = false;
        }
      }
    }
  }

  /** Used to reset the product ID existance variable */
  resetExistProductId(){
    this.productIdExist = false;
  }

  /** Used to reset the input ID existance variable */
  resetinputCodeExist() {
    this.inputCodeExist = false;
  }
}
