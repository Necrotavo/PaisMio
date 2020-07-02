import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { InputReport } from '../../models/inputReport';
import { InputComparativeReport } from '../../models/inputComparativeReport';
import { OrderReport } from '../../models/orderReport';
import * as jsPDF from 'jspdf';
import { InfoPaisMio } from 'src/models/infoPaisMio';
import { ReportedInput } from 'src/models/reportedInput';
import { Order } from 'src/models/order';
import { InputEntryReport } from 'src/models/inputEntryReport';
import { InputEntryReported } from 'src/models/inputEntryReported';
import { InputCompared } from 'src/models/inputCompared';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.scss']
})
export class ReportViewComponent implements OnInit {
  @ViewChild('pdfInsumos') pdfInsumos: ElementRef;
  @ViewChild('pdfOrderReport') pdfOrderReport: ElementRef;
  @ViewChild('pdfEntryReport') pdfEntryReport: ElementRef;
  @ViewChild('pdfComparativeReport') pdfComparativeReport: ElementRef;

  constructor(private apiService: ApiService) { }
  /** Declarations */
  listReportedInput: Array<ReportedInput> = [];
  orderList: Array<Order> = [];
  entryList: Array<InputEntryReported> = [];
  comparedInputList: Array<InputCompared> = [];

  /** Models */
  inputReport = new InputReport(null, null, '', '');
  inputComparativeReport = new InputComparativeReport(null, null, '', '', '', '');
  entryReport = new InputEntryReport(this.entryList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');
  orderReport = new OrderReport(null, null, '', '');

  /** Auxiliars */
  auxN = 'insumos';
  R1D1 = '';
  R1D2 = '';
  R2D1 = '';
  R2D2 = '';

  /** For combo validation */
  reportHasError = true;


  /** returns */
  objInputReport = new InputReport(this.listReportedInput, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');
  objInputComparativeReport = new InputComparativeReport(this.comparedInputList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '', '', '');
  objOrderReport = new OrderReport(this.orderList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');
  objEntryReport = new InputEntryReport(this.entryList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');

  ngOnInit(): void {
  }

  getInputReport() {
    this.inputReport.fechaInicio = this.R1D1;
    this.inputReport.fechaFinal = this.R1D2;

    this.apiService.getInputReport(this.inputReport).subscribe(
      data => {
        this.objInputReport = data;
      }
    );
  }

  getOrderReport() {
    this.orderReport.fechaInicio = this.R1D1;
    this.orderReport.fechaFinal = this.R1D2;

    this.apiService.getOrderReport(this.orderReport).subscribe(
      data => {
        this.objOrderReport = data;
      }
    );
  }

  getEntryReport() {
    this.entryReport.fechaInicio = this.R1D1;
    this.entryReport.fechaFinal = this.R1D2;

    this.apiService.getEntryReport(this.entryReport).subscribe(
      data => {
        this.objEntryReport = data;
      }
    );
  }

  getInputComparativeReport() {
    this.inputComparativeReport.inicioMes1 = this.R1D1;
    this.inputComparativeReport.finalMes1 = this.R1D2;
    this.inputComparativeReport.inicioMes2 = this.R2D1;
    this.inputComparativeReport.finalMes2 = this.R2D2;

    this.apiService.getInputComparativeReport(this.inputComparativeReport).subscribe(
      data => {
        this.objInputComparativeReport = data;
      }
    );
  }

  validateReportType(value) {
    if (value === 'default') {
      this.reportHasError = true;
    } else {
      this.reportHasError = false;
    }
  }

  downloadPDF() {

    const element = document.getElementById('canvasInputReport');
    html2canvas(element).then((canvas) => {

      var imgData = canvas.toDataURL('image/png');
      var imgWidth = 150;
      var pageHeight = 285;
      var imgHeight = canvas.height * imgWidth / canvas.width;
      var heightLeft = imgHeight;
      var doc = new jsPDF('p', 'mm');
      var position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Insumos.pdf');

    })
  }
/*
  openPDF(): void {
    const DATA = this.pdfInsumos.nativeElement;
    const doc = new jsPDF('p', 'pt', 'a4');
    doc.fromHTML(DATA.innerHTML, 15, 15);
    doc.output('dataurlnewwindow');
  }
*/
  openOrderPDF(): void {
    const element = document.getElementById('pdfOrderReport');
    html2canvas(element).then((canvas) => {

      var imgData = canvas.toDataURL('image/png');
      var imgWidth = 150;
      var pageHeight = 300;
      var imgHeight = (canvas.height * imgWidth / canvas.width);
      var heightLeft = imgHeight;
      var doc = new jsPDF('p', 'mm');
      var position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Ordenes.pdf');

    })
    /*
    const DATA = this.pdfOrderReport.nativeElement;
    const doc = new jsPDF('p', 'pt', 'a4');
    doc.fromHTML(DATA.innerHTML, 15, 15);
    doc.output('dataurlnewwindow');
    */
  }

  openEntryPDF(): void {
    const element = document.getElementById('pdfEntryReport');
    html2canvas(element).then((canvas) => {

      var imgData = canvas.toDataURL('image/png');
      var imgWidth = 150;
      var pageHeight = 300;
      var imgHeight = canvas.height * imgWidth / canvas.width;
      var heightLeft = imgHeight;
      var doc = new jsPDF('p', 'mm');
      var position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Entradas.pdf');

    })
    /*
    const DATA = this.pdfEntryReport.nativeElement;
    const doc = new jsPDF('p', 'pt', 'a4');
    doc.fromHTML(DATA.innerHTML, 15, 15);
    doc.output('dataurlnewwindow');
    */
  }

  openComparativePDF(): void {
    const element = document.getElementById('pdfComparativeReport');
    html2canvas(element).then((canvas) => {

      var imgData = canvas.toDataURL('image/png');
      var imgWidth = 150;
      var pageHeight = 300;
      var imgHeight = canvas.height * imgWidth / canvas.width;
      var heightLeft = imgHeight;
      var doc = new jsPDF('p', 'mm');
      var position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte comparativo.pdf');

    })

    /*
    const DATA = this.pdfComparativeReport.nativeElement;
    const doc = new jsPDF('p','pt', 'a4');
    doc.fromHTML(DATA.innerHTML,15,15);
    doc.output('dataurlnewwindow');
    */
  }
}
