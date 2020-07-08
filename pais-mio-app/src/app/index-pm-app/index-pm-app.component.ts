import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';
import { User } from 'src/models/user';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-index-pm-app',
  templateUrl: './index-pm-app.component.html',
  styleUrls: ['./index-pm-app.component.scss']
})
export class IndexPmAppComponent implements OnInit {

  order: Order;
  orderList: Order[];
  userIn = new User('', '', '', '', '', '');

  constructor(private data: DataService, private apiService: ApiService, private authService: AuthService) { }

  ngOnInit(): void {
    this.data.activeOrder.subscribe(order => this.order = order);

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    this.userIn = JSON.parse(localStorage.getItem('user logged'));
  }

  /** Used to change the order in the observable and set and active order in local storage */
  newOrder(i: number) {
    this.data.changeOrder(this.orderList[i]);
    localStorage.setItem('active order', JSON.stringify(this.orderList[i]));
  }

}
