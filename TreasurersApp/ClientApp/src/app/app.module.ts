import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CashJournalComponent } from './cashjournal/cash-journal.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './security/auth.guard';
import { HttpInterceptorModule } from './security/http-interceptor.module';
import { HasClaimDirective } from './security/has-claim.directive';
import { SecurityService } from './security/security.service';
import { ReportsComponent } from './reports/reports.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { ReportCashJournalComponent } from './reports/rcj/report-cash-journal.component';
import { ReportCashJournalByDateRangeComponent } from './reports/rcjbdr/report-cash-journal-by-date-range.component';
import { ReportCashJournalByContributorComponent } from './reports/rcjbc/report-cash-journal-by-contributor.component';
import { ReportVoidedChecksComponent } from './reports/rvc/report-voided-checks.component';
import { ReportReceiptsByContributionComponent } from './reports/rrbc/report-receipts-by-contribution.component';
import { ReportReceiptsByGregorianYearComponent } from './reports/rrbgy/report-receipts-by-gregorian-year.component';


@NgModule({
  declarations: [
    AppComponent,
    CashJournalComponent,
    DashboardComponent,
    LoginComponent,
    HasClaimDirective,
    ReportsComponent,
    MaintenanceComponent,
    ReportCashJournalComponent,
    ReportCashJournalByDateRangeComponent,
    ReportCashJournalByContributorComponent,
    ReportVoidedChecksComponent,
    ReportReceiptsByContributionComponent,
    ReportReceiptsByGregorianYearComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    FormsModule,
    HttpClientModule,
    HttpInterceptorModule
  ],
  providers: [SecurityService, AuthGuard, LoginComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
