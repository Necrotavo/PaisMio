import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

import { InputQ } from 'src/models/inputQ';
import { Input } from 'src/models/input';
import { Cellar } from 'src/models/cellar';

@Component({
  selector: 'app-inventory-control',
  templateUrl: './inventory-control.component.html',
  styleUrls: ['./inventory-control.component.scss']
})
export class InventoryControlComponent implements OnInit {


  inputQList: InputQ[];
  inputQListInCellar: Array<InputQ> = [];
  cellarList: Cellar[];
  input: Input;

  /** Filter terms */
  termIQ: string;
  termIQ2: string;


  constructor(private apiService: ApiService) { }

  ngOnInit(): void {

    this.apiService.getCellar().subscribe(
      data => {
        this.cellarList = data;

        this.cellarList.forEach(element => {
          for (const i of element.listaInsumosEnBodega) {
            this.inputQListInCellar.push(i);
          }
        });
      }
    );

    this.apiService.getACellar().subscribe(
      data => {
        this.cellarList = data;
      }
    );

  }

  getInputQOnClick(){
    this.apiService.getInputQ().subscribe(
      data => {
        this.inputQList = data;
      }
    );
  }

}
