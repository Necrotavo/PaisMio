import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  order: Order;
  orderList: Order[];
  count: number;
  activeMessage: string;

  constructor(private data: DataService, private apiService: ApiService) { }

  ngOnInit(): void {
    this.data.activeOrder.subscribe(order => this.order = order);

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
        this.count = this.orderList.length;
        if (this.count === 1) {
          this.activeMessage = this.count + ' Pedido activo';
        } else  if (this.count === 0){
          this.activeMessage = 'No hay pedidos activos';
        } else {
          this.activeMessage = this.count + ' Pedidos activos';
        }
      }
    );
  }

  newOrder(i: number) {
    this.data.changeOrder(this.orderList[i]);
  }

}
