import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { CashJournalComponent } from './cashjournal/cash-journal.component';
import { LoginComponent } from './login/login.component';
import { ReportsComponent } from './reports/reports.component';
import { ReportCashJournalComponent } from './reports/rcj/report-cash-journal.component';
import { ReportCashJournalByContributorComponent } from './reports/rcjbc/report-cash-journal-by-contributor.component';
import { ReportCashJournalByDateRangeComponent } from './reports/rcjbdr/report-cash-journal-by-date-range.component';
import { ReportVoidedChecksComponent } from './reports/rvc/report-voided-checks.component';
import { ReportReceiptsByContributionComponent } from './reports/rrbc/report-receipts-by-contribution.component';
import { ReportReceiptsByGregorianYearComponent } from './reports/rrbgy/report-receipts-by-gregorian-year.component';
import { AuthGuard } from './security/auth.guard';
import { MaintenanceComponent } from './maintenance/maintenance.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'cashjournal',
    component: CashJournalComponent,
    canActivate: [AuthGuard],
    data: { claimType: 'canAccessCashJournal' }
  },
  {
    path: 'reports',
    component: ReportsComponent,
    canActivate: [AuthGuard],
    data: { claimType: 'canAccessReports' }
  },
  {
    path: 'reports/cashjournal',
    component: ReportCashJournalComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'Cash Journal' }
  },
  {
    path: 'reports/cashjournalbydaterange',
    component: ReportCashJournalByDateRangeComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'Cash Journal - By Date Range' }
  },
  {
    path: 'reports/cashjournalbycontributor',
    component: ReportCashJournalByContributorComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'CashJournal - By Contributor' }
  },
  {
    path: 'reports/voidedchecks',
    component: ReportVoidedChecksComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'Voided Checks' }
  },
  {
    path: 'reports/receiptsbycontribution',
    component: ReportReceiptsByContributionComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'Receipts - By Contribution' }
  },
  {
    path: 'reports/receiptsbygregorianyear',
    component: ReportReceiptsByGregorianYearComponent,
    data: { claimType: 'canAccessReports', reportDisplayName: 'Receipts - By Gregorian Year' }
  },
  {
    path: 'maintenance',
    component: MaintenanceComponent,
    canActivate: [AuthGuard],
    data: { claimType: 'canPerformAdmin'}
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { methodName: 'login'}
  },
  {
    path: 'logout',
    component: LoginComponent,
    data: { methodName: 'logout' }
  },
  {
    path: '', redirectTo: 'dashboard', pathMatch: 'full'
  },
  {
    path: '**', component: DashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
