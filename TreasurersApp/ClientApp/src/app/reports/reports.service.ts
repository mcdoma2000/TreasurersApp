import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Report } from './Report';
import { ReportParameters } from './ReportParameters';
import { ReportStatus } from './ReportStatus';

const API_URL = 'http://localhost:5000/api/reports/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ReportsService implements OnInit, OnDestroy {
  reportList: Report[] = null;
  reportSub = null;
  executeReportSub = null;
  reportStatus: ReportStatus = new ReportStatus();
  reportName = '';

  constructor(private http: HttpClient) {
  }

  // NOTE: This must be called with a target of _blank so that it opens in a new browser window
  executeReport(reportParameters: ReportParameters): Observable<ReportStatus> {
    console.log('ReportService::executeReport');
    console.log(reportParameters);
    this.reportName = reportParameters.reportName;

    this.executeReportSub = this.http.post<ReportStatus>(API_URL + '/ExecuteReport', reportParameters, httpOptions).subscribe(
      (results) => {
        console.log('executeReport results: ');
        console.log(results);
        this.reportStatus.success = true;
        this.reportStatus.messages = [ 'Report: ' + this.reportName + ' was successfully processed.' ];
      },
      error => {
        this.reportStatus.success = false;
        this.reportStatus.messages = [ 'Report: ' + this.reportName + ' failed to process.' ];
        this.handleError(error);
      }
    );
    return of(this.reportStatus);
  }

  ngOnInit(): void {
    console.log('ReportsService::ngOnInit');
    this.reportSub = this.http.get<Report[]>(API_URL, httpOptions).subscribe(data => {
      console.log(data);
      this.reportList = data;
    });
  }

  ngOnDestroy(): void {
    console.log('ReportsService::ngOnDestroy');
    if (this.reportSub) {
      this.reportSub.unsubscribe();
    }
    if (this.executeReportSub) {
      this.executeReportSub.unsubscribe();
    }
  }

  getReportsList() {
    if (this.reportList && this.reportList.length > 0) {
      return of(this.reportList);
    } else {
      return this.http.get<Report[]>(API_URL, httpOptions)
        .pipe(
          retry(3),
          catchError(this.handleError)
        );
    }
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
    }
  }
