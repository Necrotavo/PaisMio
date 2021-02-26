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
  searchInputModel = new Input(0, ' ', 0, ' ', ' ', ' ');
  searchInputModel2 = new Input(0, '', 0, '', '', '');
  localUser = new User('', '', '', '', '', '');
  autoCellarEntryModel = new Cellar(0, '', '', '', '', new Array<InputQ>());
  /** Data return objects */
  objInputQ: InputQ;
  objCellarAdmin: CellarAdmin;

  /** Filter terms */
  termIQ: string;
  termIQ2: string;

  /** Validations */
  cellarHasError = true;
  inputHasError = true;
  inputAlreadyAdded = false;


  /** Aux variables */
  auxQ = 1;

  /** variables for autocomplete */
  public keyword = 'nombre';
  autoCompleteInput;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {

    this.getACellarList();

    /** Gets all Inputs on Init from API service */
    this.apiService.getInput().subscribe(
      data => {
        this.inputList = data;
        this.autoCompleteInput = this.inputList;
      }
    );

    this.localUser = JSON.parse(localStorage.getItem('user logged'));
  }

  /** Gets all cellars on Init from API service */
  getCellarList() {
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
  }

  /** Gets all avaliable cellars on Init from API service */
  getACellarList() {
    this.apiService.getACellar().subscribe(
      data => {
        this.cellarList = data;
        this.cellarList.forEach(element => {
          for (const i of element.listaInsumosEnBodega) {
            this.inputQListInCellar.push(i);
          }
        });
      }
    );
  }

  /** Used to get all input quantity on click */
  getInputQOnClick() {
    this.apiService.getInputQ().subscribe(
      data => {
        this.inputQList = data;
      }
    );
  }

  /** Used to post input quantity using API service */
  postInputQ() {

    this.apiService.addInputQ(this.inputQModel).subscribe(
      data => {
        this.objInputQ = data;
      }
    );
  }

  /** Used to validate combo on cellar */
  validateCellar(value) {
    if (value === 'default') {
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
            title: '¡Listo!',
            text: '¡Se agregaron los insumos con éxito!',
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
    this.getACellarList();
  }

  /** Used to push an input into de input entry list */
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

  /** Used to search and input and validate existance */
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

  /** Used to validate if the input entry list is empty */
  validateList() {
    if (this.inputEntryList.length > 0) {
      this.listIsNotEmpty = true;
    } else {
      this.listIsNotEmpty = false;
    }
  }

  /** Used to remove an input from the input entry list */
  removeFromList(i: number) {
    this.inputEntryList.splice(i, 1);
    this.validateList();
    this.inputAlreadyAdded = false;
  }

  /** Used to reset the input entry list */
  resetInputEntryList() {
    this.auxQ = 1;
    this.inputEntryList.length = 0;
    this.listIsNotEmpty = false;
    this.inputExist = false;
    this.searchInputModel = new Input(0, '', 0, '', '', '');
    this.inputAlreadyAdded = false;
  }

  /** Used to validate the input quantity */
  validateEntryQuantity() {
    if (this.auxQ < 0) {
      this.auxQ = 0;
    }
  }

  /** Used to validate if an input is already on list */
  validateInputName() {
    for (const i of this.inputEntryList) {
      if (this.searchInputModel.nombre.toUpperCase() === i.insumo.nombre.toUpperCase()) {
        this.inputAlreadyAdded = true;
        return;
      } else {
        this.inputAlreadyAdded = false;
      }
    }
  }

  /** Used when an input is selected on the autocomplete list */
  selectedInput(item) {
    for (const i of this.inputEntryList) {
      if (item.nombre === i.insumo.nombre) {
        this.inputAlreadyAdded = true;
      } else {
        this.inputAlreadyAdded = false;
      }
    }
    this.inputExist = true;
    this.searchInputModel2 = item;
    this.inputHasError = false;
  }

  /** Used when an input is deselected */
  deselectedInput(item){
    this.auxQ = 0;
    this.inputHasError = true;
  }

  /** Used when an input is selected to inject the code in the cellar entry model */
  selectedCellar(item){
    this.cellarEntryModel.codigo = item.codigo;
    this.cellarHasError = false;
  }

  /** Used when an cellar is deselected */
  deselectedCellar(item){
    this.cellarHasError = true;
  }
}
