import { Component, OnInit } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';

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
import { UserRolUpgrade } from 'src/models/userRolUpgrade';


@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})


export class AdminViewComponent implements OnInit {

  constructor(private apiService: ApiService) { }

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

  /** Object Lists */
  orderList: Order[];
  inputList: Input[];
  userList: User[];
  clientList: Client[];
  clientAList: Client[];
  productList: Product[];
  unitList: Unit[];
  productInOrderList: ProductInOrder[];
  inputQList: InputQ[];
  inputQListDiscard: InputQ[];
  cellarList: Cellar[];
  inputRequestList: InputRequest[]

  /** Data return objects */
  objOrder: Order;
  objInput: Input;
  objUser: User;
  objClient: Client;
  objProduct: Product;
  objCellar: Cellar;
  objMoveInput: MoveInput;
  objCellarAdmin: CellarAdmin;
  objInputRequest: InputRequest;
  objInputRequestDesicion: InputRequestDesicion;

  /** Filter terms */
  termO: string; // for Orders
  termO2: string; // for Orders by Clients
  termI: string; // for Inputs
  termU: string; // for Users
  termC: string; // for Clients
  termP: string; // for Products

  /** Combo validations */
  rolHasError = true;
  unitHasError = true;
  clientHasError = true;

  /** Models */
  clientModel = new Client('', '', '', '', '', '');
  userModel = new User('', '', '', '', '', 'default');
  inputModel = new Input(0, '', 0, '', '', '');
  productModel = new Product(0, '', '', '', '');
  orderModel = new Order(0, '', '', this.productInOrderList);
  cellarModel = new Cellar(0, '', '', '', '', this.inputQList);
  cellarAdminModel = new CellarAdmin(this.cellar, '');
  moveInputModel = new MoveInput(0, 0, 0, 0);
  inputRequestModel = new InputRequest(0, 0, 0, this.inputQList, this.inputQListDiscard, '', '', '', '');
  inputRequestDesicionModel = new InputRequestDesicion(this.inputRequest, this.user, '');

  ngOnInit(): void {

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    /** Gets all Inputs on Init */
    this.apiService.getInput().subscribe(
      data => {
        this.inputList = data;
      }
    );

    /** Gets all Users on Init */
    this.apiService.getUser().subscribe(
      data => {
        this.userList = data;
      }
    );

    /** Gets all clients on Init */
    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );

    /** Gets all available clients on Init */
    this.apiService.getAClient().subscribe(
      data => {
        this.clientAList = data;
      }
    );

    /** Gets all products on Init */
    this.apiService.getProduct().subscribe(
      data => {
        this.productList = data;
      }
    );

    /** Gets all unit types on Init */
    this.apiService.getUnits().subscribe(
      data => {
        this.unitList = data;
      }
    );

  }

  getAllClient(){

    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );
  }

  postOrder(){

    this.apiService.addOrder(this.orderModel).subscribe(
      data => {
        this.objOrder = data;
      }
    );
  }

  postInput(){

    this.apiService.addInput(this.inputModel).subscribe(
      data => {
        this.objInput = data;
      }
    );
  }

  postUser(){

    this.userModel.estado = 'HABILITADO';
    this.apiService.addUser(this.userModel).subscribe(
      data => {
        this.objUser = data;
      }
    );
  }

  postClient(){

    this.clientModel.estado = 'HABILITADO';
    console.log(this.clientModel);
    this.apiService.addClient(this.clientModel).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

  postProduct(){

    this.apiService.addProduct(this.productModel).subscribe(
      data => {
        this.objProduct = data;
      }
    );
  }

  /**Cellar Crud */
  postCellar(){

    this.apiService.addCellar(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  getCellar(){

    this.apiService.getCellar().subscribe(
      data => {
        this.cellarList = data;
      }
    );
  }

  getACellar(){

    this.apiService.getACellar().subscribe(
      data => {
        this.cellarList = data;
      }
    );
  }

  getOneCellar(){

    this.apiService.getOneCellar(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  updateCellar(){

    this.apiService.updateCellar(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  cellarStatus(){

    this.apiService.cellarStatus(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  cellarMoveInput(){

    this.apiService.cellarMoveInput(this.moveInputModel).subscribe(
      data => {
        this.objMoveInput = data;
      }
    );
  }

  /**Este metodo permite la entrada de insumos */
  cellarInputPut(){

    this.apiService.cellarInputPut(this.cellarAdminModel).subscribe(
      data => {
        this.objCellarAdmin = data;
      }
    );
  }

  cellarGetInputList(){

    this.apiService.getCellarInputList(this.cellarModel).subscribe(
      data => {
        this.objCellar = data;
      }
    );
  }

  /**Aqui Estoy
      solicitudPorOperario	POST	Service at https://www.spepaismio.tk/WS_SolicitudInsumo.svc/solicitudPorOperario
      solicitudPorPedido	POST	Service at https://www.spepaismio.tk/WS_SolicitudInsumo.svc/solicitudPorPedido
  */
  /**InputRequest Crud*/
  postInputRequest(){

    this.apiService.addInputRequest(this.inputRequestModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }

  getInputRequest(){

    this.apiService.getInputRequest().subscribe(
      data => {
        this.inputRequestList = data;
      }
    );
  }

  setInputRequestDesicion(){

    this.apiService.setInputRequestDecision(this.inputRequestDesicionModel).subscribe(
      data => {
        this.objInputRequestDesicion = data;
      }
    );
  }

  getInputRequestByUser(){

    this.apiService.getInputRequestByUser(this.userModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }

  getInputRequestByOrder(){

    this.apiService.getInputRequestByOrder(this.orderModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }

  searchInputRequest(){

    this.apiService.getInputRequestByID(this.inputRequestModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }


  /** Used to validate combo on user rol */
  validateRol(value){
    if (value === 'default'){
      this.rolHasError = true;
    } else {
      this.rolHasError = false;
    }
  }

  /** Used to validate combo on input unit */
  validateUnit(value){
    if (value === 'default'){
      this.unitHasError = true;
    } else {
      this.unitHasError = false;
    }
  }

    /** Used to validate combo on user rol */
    validateClient(value){
      if (value === 'default'){
        this.clientHasError = true;
      } else {
        this.clientHasError = false;
      }
    }

}
