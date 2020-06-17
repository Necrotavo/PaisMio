import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

import { InputQ } from 'src/models/inputQ';
import { Input } from 'src/models/input';
import { Cellar } from 'src/models/cellar';
import { CellarAdmin } from 'src/models/cellarAdmin';

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
  cellar: Cellar;
  adminAddress: string;
  inputEntryList: Array<InputQ> = [];

  /** Input list validations */
  inputList: Input[];
  inputExist = false;

  /** Models */
  inputQModel = new InputQ(0, this.input);
  cellarModel = new Cellar(0, '', '', '', '', this.inputQListInCellar);
  cellarEntryModel = new Cellar(0, '', '', '', '', this.inputEntryList);
  cellarAdminModel = new CellarAdmin(this.cellarEntryModel, '');
  inputEntryModel = new InputQ(0, this.input);
  searchInputModel = new Input(0, '', 0, '', '', '');
  searchInputModel2 = new Input(0, '', 0, '', '', '');

  /** Data return objects */
  objInputQ: InputQ;
  objCellarAdmin: CellarAdmin;

  /** Filter terms */
  termIQ: string;
  termIQ2: string;

  /** Combo validations */
  cellarHasError = true;


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

    /** Gets all Inputs on Init */
    this.apiService.getInput().subscribe(
      data => {
        this.inputList = data;
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

  postInputQ(){

    this.apiService.addInputQ(this.inputQModel).subscribe(
      data => {
        this.objInputQ = data;
      }
    );
  }

  /** Used to validate combo on cellar */
  validateCellar(value){
    if (value === 'default'){
      this.cellarHasError = true;
    } else {
      this.cellarHasError = false;
    }
  }

  /** Used to add a input entry */
  inputEntry(){
    this.cellarEntryModel.listaInsumosEnBodega = this.inputEntryList;
    this.cellarAdminModel.doBodega = this.cellarEntryModel;
    this.cellarAdminModel.correoAdministrador = 'pal@lomo.com';

    this.apiService.inputEntry(this.cellarAdminModel).subscribe(
      data => {
        this.objCellarAdmin = data;
      }
    );
  }

  pushIntoEntryList(){
    this.inputEntryList.push(this.inputEntryModel);
  }

  searchInput(){
    for (const i of this.inputList){
      if (this.searchInputModel.nombre === i.nombre){
        this.inputExist = true;
        this.searchInputModel2 = i;
        return;
      } else {
        this.inputExist = false;
      }
    }
  }

}
