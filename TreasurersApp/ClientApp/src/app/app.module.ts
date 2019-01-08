import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { MenubarModule } from 'primeng/menubar';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext'
import { DialogModule } from 'primeng/dialog';
import { SliderModule } from 'primeng/slider';
import { DataViewModule } from 'primeng/dataview';
import { DataGridModule } from 'primeng/datagrid';
import { ButtonModule } from 'primeng/button';
import { BlockUIModule } from 'primeng/blockui';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { DataTableModule } from 'primeng/datatable';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations/';

import { DxDataGridModule } from 'devextreme-angular/ui/data-grid';
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
import { StandardReportFormComponent } from './standard-report-form/standard-report-form.component';
import { AddressMaintenanceComponent } from './maintenance/address-maintenance/address-maintenance.component';
import { ContributorMaintenanceComponent } from './maintenance/contributor-maintenance/contributor-maintenance.component';
import { TransactionCategoryMaintenanceComponent } from './maintenance/transaction-category-maintenance/transaction-category-maintenance.component';
import { TransactionTypeMaintenanceComponent } from './maintenance/transaction-type-maintenance/transaction-type-maintenance.component';
import { PhoneMaintenanceComponent } from './maintenance/phone-maintenance/phone-maintenance.component';
import { EmailMaintenanceComponent } from './maintenance/email-maintenance/email-maintenance.component';
import { AddressTypeMaintenanceComponent } from './maintenance/address-type-maintenance/address-type-maintenance.component';
import { PhoneTypeMaintenanceComponent } from './maintenance/phone-type-maintenance/phone-type-maintenance.component';
import { EmailTypeMaintenanceComponent } from './maintenance/email-type-maintenance/email-type-maintenance.component';

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
    ReportReceiptsByGregorianYearComponent,
    StandardReportFormComponent,
    ContributorMaintenanceComponent,
    TransactionCategoryMaintenanceComponent,
    TransactionTypeMaintenanceComponent,
    AddressMaintenanceComponent,
    AddressTypeMaintenanceComponent,
    PhoneMaintenanceComponent,
    PhoneTypeMaintenanceComponent,
    EmailMaintenanceComponent,
    EmailTypeMaintenanceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    FormsModule,
    HttpClientModule,
    HttpInterceptorModule,
    CalendarModule,
    ButtonModule,
    MultiSelectModule,
    MenubarModule,
    InputMaskModule,
    InputTextModule,
    ToastModule,
    DialogModule,
    ConfirmDialogModule,
    BlockUIModule,
    SliderModule,
    DataViewModule,
    DataGridModule,
    DataTableModule,
    DxDataGridModule,
    DropdownModule,
    CheckboxModule,
    BrowserAnimationsModule
  ],
  providers: [
    SecurityService,
    AuthGuard,
    LoginComponent,
    ConfirmationService,
    MessageService,
    BrowserAnimationsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
