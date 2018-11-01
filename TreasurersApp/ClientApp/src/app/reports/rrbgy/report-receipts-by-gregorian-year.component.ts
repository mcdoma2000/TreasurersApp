import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-receipts-by-gregorian-year',
  templateUrl: './report-receipts-by-gregorian-year.component.html',
  styleUrls: ['./report-receipts-by-gregorian-year.component.css']
})
export class ReportReceiptsByGregorianYearComponent implements OnInit {
  reportName = 'receiptsbygregorianyear';
  reportDisplayName = 'Receipts - By Gregorian Year';

  constructor() { }

  ngOnInit() {
  }

  onViewReport() {
    console.log('Attempting to view report: Receipts - By Gregorian Year');
  }

}
