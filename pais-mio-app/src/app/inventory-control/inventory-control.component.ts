import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

import { InputQ } from 'src/models/inputQ';
import { Input } from 'src/models/input';
import { Cellar } from 'src/models/cellar';
import { CellarAdmin } from 'src/models/cellarAdmin';
import { User } from 'src/models/user';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-inventory-control',
  templateUrl: './inventory-control.component.html',
  styleUrls: ['./inventory-control.component.scss']
})
export class InventoryControlComponent implements OnInit {


  /** List declarations */
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
  listIsNotEmpty = false;

  /** Models */
  inputQModel = new InputQ(0, this.input);
  cellarModel = new Cellar(0, '', '', '', '', this.inputQListInCellar);
  cellarEntryModel = new Cellar(0, '', '', '', '', this.inputEntryList);
  cellarAdminModel = new CellarAdmin(this.cellarEntryModel, '');
  inputEntryModel = new InputQ(0, this.input);
  searchInputModel = new Input(0, '', 0, '', '', '');
  searchInputModel2 = new Input(0, '', 0, '', '', '');
  localUser = new User('', '', '', '', '', '');

  /** Data return objects */
  objInputQ: InputQ;
  objCellarAdmin: CellarAdmin;

  /** Filter terms */
  termIQ: string;
  termIQ2: string;

  /** Combo validations */
  cellarHasError = true;

  /** Aux variables */
  auxQ: number;


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

    this.localUser = JSON.parse(localStorage.getItem('user logged'));
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
  inputEntry() {
    this.cellarAdminModel.doBodega = this.cellarEntryModel;
    this.cellarAdminModel.correoAdministrador = this.localUser.correo;

    this.apiService.inputEntry(this.cellarAdminModel).subscribe(
      data => {
        this.objCellarAdmin = data;
        if (this.objCellarAdmin) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: '¡Se agregó el insumo con éxito!',
            showConfirmButton: false,
            timer: 1500
          });
        } else {
          Swal.fire({
            icon: 'warning',
            title: '!Ups!',
            text: 'Ocurrió algún error, vuelve a intentarlo',
            showConfirmButton: false,
            timer: 1500
          });
        }
      }
    );
  }

  pushIntoEntryList() {
    this.inputExist = false;
    this.inputEntryModel.cantidadDisponible = this.auxQ;
    this.inputEntryModel.insumo = this.searchInputModel2;
    this.inputEntryList.push(this.inputEntryModel);
    this.inputEntryModel = new InputQ(0, this.input);
    this.auxQ = 0;
    this.searchInputModel2 = new Input(0, '', 0, '', '', '');
    this.searchInputModel = new Input(0, '', 0, '', '', '');
    this.validateList();
    this.inputExist = false;
  }

  searchInput() {
    for (const i of this.inputList) {
      if (this.searchInputModel.nombre.toUpperCase() === i.nombre.toUpperCase()) {
        this.inputExist = true;
        this.searchInputModel2 = i;
        return;
      } else {
        this.inputExist = false;
      }
    }
  }

  validateList(){
    if (this.inputEntryList.length > 0){
      this.listIsNotEmpty = true;
    } else {
      this.listIsNotEmpty = false;
    }
  }

  removeFromList(i: number){
    this.inputEntryList.splice(i, 1);
    this.validateList();
  }

  resetInputEntryList(){
    this.inputEntryList.length = 0;
    this.listIsNotEmpty = false;
    this.inputExist = false;
    this.searchInputModel = new Input(0, '', 0, '', '', '');
  }

  validateEntryQuantity() {
    if (this.auxQ < 0) {
      this.auxQ = 0;
    }
  }


}
