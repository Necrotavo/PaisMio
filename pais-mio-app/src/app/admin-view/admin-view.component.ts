import { Component, OnInit } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';

import { ApiService } from '../api.service';
import { Client } from 'src/models/client';
import { User } from 'src/models/user';
import { Input } from 'src/models/input';
import { Product } from 'src/models/product';
import { Order } from 'src/models/order';
import { Unit } from 'src/models/unit';
import { ProductInOrder } from 'src/models/productInOrder';


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

  /** Object Lists */
  orderList: Order[];
  inputList: Input[];
  userList: User[];
  clientList: Client[];
  clientAList: Client[];
  productList: Product[];
  unitList: Unit[];
  productInOrderList: ProductInOrder[];

  /** Data return objects */
  objOrder: Order;
  objInput: Input;
  objUser: User;
  objClient: Client;
  objProduct: Product;

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

  /** Models */
  clientModel = new Client('', '', '', '', '', '');
  userModel = new User('', '', '', '', '', 'default');
  inputModel = new Input(0, '', 0, '', '', '');
  productModel = new Product(0, '', '', '', '');

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
    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
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
    //this.productModel.estado = 'HABILITADO';
    this.apiService.addProduct(this.productModel).subscribe(
      data => {
        this.objProduct = data;
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

}
