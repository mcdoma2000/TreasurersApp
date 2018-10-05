import { Component, OnInit } from '@angular/core';

import { ReportsService } from '../reports.service';
import { Report } from '../Report';
import { ReportStatus } from '../ReportStatus';

@Component({
  selector: 'app-report-cash-journal-by-date-range',
  templateUrl: './report-cash-journal-by-date-range.component.html',
  styleUrls: ['./report-cash-journal-by-date-range.component.css']
})
export class ReportCashJournalByDateRangeComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
