import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Client } from '../../models/client';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss']
})
export class ClientComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  clientList: Client[];

  ngOnInit(): void {
    this.apiService.getClient().subscribe(
      data => {
        this.clientList = data;
      }
    );
  }

  saveClient(): void {
  }
}
