import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { PhoneNumber } from '../../models/PhoneNumber';
import { PhoneService } from './phone.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { PhoneNumberRequest } from 'src/app/models/PhoneNumberRequest';
import { SecurityService } from 'src/app/security/security.service';

@Component({
  selector: 'app-phone-maintenance',
  templateUrl: './phone-maintenance.component.html',
  styleUrls: ['./phone-maintenance.component.css']
})
export class PhoneMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: PhoneNumber[] = [];
  foundPhone: PhoneNumber = null;
  rowIsSelected = false;
  selectedPhone = new PhoneNumber(0, null, null, null, null, null);
  phoneToEdit = new PhoneNumber(0, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private phoneService: PhoneService,
              private securityService: SecurityService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.phoneService.getPhones(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addPhone() {
    this.displayAdd = true;
    this.phoneToEdit = this.phoneService.newPhoneNumber();
  }

  editPhone() {
    this.displayEdit = true;
    this.phoneToEdit =
      new PhoneNumber(this.selectedPhone.id, this.selectedPhone.phoneNumber, this.selectedPhone.createdBy,
        this.selectedPhone.createdDate, this.selectedPhone.lastModifiedBy, this.selectedPhone.lastModifiedDate);
    this.confirmUpdate();
  }

  deletePhone() {
    this.phoneToEdit =
      new PhoneNumber(this.selectedPhone.id, this.selectedPhone.phoneNumber, this.selectedPhone.createdBy,
        this.selectedPhone.createdDate, this.selectedPhone.lastModifiedBy, this.selectedPhone.lastModifiedDate);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Maintenance: Delete',
      detail: 'Deleting phone...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.phoneService.deletePhone(this.phoneToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Phone Maintenance: Delete',
              detail: 'An error occurred while attempting to delete a phone.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Maintenance: Add',
      detail: 'Adding phone...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new PhoneNumberRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.phoneToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.phoneService.addPhone(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Maintenance: Update',
      detail: 'Updating phone...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new PhoneNumberRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.phoneToEdit;
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.phoneService.updatePhone(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: PhoneMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.phoneService.getPhones(true).subscribe(
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
    this.phoneToEdit =
      new PhoneNumber(this.selectedPhone.id, this.selectedPhone.phoneNumber, this.selectedPhone.createdBy,
        this.selectedPhone.createdDate, this.selectedPhone.lastModifiedBy, this.selectedPhone.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Phone Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Phone Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.phoneService.validatePhone(this.phoneToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedPhone.id && this.selectedPhone.phoneNumber !== this.phoneToEdit.phoneNumber) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.phoneToEdit = this.phoneService.newPhoneNumber();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.phoneToEdit = this.phoneService.newPhoneNumber();
  }

  onRowClick(e) {
    this.selectedPhone = new PhoneNumber(
      e.data.id,
      e.data.phoneNumber,
      e.data.createdBy,
      e.data.createdDate,
      e.data.lastModifiedBy,
      e.data.lastModifiedDate);
    this.rowIsSelected = true;
  }

  validatePhone(phone: PhoneNumber): boolean {
    if (!phone.phoneNumber) {
      return false;
    }
    return true;
  }

  getPhoneById(id: number): Observable<PhoneNumber> {
    if (this.rowData.length > 0) {
      this.foundPhone = this.rowData.find(x => x.id === id);
    }
    if (this.foundPhone) {
      return of(this.foundPhone);
    } else {
      return this.phoneService.getPhoneById(id);
    }
  }

  getPhones(forceReload: boolean = false): Observable<PhoneNumber[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.phoneService.getPhones().subscribe(
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
