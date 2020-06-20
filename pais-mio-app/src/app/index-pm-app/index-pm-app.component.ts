import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-index-pm-app',
  templateUrl: './index-pm-app.component.html',
  styleUrls: ['./index-pm-app.component.scss']
})
export class IndexPmAppComponent implements OnInit {

  order: Order;
  orderList: Order[];

  constructor(private data: DataService, private apiService: ApiService) { }

  ngOnInit(): void {
    this.data.activeOrder.subscribe(order => this.order = order);

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );
  }

  newOrder(i: number) {
    this.data.changeOrder(this.orderList[i]);
  }

}
