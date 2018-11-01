import { Component, OnInit, Input } from '@angular/core';

import * as moment from 'moment';
import { ReportParameters } from '../models/ReportParameters';
import { ContributorService } from '../maintenance/contributor-maintenance/contributor.service';
import { SelectItem } from '../../../node_modules/primeng/components/common/selectitem';

@Component({
  selector: 'app-standard-report-form',
  templateUrl: './standard-report-form.component.html',
  styleUrls: ['./standard-report-form.component.css']
})
export class StandardReportFormComponent implements OnInit {
  @Input() reportName: string;
  contributors: SelectItem[] = [];
  minGregorianYear = moment().add(-10, 'years').year();
  maxGregorianYear = moment().add(20, 'years').year();
  displayGregorianYear = true;
  displayContributors = true;
  displayStartEndDate = true;
  reportParameters: ReportParameters = new ReportParameters();

  constructor(private contributorService: ContributorService) {}

  ngOnInit() {
    // TODO: pull this information from the database
    switch (this.reportName) {
      case 'cashjournal':
        // this.displayGregorianYear = false;
        // this.displayContributors = false;
        // this.displayStartEndDate = true;
        break;
      case 'cashjournalbycontributor':
        // this.displayGregorianYear = false;
        // this.displayContributors = true;
        // this.displayStartEndDate = true;
        break;
      case 'cashjournalbydaterange':
        // this.displayGregorianYear = false;
        // this.displayContributors = false;
        // this.displayStartEndDate = true;
        break;
      case 'receiptsbycontribution':
        // this.displayGregorianYear = false;
        // this.displayContributors = false;
        // this.displayStartEndDate = true;
        break;
      case 'receiptsbygregorianyear':
        // this.displayGregorianYear = true;
        // this.displayContributors = true;
        // this.displayStartEndDate = true;
        break;
      case 'voidedchecks':
        // this.displayGregorianYear = true;
        // this.displayContributors = false;
        // this.displayStartEndDate = false;
        break;
    }
    this.reportParameters.startDate = moment().startOf('month').toDate();
    this.reportParameters.endDate = moment().endOf('month').toDate();
    this.reportParameters.gregorianYear = moment().year();
    this.contributorService.getContributors().subscribe(
      resp => {
        resp.forEach(contributor => {
          this.contributors.push({
            label: contributor.firstName + ' ' + contributor.lastName,
            value: contributor
          });
        });
      },
      err => {
        console.log(err);
        this.contributors = [];
      }
    );
  }
}
