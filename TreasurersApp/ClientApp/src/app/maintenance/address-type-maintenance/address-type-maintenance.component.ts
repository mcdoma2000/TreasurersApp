import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { AddressType } from '../../models/AddressType';
import { AddressTypeRequest } from '../../models/AddressTypeRequest';
import { AddressTypeService } from './address-type.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { SecurityService } from 'src/app/security/security.service';

@Component({
  selector: 'app-address-type-maintenance',
  templateUrl: './address-type-maintenance.component.html',
  styleUrls: ['./address-type-maintenance.component.css']
})
export class AddressTypeMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: AddressType[] = [];
  foundAddressType: AddressType = null;
  rowIsSelected = false;
  selectedAddressType = new AddressType(0, null, null, null, null, null, null, null);
  addressTypeToEdit = new AddressType(0, null, null, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private addressTypeService: AddressTypeService,
              private confirmationService: ConfirmationService,
              private securityService: SecurityService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.addressTypeService.getAddressTypes(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addAddressType() {
    this.displayAdd = true;
    this.addressTypeToEdit = this.addressTypeService.newAddressType();
  }

  editAddressType() {
    this.displayEdit = true;
    this.addressTypeToEdit =
      new AddressType(
        this.selectedAddressType.id,
        this.selectedAddressType.name,
        this.selectedAddressType.description,
        this.selectedAddressType.active,
        this.selectedAddressType.createdBy,
        this.selectedAddressType.createdDate,
        this.selectedAddressType.lastModifiedBy,
        this.selectedAddressType.lastModifiedDate);
    this.confirmUpdate();
  }

  deleteAddressType() {
    this.addressTypeToEdit =
    new AddressType(
      this.selectedAddressType.id,
      this.selectedAddressType.name,
      this.selectedAddressType.description,
      this.selectedAddressType.active,
      this.selectedAddressType.createdBy,
      this.selectedAddressType.createdDate,
      this.selectedAddressType.lastModifiedBy,
      this.selectedAddressType.lastModifiedDate);
  this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Type Maintenance: Delete',
      detail: 'Deleting address type...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.addressTypeService.deleteAddressType(this.addressTypeToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Type Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Address Type Maintenance: Delete',
              detail: 'An error occurred while attempting to delete an address type.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Type Maintenance: Add',
      detail: 'Adding address type...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new AddressTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.addressTypeToEdit;
      request.data.createdDate = new Date();
      request.data.createdBy = this.securityService.loggedInUserId();
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.addressTypeService.addAddressType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Type Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Type Maintenance: Update',
      detail: 'Updating address type...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new AddressTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.addressTypeToEdit;
      request.data.lastModifiedDate = new Date();
      request.data.lastModifiedBy = this.securityService.loggedInUserId();
      this.addressTypeService.updateAddressType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Type Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Type Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: AddressTypeMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.addressTypeService.getAddressTypes(true).subscribe(
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
    this.addressTypeToEdit =
      new AddressType(
        this.selectedAddressType.id,
        this.selectedAddressType.name,
        this.selectedAddressType.description,
        this.selectedAddressType.active,
        this.selectedAddressType.createdBy,
        this.selectedAddressType.createdDate,
        this.selectedAddressType.lastModifiedBy,
        this.selectedAddressType.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Address Type Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Address Type Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.addressTypeService.validateAddressType(this.addressTypeToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if ( this.selectedAddressType.id &&
        (this.selectedAddressType.name !== this.addressTypeToEdit.name ||
         this.selectedAddressType.description !== this.addressTypeToEdit.description ||
         this.selectedAddressType.active !== this.addressTypeToEdit.active)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.addressTypeToEdit = this.addressTypeService.newAddressType();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.addressTypeToEdit = this.addressTypeService.newAddressType();
  }

  onRowClick(e) {
    this.selectedAddressType = new AddressType(
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

  validateAddressType(addressType: AddressType): boolean {
    if (!addressType.name || !addressType.description || addressType.active === null) {
      return false;
    }
    return true;
  }

  getAddressTypeById(id: number): Observable<AddressType> {
    if (this.rowData.length > 0) {
      this.foundAddressType = this.rowData.find(x => x.id === id);
    }
    if (this.foundAddressType) {
      return of(this.foundAddressType);
    } else {
      return this.addressTypeService.getAddressTypeById(id);
    }
  }

  getAddressTypes(forceReload: boolean = false): Observable<AddressType[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.addressTypeService.getAddressTypes().subscribe(
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
