import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { ApiService } from '../api.service';
import { InputRequest } from 'src/models/inputRequest';
import { InputQ } from 'src/models/inputQ';
import { Input } from 'src/models/input';
import { InputRequestDesicion } from 'src/models/inputRequestDecision';
import { User } from 'src/models/user';
import { Client } from 'src/models/client';
import { ProductInOrder } from 'src/models/productInOrder';
import { Analysis } from 'src/models/analysis';
import { AnalysisPC } from 'src/models/analysisPC';

import { Cellar } from 'src/models/cellar';
import Swal from 'sweetalert2';
import { Router, RouterLink } from '@angular/router';

import { NavbarComponent } from 'src/app/navbar/navbar.component';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent implements OnInit {

  analysisExist = false;

  constructor(private data: DataService, private apiService: ApiService, router: Router) { }

  navbar: NavbarComponent;

  /** Object Declarations */
  order: Order;
  inputRequest: InputRequest;
  input: Input;


  user: User;
  client: Client;

  /** Object Lists */
  cellarList: Cellar[];
  orderList: Order[];
  consumeList: Array<InputQ> = [];
  discardList: Array<InputQ> = [];
  inputRequestListByOrder: Array<InputRequest> = [];
  inputRequestListByUser: InputRequest[];
  inputRequestList: InputRequest[];
  productEntryList: Array<ProductInOrder> = [];
  inputList: Input[];
  inputConsumeList: Array<InputQ> = [];
  inputDiscardList: Array<InputQ> = [];
  inputEntryList: Array<InputQ> = [];
  inputQListInCellar: Array<InputQ> = [];
  pqsAnalysisList: Array<AnalysisPC> = [];

  /** Data Return Objects */
  objInputRequest: InputRequest;
  objInputRequestDesicion: InputRequestDesicion;
  objAnalysis: Analysis;

  /** Validations */
  inputExist = false;
  listIsNotEmpty = false;
  discardListIsNotEmpty = false;
  cellarHasError = false;
  inputInConsumeList = false;
  inputInDiscardList = false;
  invalidRequest = false;

  /** Models */
  inputRequestModel = new InputRequest(0, 0, 0, this.consumeList, this.discardList, '', '', '', '', '');
  inputPostRequestModel = new InputRequest(0, 0, 0, this.consumeList, this.discardList, 'jojo@goldenwind.com', '', '', '', '');
  inputRequestDetailsModel = new InputRequest(0, 0, 0, this.consumeList, this.discardList, '', '', '', '', '');
  inputRequestDesicionModel = new InputRequestDesicion(this.inputRequestModel, this.user, '');
  userModel = new User('', '', '', '', '', 'default');
  searchInputModel = new Input(0, ' ', 0, ' ', ' ', ' ');
  searchInputModel2 = new Input(0, '', 0, '', '', '');
  inputEntryModel = new InputQ(0, this.input);
  cellarInputSelected = new InputQ(0, new Input(0, '', 0, '', '', ''));
  analysisModel = new Analysis(0, 0, 0, 0, 0, 0, '', '', '', '', Array<AnalysisPC>());
  localUser = new User('', '', '', '', '', '');
  cellarEntryModel = new Cellar(0, '', '', '', '', this.inputEntryList);
  cellarDetailModel = new Cellar(0, '', '', '', '', null);
  orderModel = new Order(0, this.client, '', this.productEntryList);
  objOrder: Order;

  /** Aux variables */
  auxQ: number;
  auxN: string;
  aviableQuantity: number;

  dispatchSwal = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger',
      closeButton: 'btn btn-secondary'
    },
    buttonsStyling: false
  });

  router: Router;

  Swal = ('sweetalert2');

  public keyword = 'nombre';
  autoCompleteInput : Array<Input> = [];

  ngOnInit(): void {

    this.localUser = JSON.parse(localStorage.getItem('user logged'));

    /** Gets all Orders from API service on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
      }
    );

    /** Get current order from the API service on init */
    this.data.activeOrder.subscribe((order) => {
      this.order = order;

      this.changeAnalysis();

      this.getInputRequestByOrder();

    });

    /** Validates the existance of an order and gets the active order from local storage */
    if (this.order === null) {
      this.order = JSON.parse(localStorage.getItem('active order'));
      console.log('Imprimo: ' + this.order.cliente);
    }

    /** Used to get the analysis of aguardiente from the API service on init */
    this.getInputRequestByOrder();

    /** get Analysis type */
    this.getPQsAnalysis();

    /** get Analysis */
    this.changeAnalysis();

    /** Used to get the active inputs from the API service on init */
    this.apiService.getInputA().subscribe(
      data => {
        this.inputList = data;
        //this.autoCompleteInput = this.inputList;
      }
    );

    /** Used to get all cellar from the API service on init */
    this.getCellarList();
  }

  getCellarList(){
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

  /** Change the analysis or an order by ID using the API service */
  changeAnalysis() {

    if (this.order !== null) {
      this.apiService.getAnalysisByID(this.order.codigo).subscribe(
        data => {
          this.analysisModel = data;
          this.analysisModel.pedCodigo = this.order.codigo;
          this.validateAnalysisExistance();
        }
      );
    }
  }

  /** Used to get a psyco chemical analysis of an order from the API service */
  getPQsAnalysis() {
    this.apiService.getPQAnalsis().subscribe(
      data => {
        this.pqsAnalysisList = data;
      }
    );
  }

  /** Build and post the analysis of an order */
  postAnalysis() {
    this.dispatchSwal.fire({
      title: '¿Desea realizar esta acción?',
      text: 'Esta decisión no es reversible',
      icon: 'warning',
      showCancelButton: true,
      showCloseButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        this.analysisModel.analisisFQs = this.pqsAnalysisList;
        console.log(`ORDERCODE TO ADD${this.order.codigo}`);
        this.analysisModel.pedCodigo = this.order.codigo;
        console.log(`${this.analysisModel}`);
        this.apiService.addAnalysis(this.analysisModel).subscribe(
      data => {
        this.objAnalysis = data;
        if (this.objAnalysis) {
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: '¡Se agregó el análisis con éxito!',
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
        this.changeAnalysis();
      }
    );
        document.getElementById('botonCerrar').click();
        this.analysisModel =  new Analysis(0, 0, 0, 0, 0, 0, '', '', '', '', Array<AnalysisPC>());
      }
    });
  }

  /** Used to validate the existance of an analysis in a specific order */
  validateAnalysisExistance() {

    if (this.analysisModel.fechaEmision !== null) {
      this.analysisExist = true;
    } else {
      this.analysisExist = false;
    }
    console.log(`${this.analysisExist}`);
    return this.analysisExist;
  }

  dropdownReset() {
    (document.getElementById('exaVisualSelect') as HTMLSelectElement).value = '0';
    this.analysisModel.exVisual = 0;
    (document.getElementById('exaOlfaSelect') as HTMLSelectElement).value = '0';
    this.analysisModel.exOlfativo = 0;
    (document.getElementById('exaGustSelect') as HTMLSelectElement).value = '0';
    this.analysisModel.exGustativo = 0;
    (document.getElementById('exaArmoSelect') as HTMLSelectElement).value = '0';
    this.analysisModel.aSensorial = 0;
  }

  /** InputRequest CRUD */
  /** Build and post a input request */
  postInputRequest() {
    this.inputPostRequestModel.operario = this.localUser.correo;
    this.inputPostRequestModel.insumosConsumo = this.inputConsumeList;
    this.inputPostRequestModel.insumosDescarte = this.inputDiscardList;
    this.inputPostRequestModel.codigoPedido = this.order.codigo;
    this.inputPostRequestModel.fecha = '\/Date(928171200000-0600)\/';
    this.inputPostRequestModel.bodega = this.cellarEntryModel.codigo;

    this.apiService.addInputRequest(this.inputPostRequestModel).subscribe(
      data => {
        this.objInputRequest = data;
        this.getInputRequestByOrder();
        if (this.objInputRequest){
          Swal.fire({
            icon: 'success',
            title: '!Listo!',
            text: 'Solicitud enviada con éxito',
            showConfirmButton: false,
            timer: 1500
          });
        } else{
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
    document.getElementById('closeRequestBtn').click();
  }

  resetInputEntryLists(){
    this.aviableQuantity = 1;
    this.inputInDiscardList = false;
    this.inputInConsumeList = false;
    this.inputConsumeList.length = 0;
    this.inputDiscardList.length = 0;
    this.getCellarList();
    this.autoCompleteInput.length = 0;
    this.validateList();
    this.validateDiscarList();
  }

  /** Get the input requests from the API service */
  getInputRequest() {

    this.apiService.getInputRequest().subscribe(
      data => {
        this.inputRequestList = data;
      }
    );
  }

  /** Get the input requests from a user from the API service */
  getInputRequestByUser() {

    this.apiService.getInputRequestByUser(this.userModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }

  /** Get the input requests of an order from the API service */
  getInputRequestByOrder() {
    if (this.order !== null) {
    this.apiService.getInputRequestByOrder(this.order).subscribe(
      data => {
        this.inputRequestListByOrder = data;
      }
    );
    }
  }

  /** Get an input request by ID from the API service */
  searchInputRequest() {

    this.apiService.getInputRequestByID(this.inputRequestModel).subscribe(
      data => {
        this.objInputRequest = data;
      }
    );
  }

  /** Get a cellar by ID from the API service */
  getCellarById(code: number) {

    this.apiService.getOneCellar(code).subscribe(
      data => {
        this.cellarDetailModel = data;
      }
    );
  }

  /** Used to asign a request into an specific order */
  asignRequest(request: InputRequest) {
    this.getCellarList();
    this.invalidRequest = false;
    this.inputRequestModel = request;
    this.getCellarById(this.inputRequestModel.bodega);
    this.searchAvalability(this.inputRequestModel.bodega);
  }

  searchAvalability(cellarId: number){
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

    let  bodega: Cellar;

    for (const bodegaEnLista of this.cellarList){
      if (cellarId === bodegaEnLista.codigo){
        bodega = bodegaEnLista;
      }
    }

    let exist = false;
    for (const insumoConsumidoEnLista of this.inputRequestModel.insumosConsumo){
      for (const insumoEnBodega of bodega.listaInsumosEnBodega){
        if(insumoConsumidoEnLista.insumo.codigo === insumoEnBodega.insumo.codigo){
          exist = true;
        }
      }
    }
    for (const insumoDescartadoEnLista of this.inputRequestModel.insumosDescarte){
      for (const insumoEnBodega of bodega.listaInsumosEnBodega){
        if (insumoDescartadoEnLista.insumo.codigo === insumoEnBodega.insumo.codigo){
          exist = true;
        }
      }
    }

    if(!exist){
      this.invalidRequest = true;
      return;
    }
    
    let totalBothList: Array<InputQ> = [];
    for (const insumoConsumidoEnLista of this.inputRequestModel.insumosConsumo){
      for (const insumoDescartadoEnLista of this.inputRequestModel.insumosDescarte){
        if (insumoConsumidoEnLista.insumo.codigo === insumoDescartadoEnLista.insumo.codigo){
          let tempInputQ = new InputQ(0, new Input(0, '', 0, '', '', ''));
          tempInputQ.cantidadDisponible = insumoConsumidoEnLista.cantidadDisponible+insumoDescartadoEnLista.cantidadDisponible;
          tempInputQ.insumo.cantMinStock = insumoConsumidoEnLista.insumo.cantMinStock;
          tempInputQ.insumo.codigo = insumoConsumidoEnLista.insumo.codigo;
          tempInputQ.insumo.estado = insumoConsumidoEnLista.insumo.estado;
          tempInputQ.insumo.id = insumoConsumidoEnLista.insumo.id;
          tempInputQ.insumo.nombre = insumoConsumidoEnLista.insumo.nombre;
          tempInputQ.insumo.unidad = insumoConsumidoEnLista.insumo.unidad;
          totalBothList.push(tempInputQ);
        }
      }
    }

    for(const inputTotal of totalBothList){
      for (const insumoEnBodega of bodega.listaInsumosEnBodega){
        if (inputTotal.insumo.codigo === insumoEnBodega.insumo.codigo){
          if(inputTotal.cantidadDisponible > insumoEnBodega.cantidadDisponible){
            this.invalidRequest = true;
            return;
          }
        }
      }
    }




    for (const insumoConsumidoEnLista of this.inputRequestModel.insumosConsumo){
      for (const insumoEnBodega of bodega.listaInsumosEnBodega){
        if (insumoConsumidoEnLista.insumo.codigo === insumoEnBodega.insumo.codigo){
          if(insumoConsumidoEnLista.cantidadDisponible > insumoEnBodega.cantidadDisponible){
            this.invalidRequest = true;
            return;
          }
        }
      }
    }

    for (const insumoDescartadoEnLista of this.inputRequestModel.insumosDescarte){
      for (const insumoEnBodega of bodega.listaInsumosEnBodega){
        if (insumoDescartadoEnLista.insumo.codigo === insumoEnBodega.insumo.codigo){
          if (insumoDescartadoEnLista.cantidadDisponible > insumoEnBodega.cantidadDisponible){
            this.invalidRequest = true;
            return;
          }
        }
      }
    }



/*
    for(const j of this.cellarList ){
      if(j.codigo === this.inputRequestModel.bodega){
        for(const i of this.inputRequestModel.insumosConsumo){
          for(const k of j.listaInsumosEnBodega){
            if(k.cantidadDisponible === 0){
              this.invalidRequest = true;
              return;
            }
            if(i.cantidadDisponible > k.cantidadDisponible){
              this.invalidRequest = true;
              return;
            }
          }
        }

        for(const i of this.inputRequestModel.insumosDescarte){
          for(const k of j.listaInsumosEnBodega){
            if(k.cantidadDisponible === 0){
              this.invalidRequest = true;
              return;
            }
            if(i.cantidadDisponible > k.cantidadDisponible){
              this.invalidRequest = true;
              return;
            }
          }
        }
      }
    }
    */
  }
  /** used to search for an input from an specific cellar */
  searchInput() {
    for (const i of this.cellarEntryModel.listaInsumosEnBodega) {
      if (this.searchInputModel.nombre.toUpperCase() === i.insumo.nombre.toUpperCase()) {
        this.inputExist = true;
        this.searchInputModel2 = i.insumo;
        this.aviableQuantity = i.cantidadDisponible;
        this.cellarInputSelected = i;
        return;
      } else {
        this.inputExist = false;
      }
    }
  }

  selectedInput(item){
    this.inputExist = true;
    this.inputInConsumeList = false;
    this.inputInDiscardList = false;
    this.searchInputModel2 = item;
    for (const i of this.cellarEntryModel.listaInsumosEnBodega) {
      if (item.nombre === i.insumo.nombre) {
        this.aviableQuantity = i.cantidadDisponible;
        this.cellarInputSelected = i;
      }
    }

    for (const j of this.inputDiscardList){
      if (j.insumo.codigo === item.codigo){
        this.inputInDiscardList = true;
      }
    }

    for (const k of this.inputConsumeList){
      if (k.insumo.codigo === item.codigo){
        this.inputInConsumeList = true;
      }
    }

    return;
  }

  /** Used to push an input into the entry list */
  pushIntoEntryList() {
    this.inputExist = false;
    this.inputEntryModel.cantidadDisponible = this.auxQ;
    this.inputEntryModel.insumo = this.searchInputModel2;
    this.inputConsumeList.push(this.inputEntryModel);
    this.cellarInputSelected.cantidadDisponible -= this.auxQ;
    this.inputEntryModel = new InputQ(0, this.input);
    this.auxQ = 0;
    this.searchInputModel2 = new Input(0, '', 0, '', '', '');
    this.searchInputModel = new Input(0, '', 0, '', '', '');
    this.validateList();
    this.inputExist = false;
  }

  /** Used to validate the status of the input list */
  validateList() {
    if (this.inputConsumeList.length > 0) {
      this.listIsNotEmpty = true;
    } else {
      this.listIsNotEmpty = false;
    }
  }

  /** Used to remove an input from the consume list  */
  removeFromList(i: number) {
    let consumeInput = this.inputConsumeList.splice(i, 1)[0];
    this.returnInputToCellar(consumeInput);
    this.validateList();
  }

  /** Used to push an input into the discard list */
  pushIntoDiscardList() {
    this.inputEntryModel.cantidadDisponible = this.auxQ;
    this.inputEntryModel.insumo = this.searchInputModel2;
    this.inputDiscardList.push(this.inputEntryModel);
    this.cellarInputSelected.cantidadDisponible -= this.auxQ;
    this.inputEntryModel = new InputQ(0, this.input);
    this.auxQ = 0;
    this.searchInputModel2 = new Input(0, '', 0, '', '', '');
    this.searchInputModel = new Input(0, '', 0, '', '', '');
    this.validateDiscarList();
    this.inputExist = false;
  }

  /** Used to validate the discard list */
  validateDiscarList() {
    if (this.inputDiscardList.length > 0) {
      this.discardListIsNotEmpty = true;
    } else {
      this.discardListIsNotEmpty = false;
    }
  }

  /** Used to remove an input from the discard list */
  removeFromDiscardList(i: number) {
    let discardInput = this.inputDiscardList.splice(i, 1)[0];
    this.returnInputToCellar(discardInput);
    this.validateDiscarList();
  }

  /** Used to return a quantity of a input to the input list of a cellar */
  returnInputToCellar(inpuq: InputQ){
    for (const i of this.cellarEntryModel.listaInsumosEnBodega) {
      if (inpuq.insumo.codigo === i.insumo.codigo) {
        i.cantidadDisponible += inpuq.cantidadDisponible;
      }
    }
    this.auxQ = 0;
    this.searchInputModel2 = new Input(0, '', 0, '', '', '');
    this.searchInputModel = new Input(0, '', 0, '', '', '');
  }

  /** Cellar methods */
  /** Used to validate combo on cellar */
  validateCellar(value) {
    if (value === 'default') {
      this.cellarHasError = true;
    } else {
      this.cellarHasError = false;
      this.searchCellar();
    }
  }

  /** Used to search a cellar */
  searchCellar() {
    this.getCellarList();
    this.aviableQuantity = 1;
    this.autoCompleteInput.length = 0;
    for (const i of this.cellarList) {
      if(this.auxN)
      {
        if (this.auxN.toUpperCase() === i.nombre.toUpperCase()) {
          this.cellarEntryModel = i;
          for (const j of this.cellarEntryModel.listaInsumosEnBodega){
            if(j.insumo.estado.toUpperCase() === 'HABILITADO'){
              this.autoCompleteInput.push(j.insumo);
            }
               
          }
          this.resetInputRequestModal();
          return;
        }
      }
      
    }

    // this.resetInputRequestModal();
  }

  /** Used to request for a status on an input request */
  requestDecision(valueD: string) {

    this.dispatchSwal.fire({
      title: '¿Desea realizar esta acción?',
      text: 'Esta decision no es reversible',
      icon: 'warning',
      showCancelButton: true,
      showCloseButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        this.inputRequestDesicionModel.admin = this.localUser;
        this.inputRequestDesicionModel.solicitud = this.inputRequestModel;
        this.inputRequestDesicionModel.estado = valueD;
        this.apiService.setInputRequestDecision(this.inputRequestDesicionModel).subscribe(
          data => {
            this.objInputRequestDesicion = data;
            this.getInputRequestByOrder();
          }
        );
        this.getCellarList();
      }
    });
  }

  /** Used to reset the input model */
  resetInputRequestModal() {
    this.inputExist = false;
    this.searchInputModel.nombre = '';
    this.inputConsumeList = new Array<InputQ>();
    this.inputDiscardList = Array<InputQ>();
    this.listIsNotEmpty = false;
    this.discardListIsNotEmpty = false;
    this.auxQ = 0;
    this.inputPostRequestModel.notas = '';
  }

  /** Change quantity */
  validateEntryQuantity() {
    if (this.auxQ > this.aviableQuantity) {
      this.auxQ = this.aviableQuantity;
    }
    if (this.auxQ < 0) {
      this.auxQ = 0;
    }
  }

  /** Used to dispatch order with the status "Conforme" or "No conforme"
   * and shows the message to the user.
   */
  dispatchOrder() {

    this.orderModel = this.order;
    this.orderModel.correoAdminDespacho = this.localUser.correo;

    this.dispatchSwal.fire({
      title: '¿Desea despachar este pedido?',
      text: 'Esta decision no es reversible',
      icon: 'warning',
      showCancelButton: true,
      showCloseButton: true,
      confirmButtonText: 'Completo',
      cancelButtonText: 'Deficiente',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        this.orderModel.estado = 'COMPLETO';
        this.apiService.packOff(this.orderModel).subscribe(
          data => {
            this.objOrder = data;
            this.dispatchSwal.fire(
              'Despachado',
              'El pedido ha sido despachado como completo',
              'success'
            );
          }
        );
        this.newOrder();
      } else if (
        result.dismiss === Swal.DismissReason.cancel
      ) {
        this.orderModel.estado = 'DEFICIENTE';
        this.apiService.packOff(this.orderModel).subscribe(
          data => {
            this.objOrder = data;
            this.dispatchSwal.fire(
              'Despachado',
              'El pedido ha sido despachado como deficiente',
              'success'
            );
          }
        );
        this.newOrder();
      }
    });
  }

    newOrder() {
    // window.location.reload();
    this.data.reloadOrderList(true);
    localStorage.setItem('active order', JSON.stringify(this.order));
  }

  navreload() {
    this.navbar.navbarReloadOrder();
  }

}
