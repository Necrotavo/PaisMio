import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { InputReport } from '../../models/inputReport';
import { InputComparativeReport } from '../../models/inputComparativeReport';
import { OrderReport } from '../../models/orderReport';
import { InfoPaisMio } from '../../models/infoPaisMio';
import { ReportedInput } from '../../models/reportedInput';
import { InputCompared } from '../../models/inputCompared';
import { Order } from '../../models/order';

@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.scss']
})
export class ReportViewComponent implements OnInit {

  constructor(private apiService: ApiService) { }
  /** Declarations */


  /** Models */
  inputReport = new InputReport(null, null, '','');
  inputComparativeReport = new InputComparativeReport(null, null,'','','','');
  orderReport = new OrderReport(null,null,'','');

  /** Auxiliars */
  auxN = 'insumos';
  R1D1 = '';
  R1D2 = '';
  R2D1 = '';
  R2D2 = '';

  /** For combo validation */
  reportHasError = true;


  /** returns */
  objInputReport: InputReport;
  objInputComparativeReport: InputComparativeReport;
  objOrderReport: OrderReport;

  ngOnInit(): void {
  }

  getReport(){
    if(this.auxN === 'insumos'){
      this.inputReport.fechaInicio = this.R1D1;
      this.inputReport.fechaFinal = this.R1D2;
      this.getInputReport();
    }
    if(this.auxN === 'pedidos'){
      this.orderReport.fechaInicio = this.R1D1;
      this.orderReport.fechaFinal = this.R1D2;
      this.getOrderReport();
    }
    if(this.auxN === 'entrada'){

    }
    if(this.auxN === 'comparativo'){
      this.inputComparativeReport.inicioMes1 = this.R1D1;
      this.inputComparativeReport.finalMes1 = this.R1D2;
      this.inputComparativeReport.inicioMes2 = this.R2D1;
      this.inputComparativeReport.finalMes2 = this.R2D2;
      this.getInputComparativeReport();
    }
  }
  getInputReport(){
    this.apiService.getInputReport(this.inputReport).subscribe(
      data => {
        this.objInputReport = data;
      }
    );
  }

  getOrderReport(){
    this.apiService.getOrderReport(this.orderReport).subscribe(
      data => {
        this.objOrderReport = data;
      }
    );
  }


  getInputComparativeReport(){
    this.apiService.getInputComparativeReport(this.inputComparativeReport).subscribe(
      data => {
        this.objInputComparativeReport = data;
      }
    );
  }


  downloadPDF() {
    /* Report logic */
  }

  validateReportType(value){
    if (value === 'default'){
      this.reportHasError = true;
    } else {
      this.reportHasError = false;
    }
  }

}
