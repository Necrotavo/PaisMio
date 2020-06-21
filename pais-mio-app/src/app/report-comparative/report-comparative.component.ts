import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-comparative',
  templateUrl: './report-comparative.component.html',
  styleUrls: ['./report-comparative.component.scss']
})
export class ReportComparativeComponent implements OnInit {

  years: Array<number>;
  months: Array<string>;

  ngOnInit() {
    this.years = [ 2018, 2019, 2020, 2021, 2022, 2023, 2024 ];
    this.months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
  }

  constructor() { }

}
