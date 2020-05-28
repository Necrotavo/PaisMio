import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { Client } from '../../models/client';
import { User } from '../../models/user';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  clientList: Client[];
  userList: User[];
  client: Client;
  objClient: Client;

  ngOnInit(): void {
    /** Client */
    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );

    const newClient = new Client('333333', 'prueba@mail.com', 'grecia', 'HABILITADO', 'Random.INC', '(+506) 131313123');

    this.apiService.addClient(newClient).subscribe(
      data => {
        this.objClient = data;
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
}
