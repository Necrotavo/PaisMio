import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { InputReport } from '../../models/inputReport';
import { InputComparativeReport } from '../../models/inputComparativeReport';
import { OrderReport } from '../../models/orderReport';
import * as jsPDF from 'jspdf';
import { InfoPaisMio } from 'src/models/infoPaisMio';
import { ReportedInput } from 'src/models/reportedInput';


@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.scss']
})
export class ReportViewComponent implements OnInit {
  @ViewChild('pdfReport') pdfReport: ElementRef;

  constructor(private apiService: ApiService) { }
  /** Declarations */
  listReportedInput: Array<ReportedInput> = [];

  /** Models */
  inputReport = new InputReport(null, null, '', '');
  inputComparativeReport = new InputComparativeReport(null, null, '', '', '', '');
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
  objInputReport = new InputReport(this.listReportedInput, new InfoPaisMio(0,'','','','','',''), '','');
  objInputComparativeReport: InputComparativeReport;
  objOrderReport: OrderReport;

  ngOnInit(): void {
  }

  getInputReport(){
    this.inputReport.fechaInicio = this.R1D1;
    this.inputReport.fechaFinal = this.R1D2;

    this.apiService.getInputReport(this.inputReport).subscribe(
      data => {
        this.objInputReport = data;
      }
    );
  }

  getOrderReport(){
    this.orderReport.fechaInicio = this.R1D1;
    this.orderReport.fechaFinal = this.R1D2;

    this.apiService.getOrderReport(this.orderReport).subscribe(
      data => {
        this.objOrderReport = data;
      }
    );
  }



  getInputComparativeReport(){
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


  downloadPDF() {

      const DATA = this.pdfReport.nativeElement;
      const doc = new jsPDF('p', 'pt', 'a4');
      const handleElement = {
        '#editor': (element, renderer) => {
          return true;
        }
      };
      doc.fromHTML(DATA.innerHTML, 15, 15, {
        width: 200,
        elementHandlers: handleElement
      });

      doc.save('angular-demo.pdf');

    }

  validateReportType(value){
    if (value === 'default'){
      this.reportHasError = true;
    } else {
      this.reportHasError = false;
    }
  }

}
