import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';
import { InputRequest } from 'src/models/inputRequest';
import { InputQ } from 'src/models/inputQ';
import { InputRequestDesicion } from 'src/models/inputRequestDecision';
import { User } from 'src/models/user';
import { Client } from 'src/models/client';
import { ProductInOrder } from 'src/models/productInOrder';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  analysisExist = false;

  constructor(private data: DataService, private apiService: ApiService) { }

  /** Object Declarations */
  order: Order;
  inputRequest: InputRequest;

  user: User;
  client: Client;

  /** Object Lists */
  orderList: Order[];
  consumeList: InputQ[];
  discardList: InputQ[];
  inputRequestListByOrder: InputRequest[];
  inputRequestListByUser: InputRequest[];
  inputRequestList: InputRequest[];
  productEntryList: Array<ProductInOrder> = [];

  /** Data Return Objects */
  objInputRequest: InputRequest;
  objInputRequestDesicion: InputRequestDesicion;


  /** Filter Terms */
  /** Combo Validations */
  /** Models */
  inputRequestModel = new InputRequest(0, 0, 0, this.consumeList, this.discardList, '', '', '', '');
  inputRequestDetailsModel = new InputRequest(0, 0, 0, this.consumeList, this.discardList, '', '', '', '');
  inputRequestDesicionModel = new InputRequestDesicion(this.inputRequest, this.user, '');
  userModel = new User('', '', '', '', '', 'default');

  /*
  clientModel = new Client('', '', '', '', '', '');
  inputModel = new Input(0, '', 0, '', '', '');
  productModel = new Product(0, '', '', '', '');
  clientEntryModel = new Client('', '', '', '', '', '');
  orderModel = new Order(0, this.clientEntryModel, '', this.productEntryList);
  cellarModel = new Cellar(0, '', '', '', '', this.inputQList);
  cellarAdminModel = new CellarAdmin(this.cellar, '');
  moveInputModel = new MoveInput(0, 0, 0, 0);
  inputRequestModel = new InputRequest(0, 0, 0, this.inputQList, this.inputQListDiscard, '', '', '', '');

  unitModel = new Unit('');
*/


  ngOnInit(): void {

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    this.data.activeOrder.subscribe(order => this.order = order);
    if (this.order === null) {
      this.order = JSON.parse(localStorage.getItem('active order'));
      console.log('Imprimo: ' + this.order.cliente);
    }

    this.getInputRequestByOrder();

  }

  validateAnalysisExistance(){
    if (this.order.doAnalisisAA !== null){
      this.analysisExist = true;
    } else {
      this.analysisExist = false;
    }
  }

  /** InputRequest CRUD */

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

    this.apiService.getInputRequestByOrder(this.order).subscribe(
      data => {
        this.inputRequestListByOrder = data;
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

  asignRequest(request: InputRequest){
    this.inputRequestModel = request;
  }
}
