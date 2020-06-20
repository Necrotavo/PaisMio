import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';
import { User } from 'src/models/user';

@Component({
  selector: 'app-index-pm-app',
  templateUrl: './index-pm-app.component.html',
  styleUrls: ['./index-pm-app.component.scss']
})
export class IndexPmAppComponent implements OnInit {

  order: Order;
  orderList: Order[];
  userIn = new User('', '', '', '', '', '');

  constructor(private data: DataService, private apiService: ApiService) { }

  ngOnInit(): void {
    this.data.activeOrder.subscribe(order => this.order = order);

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    this.userIn = JSON.parse(localStorage.getItem('user logged'));
    console.log('Imprimo: ' + this.userIn.correo);
  }

  newOrder(i: number) {
    this.data.changeOrder(this.orderList[i]);
  }

}
