import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  orderList: Order[];

  private orderSource = new BehaviorSubject<Order>(null);
  activeOrder = this.orderSource.asObservable();

  constructor() { }

  changeOrder(order: Order){
    this.orderSource.next(order);
  }

}
