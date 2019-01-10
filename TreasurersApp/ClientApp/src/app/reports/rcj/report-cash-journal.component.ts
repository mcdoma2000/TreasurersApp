import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ReportParameters } from '../../models/ReportParameters';
import { ReportsService } from '../reports.service';
import { StandardReportFormComponent } from '../../standard-report-form/standard-report-form.component';

@Component({
  selector: 'app-report-cash-journal',
  templateUrl: './report-cash-journal.component.html',
  styleUrls: ['./report-cash-journal.component.css']
})
export class ReportCashJournalComponent implements OnInit, AfterViewInit {

  @ViewChild(StandardReportFormComponent)
  stdRptForm: StandardReportFormComponent;

  reportParameters = '';

  constructor(private reportsService: ReportsService) {
  }

  ngAfterViewInit(): void {
    console.log(this.stdRptForm);
  }

  ngOnInit() {
    if (this.stdRptForm) {
      console.log('Standard Report Form is present in OnInit');
    } else {
      console.log('Standard Report Form is NOT present in OnInit');
    }
  }

  executeReport(): void {
    this.stdRptForm.reportParameters.reportName = this.stdRptForm.reportName;
    this.reportParameters = JSON.stringify(this.stdRptForm.reportParameters);
    console.log(this.reportParameters);
    const rptPrm: HTMLElement = document.getElementById('paramJson') as HTMLElement;
    rptPrm.setAttribute('value', this.reportParameters);
    const element: HTMLElement = document.getElementById('submitButton') as HTMLElement;
    element.click();
  }
}
