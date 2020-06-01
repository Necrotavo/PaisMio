import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { Client } from '../../models/client';
import { User } from '../../models/user';
import { Input } from '../../models/input';
import { Product } from 'src/models/product';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  /** Object declarations for Post and Get */
  clientList: Client[];
  userList: User[];
  client: Client;
  objClient: Client;
  inputList: Input[];
  productList: Product[];

  /** Filter terms */
  termO: string; // for Orders
  termO2: string; // for Orders by Clients
  termI: string; // for Inputs
  termU: string; // for Users
  termC: string; // for Clients
  termP: string; // for Products


  ngOnInit(): void {
    /** Client */
    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );

    /** User */
    this.apiService.getUser().subscribe(
      data => {
        this.userList = data;
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

  postClient(){
    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
      }
    );
  }

  postClient2(){
    const test = '';
  }
}
