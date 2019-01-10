import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { EmailAddress } from '../../models/EmailAddress';
import { EmailService } from './email.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { EmailAddressRequest } from 'src/app/models/EmailAddressRequest';
import { SecurityService } from 'src/app/security/security.service';

@Component({
  selector: 'app-email-maintenance',
  templateUrl: './email-maintenance.component.html',
  styleUrls: ['./email-maintenance.component.css']
})
export class EmailMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: EmailAddress[] = [];
  foundEmail: EmailAddress = null;
  rowIsSelected = false;
  selectedEmail = new EmailAddress(0, null, null, null, null, null);
  emailToEdit = new EmailAddress(0, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private emailService: EmailService,
              private securityService: SecurityService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.emailService.getEmails(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addEmail() {
    this.displayAdd = true;
    this.emailToEdit = this.emailService.newEmailAddress();
  }

  editEmail() {
    this.displayEdit = true;
    this.emailToEdit =
      new EmailAddress(this.selectedEmail.id, this.selectedEmail.email, this.selectedEmail.createdBy,
        this.selectedEmail.createdDate, this.selectedEmail.lastModifiedBy, this.selectedEmail.lastModifiedDate);
    this.confirmUpdate();
  }

  deleteEmail() {
    this.emailToEdit =
      new EmailAddress(this.selectedEmail.id, this.selectedEmail.email, this.selectedEmail.createdBy,
        this.selectedEmail.createdDate, this.selectedEmail.lastModifiedBy, this.selectedEmail.lastModifiedDate);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Maintenance: Delete',
      detail: 'Deleting email...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.emailService.deleteEmail(this.emailToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Email Maintenance: Delete',
              detail: 'An error occurred while attempting to delete an email.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Maintenance: Add',
      detail: 'Adding email...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new EmailAddressRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.emailToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.emailService.addEmail(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Maintenance: Update',
      detail: 'Updating email...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new EmailAddressRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.emailToEdit;
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.emailService.updateEmail(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: EmailMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.emailService.getEmails(true).subscribe(
      (resp) => {
        this.rowData = resp;
        setTimeout(this.refreshData, 0, this);
      },
      (err) => {
        console.log(err);
        this.rowData = [];
        setTimeout(this.refreshData, 0, this);
      }
    );
  }

  showConfirmation(message: string, header: string, acceptLabel: string, confirmationMessage: ConfirmationMessage, callback: () => void) {
    this.confirmationService.confirm({
      message: message,
      header: header,
      icon: 'pi pi-info-circle',
      acceptLabel: acceptLabel,
      rejectLabel: 'Cancel',
      accept: () => {
        this.messageService.add(confirmationMessage);
        callback();
      },
      reject: () => {
        console.log('Rejected Update');
      }
    });
  }

  editAddress() {
    this.displayEdit = true;
    this.emailToEdit =
      new EmailAddress(this.selectedEmail.id, this.selectedEmail.email, this.selectedEmail.createdBy,
        this.selectedEmail.createdDate, this.selectedEmail.lastModifiedBy, this.selectedEmail.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Email Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Email Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.emailService.validateEmail(this.emailToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if ( this.selectedEmail.id && this.selectedEmail.email !== this.emailToEdit.email) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.emailToEdit = this.emailService.newEmailAddress();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.emailToEdit = this.emailService.newEmailAddress();
  }

  onRowClick(e) {
    this.selectedEmail = new EmailAddress(
      e.data.id,
      e.data.email,
      e.data.createdBy,
      e.data.createdDate,
      e.data.lastModifiedBy,
      e.data.lastModifiedDate);
    this.rowIsSelected = true;
  }

  validateEmail(email: EmailAddress): boolean {
    if (!email.email) {
      return false;
    }
    return true;
  }

  getEmailById(id: number): Observable<EmailAddress> {
    if (this.rowData.length > 0) {
      this.foundEmail = this.rowData.find(x => x.id === id);
    }
    if (this.foundEmail) {
      return of(this.foundEmail);
    } else {
      return this.emailService.getEmailById(id);
    }
  }

  getEmails(forceReload: boolean = false): Observable<EmailAddress[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.emailService.getEmails().subscribe(
        (resp) => {
          this.rowData = resp;
          return of(this.rowData);
        },
        (err) => {
          console.log(err);
          this.rowData = [];
          return of(this.rowData);
        }
      );
    }
  }
}
