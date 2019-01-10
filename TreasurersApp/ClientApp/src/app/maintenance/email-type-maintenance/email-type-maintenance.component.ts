import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { EmailType } from '../../models/EmailType';
import { EmailTypeService } from './email-type.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { SecurityService } from 'src/app/security/security.service';
import { EmailTypeRequest } from 'src/app/models/EmailTypeRequest';

@Component({
  selector: 'app-email-type-maintenance',
  templateUrl: './email-type-maintenance.component.html',
  styleUrls: ['./email-type-maintenance.component.css']
})
export class EmailTypeMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: EmailType[] = [];
  foundEmail: EmailType = null;
  rowIsSelected = false;
  selectedEmailType = new EmailType(0, null, null, null, null, null, null, null);
  emailTypeToEdit = new EmailType(0, null, null, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private emailTypeService: EmailTypeService,
              private securityService: SecurityService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.emailTypeService.getEmailTypes(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addEmailType() {
    this.displayAdd = true;
    this.emailTypeToEdit = this.emailTypeService.newEmailType();
  }

  editEmailType() {
    this.displayEdit = true;
    this.emailTypeToEdit =
      new EmailType(
        this.selectedEmailType.id,
        this.selectedEmailType.name,
        this.selectedEmailType.description,
        this.selectedEmailType.active,
        this.selectedEmailType.createdBy,
        this.selectedEmailType.createdDate,
        this.selectedEmailType.lastModifiedBy,
        this.selectedEmailType.lastModifiedDate);
    this.confirmUpdate();
  }

  deleteEmailType() {
    this.emailTypeToEdit =
    new EmailType(
      this.selectedEmailType.id,
      this.selectedEmailType.name,
      this.selectedEmailType.description,
      this.selectedEmailType.active,
      this.selectedEmailType.createdBy,
      this.selectedEmailType.createdDate,
      this.selectedEmailType.lastModifiedBy,
      this.selectedEmailType.lastModifiedDate);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Type Maintenance: Delete',
      detail: 'Deleting email type...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.emailTypeService.deleteEmailType(this.emailTypeToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Type Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Email Type Maintenance: Delete',
              detail: 'An error occurred while attempting to delete an email type.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Type Maintenance: Add',
      detail: 'Adding email type...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new EmailTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.emailTypeToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.emailTypeService.addEmailType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Type Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Email Type Maintenance: Update',
      detail: 'Updating email...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new EmailTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.emailTypeToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.emailTypeService.updateEmailType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Email Type Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Email Type Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: EmailTypeMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.emailTypeService.getEmailTypes(true).subscribe(
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
    this.emailTypeToEdit =
    new EmailType(
      this.selectedEmailType.id,
      this.selectedEmailType.name,
      this.selectedEmailType.description,
      this.selectedEmailType.active,
      this.selectedEmailType.createdBy,
      this.selectedEmailType.createdDate,
      this.selectedEmailType.lastModifiedBy,
      this.selectedEmailType.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Email Type Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Email Type Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.emailTypeService.validateEmailType(this.emailTypeToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedEmailType.id &&
          (this.selectedEmailType.name !== this.emailTypeToEdit.name ||
           this.selectedEmailType.description !== this.emailTypeToEdit.description ||
           this.selectedEmailType.active !== this.emailTypeToEdit.active)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.emailTypeToEdit = this.emailTypeService.newEmailType();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.emailTypeToEdit = this.emailTypeService.newEmailType();
  }

  onRowClick(e) {
    this.selectedEmailType = new EmailType(
      e.data.id,
      e.data.name,
      e.data.description,
      e.data.active,
      e.data.createdBy,
      e.data.createdDate,
      e.data.lastModifiedBy,
      e.data.lastModifiedDate);
    this.rowIsSelected = true;
  }

  validateEmailType(emailType: EmailType): boolean {
    if (!emailType.name || !emailType.description || emailType.active === null) {
      return false;
    }
    return true;
  }

  getEmailTypeById(id: number): Observable<EmailType> {
    if (this.rowData.length > 0) {
      this.foundEmail = this.rowData.find(x => x.id === id);
    }
    if (this.foundEmail) {
      return of(this.foundEmail);
    } else {
      return this.emailTypeService.getEmailTypeById(id);
    }
  }

  getEmailTypes(forceReload: boolean = false): Observable<EmailType[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.emailTypeService.getEmailTypes().subscribe(
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
