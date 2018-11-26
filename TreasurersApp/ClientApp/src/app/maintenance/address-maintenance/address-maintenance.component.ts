import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { Address } from '../../models/Address';
import { AddressService } from './address.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import * as $ from 'jquery';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-address-maintenance',
  templateUrl: './address-maintenance.component.html',
  styleUrls: ['./address-maintenance.component.css']
})
export class AddressMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: Address[] = [];
  foundAddress: Address = null;
  rowIsSelected = false;
  selectedAddress = new Address(null, null, null, null, null, null, null);
  addressToEdit = new Address(null, null, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private addressService: AddressService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.addressService.getAddresses(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addAddress() {
    this.displayAdd = true;
    this.addressToEdit = this.addressService.newAddress();
  }

  deleteAddress() {
    this.addressToEdit =
      new Address(this.selectedAddress.id, this.selectedAddress.addressLine1, this.selectedAddress.addressLine2,
        this.selectedAddress.addressLine3, this.selectedAddress.city, this.selectedAddress.state, this.selectedAddress.postalCode);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Maintenance: Delete',
      detail: 'Deleting address...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.addressService.deleteAddress(this.addressToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Delete', detail: 'An error occurred while attempting to delete an address.' });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Maintenance: Add',
      detail: 'Adding address...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      this.addressService.addAddress(this.addressToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Add', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Address Maintenance: Update',
      detail: 'Updating address...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      this.addressService.updateAddress(this.addressToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Address Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Address Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: AddressMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.addressService.getAddresses(true).subscribe(
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
    this.addressToEdit =
      new Address(this.selectedAddress.id, this.selectedAddress.addressLine1, this.selectedAddress.addressLine2,
        this.selectedAddress.addressLine3, this.selectedAddress.city, this.selectedAddress.state, this.selectedAddress.postalCode);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Address Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Address Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.validateAddress(this.addressToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedAddress.id &&
        (this.selectedAddress.addressLine1 !== this.addressToEdit.addressLine1 ||
         this.selectedAddress.addressLine2 !== this.addressToEdit.addressLine2 ||
         this.selectedAddress.addressLine3 !== this.addressToEdit.addressLine3 ||
         this.selectedAddress.city !== this.addressToEdit.city ||
         this.selectedAddress.state !== this.addressToEdit.state ||
         this.selectedAddress.postalCode !== this.addressToEdit.postalCode)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.addressToEdit = this.addressService.newAddress();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.addressToEdit = this.addressService.newAddress();
  }

  onRowClick(e) {
    this.selectedAddress = new Address(
      e.data.id,
      e.data.addressLine1,
      e.data.addressLine2,
      e.data.addressLine3,
      e.data.city,
      e.data.state,
      e.data.postalCode);
    this.rowIsSelected = true;
  }

  validateAddress(address: Address): boolean {
    if (!address.addressLine1 || !address.city || !address.state || !address.postalCode) {
      return false;
    }
    return true;
  }

  getAddressById(id: number): Observable<Address> {
    if (this.rowData.length > 0) {
      this.foundAddress = this.rowData.find(x => x.id === id);
    }
    if (this.foundAddress) {
      return of(this.foundAddress);
    } else {
      return this.addressService.getAddressById(id);
    }
  }

  getAddresses(forceReload: boolean = false): Observable<Address[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.addressService.getAddresses().subscribe(
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
