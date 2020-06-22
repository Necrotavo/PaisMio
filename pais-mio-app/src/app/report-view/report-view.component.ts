import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.scss']
})
export class ReportViewComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  /** Auxiliar */
  auxN : string;

  ngOnInit(): void {
  }

  downloadPDF() {
    /* Report logic */
  }

}
