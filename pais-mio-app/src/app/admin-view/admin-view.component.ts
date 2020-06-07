import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { Client } from 'src/models/client';
import { User } from 'src/models/user';
import { Input } from 'src/models/input';
import { Product } from 'src/models/product';
import { Order } from 'src/models/order';


@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
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
  productList: Product[];

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

  /** Models */
  clientModel = new Client('', '', '', '', '', '');
  userModel = new User('', '', '', '', '', '');

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

    /** Gets all products on Init */
    this.apiService.getProduct().subscribe(
      data => {
        this.productList = data;
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
    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

  postUser(){
    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

  postClient(){

    //const newClient = new Client('111111111', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');
    this.clientModel.estado = 'HABILITADO';
    console.log(this.clientModel);
    this.apiService.addClient(this.clientModel).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

  postProduct(){
    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

}
