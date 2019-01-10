import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { PhoneType } from '../../models/PhoneType';
import { PhoneTypeService } from './phone-type.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { PhoneTypeRequest } from 'src/app/models/PhoneTypeRequest';
import { SecurityService } from 'src/app/security/security.service';

@Component({
  selector: 'app-phone-type-maintenance',
  templateUrl: './phone-type-maintenance.component.html',
  styleUrls: ['./phone-type-maintenance.component.css']
})
export class PhoneTypeMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: PhoneType[] = [];
  foundPhoneType: PhoneType = null;
  rowIsSelected = false;
  selectedPhoneType = new PhoneType(0, null, null, null, null, null, null, null);
  phoneTypeToEdit = new PhoneType(0, null, null, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private phoneTypeService: PhoneTypeService,
              private securityService: SecurityService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.phoneTypeService.getPhoneTypes(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addPhoneType() {
    this.displayAdd = true;
    this.phoneTypeToEdit = this.phoneTypeService.newPhoneType();
  }

  editPhoneType() {
    this.displayEdit = true;
    this.phoneTypeToEdit =
      new PhoneType(
        this.selectedPhoneType.id,
        this.selectedPhoneType.name,
        this.selectedPhoneType.description,
        this.selectedPhoneType.active,
        this.selectedPhoneType.createdBy,
        this.selectedPhoneType.createdDate,
        this.selectedPhoneType.lastModifiedBy,
        this.selectedPhoneType.lastModifiedDate);
    this.confirmUpdate();
  }

  deletePhoneType() {
    this.phoneTypeToEdit =
    new PhoneType(
      this.selectedPhoneType.id,
      this.selectedPhoneType.name,
      this.selectedPhoneType.description,
      this.selectedPhoneType.active,
      this.selectedPhoneType.createdBy,
      this.selectedPhoneType.createdDate,
      this.selectedPhoneType.lastModifiedBy,
      this.selectedPhoneType.lastModifiedDate);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Type Maintenance: Delete',
      detail: 'Deleting  type...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.phoneTypeService.deletePhoneType(this.phoneTypeToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Type Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Phone Type Maintenance: Delete',
              detail: 'An error occurred while attempting to delete an phone type.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Type Maintenance: Add',
      detail: 'Adding phone type...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new PhoneTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.phoneTypeToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.phoneTypeService.addPhoneType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Type Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Phone Type Maintenance: Update',
      detail: 'Updating phone type...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new PhoneTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.phoneTypeToEdit;
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.phoneTypeService.updatePhoneType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Phone Type Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Phone Type Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: PhoneTypeMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.phoneTypeService.getPhoneTypes(true).subscribe(
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
    this.phoneTypeToEdit =
    new PhoneType(
      this.selectedPhoneType.id,
      this.selectedPhoneType.name,
      this.selectedPhoneType.description,
      this.selectedPhoneType.active,
      this.selectedPhoneType.createdBy,
      this.selectedPhoneType.createdDate,
      this.selectedPhoneType.lastModifiedBy,
      this.selectedPhoneType.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Phone Type Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Phone Type Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.phoneTypeService.validatePhoneType(this.phoneTypeToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedPhoneType.id &&
          (this.selectedPhoneType.name !== this.phoneTypeToEdit.name ||
           this.selectedPhoneType.description !== this.phoneTypeToEdit.description ||
           this.selectedPhoneType.active !== this.phoneTypeToEdit.active)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.phoneTypeToEdit = this.phoneTypeService.newPhoneType();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.phoneTypeToEdit = this.phoneTypeService.newPhoneType();
  }

  onRowClick(e) {
    this.selectedPhoneType = new PhoneType(
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

  validatePhoneType(emailType: PhoneType): boolean {
    if (!emailType.name || !emailType.description || emailType.active === null) {
      return false;
    }
    return true;
  }

  getPhoneTypeById(id: number): Observable<PhoneType> {
    if (this.rowData.length > 0) {
      this.foundPhoneType = this.rowData.find(x => x.id === id);
    }
    if (this.foundPhoneType) {
      return of(this.foundPhoneType);
    } else {
      return this.phoneTypeService.getPhoneTypeById(id);
    }
  }

  getPhoneTypes(forceReload: boolean = false): Observable<PhoneType[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.phoneTypeService.getPhoneTypes().subscribe(
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
