import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  constructor(private data: DataService) { }

  order: Order;

  ngOnInit(): void {
    this.data.activeOrder.subscribe(order => this.order = order);
  }

}
