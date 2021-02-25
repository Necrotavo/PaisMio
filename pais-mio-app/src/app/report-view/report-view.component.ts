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
  date1HasError = true;
  date2HasError = true;
  date3HasError = true;

  /** returns */
  objInputReport = new InputReport(this.listReportedInput, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');
  objInputComparativeReport = new InputComparativeReport(this.comparedInputList,
                              new InfoPaisMio(0, '', '', '', '', '', ''), '', '', '', '');
  objOrderReport = new OrderReport(this.orderList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');
  objEntryReport = new InputEntryReport(this.entryList, new InfoPaisMio(0, '', '', '', '', '', ''), '', '');

  ngOnInit(): void {
  }

  /** Used to get a list of input reports using the API service */
  getInputReport() {
    this.inputReport.fechaInicio = this.R1D1;
    this.inputReport.fechaFinal = this.R1D2;

    this.apiService.getInputReport(this.inputReport).subscribe(
      data => {
        this.objInputReport = data;
      }
    );
  }

  /** Used to get a list of order reports using the API service */
  getOrderReport() {
    this.orderReport.fechaInicio = this.R1D1;
    this.orderReport.fechaFinal = this.R1D2;

    this.apiService.getOrderReport(this.orderReport).subscribe(
      data => {
        this.objOrderReport = data;
      }
    );
  }

  /** Used to get a list of entry reports using the API service */
  getEntryReport() {
    this.entryReport.fechaInicio = this.R1D1;
    this.entryReport.fechaFinal = this.R1D2;

    this.apiService.getEntryReport(this.entryReport).subscribe(
      data => {
        this.objEntryReport = data;
      }
    );
  }

  /** Used to get a list of comparative reports using the API service */
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

  /** Used to validate the report type when selected */
  validateReportType(value) {
    if (value === 'default') {
      this.reportHasError = true;
    } else {
      this.reportHasError = false;
    }
  }

  /** Used to validate the first date of a new report when selected */
  validateFirstDates(){
    if (this.R1D1 > this.R1D2){
      this.date1HasError = true;
    }else{
      this.date1HasError = false;
    }
  }

  /** Used to validate the second date of a new report when selected */
  validateSecondDates(){
    if (this.R2D1 > this.R2D2){
      this.date2HasError = true;
    }else{
      this.date2HasError = false;
    }
  }

  /** Used to validate the dates of a comparative report */
  validateComparativeDates(){
    if (this.R1D2 === '' || this.R2D1 === ''){
      this.date3HasError = true;
    } else{
      if (this.date1HasError === false && this.date2HasError === false){
        if (this.R1D2 < this.R2D1){
          this.date3HasError = false;
        }else{
          this.date3HasError = true;
        }
      } else {
        this.date3HasError = true;
      }
    }
  }

  /** Used to call canvas and jsPDF to download a report as PDF */
  downloadPDF() {

    const element = document.getElementById('canvasInputReport');
    html2canvas(element).then((canvas) => {

      const imgData = canvas.toDataURL('image/png');
      const imgWidth = 150;
      const pageHeight = 285;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      let heightLeft = imgHeight;
      const doc = new jsPDF('p', 'mm');
      let position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Insumos.pdf');

    });
  }

  /** Used to open a PDF order report */
  openOrderPDF(): void {
    const element = document.getElementById('pdfOrderReport');
    html2canvas(element).then((canvas) => {

      const imgData = canvas.toDataURL('image/png');
      const imgWidth = 150;
      const pageHeight = 300;
      const imgHeight = (canvas.height * imgWidth / canvas.width);
      let heightLeft = imgHeight;
      const doc = new jsPDF('p', 'mm');
      let position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Ordenes.pdf');

    });

  }

  /** Used to open a PDF entry report */
  openEntryPDF(): void {
    const element = document.getElementById('pdfEntryReport');
    html2canvas(element).then((canvas) => {

      const imgData = canvas.toDataURL('image/png');
      const imgWidth = 150;
      const pageHeight = 300;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      let heightLeft = imgHeight;
      const doc = new jsPDF('p', 'mm');
      let position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte de Entradas.pdf');

    });

  }

  /** Used to open a PDF comparative report */
  openComparativePDF(): void {
    const element = document.getElementById('pdfComparativeReport');
    html2canvas(element).then((canvas) => {

      const imgData = canvas.toDataURL('image/png');
      const imgWidth = 150;
      const pageHeight = 300;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      let heightLeft = imgHeight;
      const doc = new jsPDF('p', 'mm');
      let position = 0;

      doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;

      while (heightLeft >= 0) {
        position = heightLeft - imgHeight;
        doc.addPage();
        doc.addImage(imgData, 'PNG', 25, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;
      }
      doc.save('Reporte comparativo.pdf');

    });

  }
}
