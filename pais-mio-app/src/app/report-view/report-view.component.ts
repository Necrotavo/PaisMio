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

<<<<<<< HEAD

  /** Models */
  inputReport = new InputReport(new ReportedInput[0], new InfoPaisMio(0,'','','','','',''), '','');
  inputComparativeReport = new InputComparativeReport(new InputCompared[0], new InfoPaisMio(0,'','','','','',''),'','','','');
  orderReport = new OrderReport(new Order[0],new InfoPaisMio(0,'','','','','',''),0,0);

  /** Auxiliars */
  auxN : string;
=======
  /** Auxiliar */
  auxN: string;

  /** For combo validation */
  reportHasError = true;

>>>>>>> master

  /** returns */
  objInputReport: InputReport;
  objInputComparativeReport: InputComparativeReport;
  objOrderReport: OrderReport;

  ngOnInit(): void {
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
