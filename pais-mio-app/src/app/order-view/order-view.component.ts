import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  analysisExist = false;

  constructor(private data: DataService, private apiService: ApiService) { }

  order: Order;
  orderList: Order[];

  ngOnInit(): void {

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    this.data.activeOrder.subscribe(order => this.order = order);
    if (this.order === null) {
      console.log('Soy null :(');
    }

  }

  validateAnalysisExistance(){
    if (this.order.doAnalisisAA !== null){
      this.analysisExist = true;
    } else {
      this.analysisExist = false;
    }
  }

}
