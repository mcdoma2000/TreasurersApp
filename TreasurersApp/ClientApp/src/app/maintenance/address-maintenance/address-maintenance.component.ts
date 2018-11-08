import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { Address } from '../../models/Address';
import { AddressService } from './address.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';

@Component({
  selector: 'app-address-maintenance',
  templateUrl: './address-maintenance.component.html',
  styleUrls: ['./address-maintenance.component.css']
})
export class AddressMaintenanceComponent implements OnInit, OnDestroy {

  columnDefs = [
    { headerName: 'ID', field: 'id', hide: true, editable: false },
    { headerName: 'Address 1', field: 'addressLine1', width: 210, editable: true },
    { headerName: 'Address 2', field: 'addressLine2', width: 160, editable: true },
    { headerName: 'Address 3', field: 'addressLine3', width: 160, editable: true },
    { headerName: 'City', field: 'city', width: 165, editable: true },
    { headerName: 'State', field: 'state', width: 70, editable: true },
    { headerName: 'Postal Code', field: 'postalCode', width: 125, editable: true },
  ];

  rowData: Address[] = [];
  foundAddress: Address = null;
  getAddresses$: Subscription = null;
  rowIsSelected = false;
  selectedAddress = new Address(null, null, null, null, null, null, null);
  addressToEdit = new Address(null, null, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;
  gridApi = null;
  columnApi = null;

  constructor(private addressService: AddressService,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
  }

  ngOnInit() {
    this.getAddresses$ = this.addressService.getAddresses(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
    if (this.getAddresses$ !== null) {
      this.getAddresses$.unsubscribe();
    }
  }

  addAddress() {
    this.displayAdd = true;
    this.addressToEdit = new Address(null, null, null, null, null, null, null);
  }

  deleteAddress() {
    console.log(this.selectedAddress);
    this.addressToEdit = {
      id: this.selectedAddress.id,
      addressLine1: this.selectedAddress.addressLine1,
      addressLine2: this.selectedAddress.addressLine2,
      addressLine3: this.selectedAddress.addressLine3,
      city: this.selectedAddress.city,
      state: this.selectedAddress.state,
      postalCode: this.selectedAddress.postalCode
    };
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = new ConfirmationMessage("warn", "Address Service Message", "Deleting address...");
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.addressService.deleteAddress(this.addressToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: "success", summary: "Address Maintenance", detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: msg });
            });
          }
        },
        (err) => {
          this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: JSON.stringify(err) });
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = new ConfirmationMessage("warn", "Address Maintenance", "Adding address...");
    this.showConfirmation("Are you certain that you want to add this record?", "Add Confirmation", "Add", confMsg, () => {
      this.addressService.addAddress(this.addressToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: "success", summary: "Address Maintenance", detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: msg });
            });
          }
        },
        (err) => {
          this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: JSON.stringify(err) });
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = new ConfirmationMessage("warn", "Address Maintenance", "Updating address...");
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.addressService.updateAddress(this.addressToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: "success", summary: "Address Maintenance", detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function(msg) {
              this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: msg });
            });
          }
        },
        (err) => {
          this.messageService.add({ severity: "error", summary: "Address Maintenance", detail: JSON.stringify(err) });
        }
      );
    });
  }

  showConfirmation(message: string, header: string, acceptLabel: string, confirmationMessage: ConfirmationMessage, callback:() => void) {

    this.confirmationService.confirm({
      message: message,
      header: header,
      icon: 'pi pi-info-circle',
      acceptLabel: acceptLabel,
      rejectLabel: "Cancel",
      accept: () => {
        this.messageService.add(confirmationMessage);
        callback();
      },
      reject: () => {
        console.log("Rejected Update");
      }
    });
  }

  editAddress() {
    this.displayEdit = true;
    this.addressToEdit = {
      id: this.selectedAddress.id,
      addressLine1: this.selectedAddress.addressLine1,
      addressLine2: this.selectedAddress.addressLine2,
      addressLine3: this.selectedAddress.addressLine3,
      city: this.selectedAddress.city,
      state: this.selectedAddress.state,
      postalCode: this.selectedAddress.postalCode
    };
  }

  saveAddClick($event) {
    console.log("Save Add was clicked");
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: "info", summary: "Address Maintenance", detail: "No data changed." });
    }
  }

  saveEditClick($event) {
    console.log("Save Edit was clicked");
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: "info", summary: "Address Maintenance", detail: "No data changed." });
    }
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedAddress.id !== this.addressToEdit.id ||
        this.selectedAddress.addressLine1 !== this.addressToEdit.addressLine1 ||
        this.selectedAddress.addressLine2 !== this.addressToEdit.addressLine2 ||
        this.selectedAddress.addressLine3 !== this.addressToEdit.addressLine3 ||
        this.selectedAddress.city !== this.addressToEdit.city ||
        this.selectedAddress.state !== this.addressToEdit.state ||
        this.selectedAddress.postalCode !== this.addressToEdit.postalCode) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log("Cancel Edit was clicked");
    this.displayEdit = false;
    this.addressToEdit = this.addressService.newAddress();
  }

  cancelAddClick($event) {
    console.log("Cancel Add was clicked");
    this.displayAdd = false;
    this.addressToEdit = this.addressService.newAddress();
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.columnApi = params.columnApi;
  }

  onSelectionChanged() {
    const selectedRows = this.gridApi.getSelectedRows();
    if (selectedRows.length === 0) {
      this.rowIsSelected = false;
      this.selectedAddress = null;
    } else {
      this.rowIsSelected = true;
      this.selectedAddress = selectedRows[0];
    }
  }

  validateAddress(address: Address): boolean {
    if (!address.addressLine1 || !address.city || !address.state || !address.postalCode) {
      return false;
    }
    return true;
  }

  updateAddressToDB(address: Address): Observable<Address> {
    if (address.id === null) {
      const msg = "An incomplete address was passed to update address";
      console.log(msg);
      this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
      return of(address);
    }
    if (this.validateAddress(address) === false) {
      const msg = "An invalid address was passed to update address";
      console.log(msg);
      this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
      return of(address);
    }
    this.addressService.updateAddress(address).subscribe(
      (resp) => {
        if (resp.success === true) {
          this.messageService.add({ severity: "success", summary: "Address Service Message", detail: resp.statusMessages[0] });
          return of(resp.address);
        } else {
          this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to update the address." });
          resp.statusMessages.forEach(function (msg) {
            this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
          });
          return of(resp.address);
        }
      },
      (err) => {
        console.log(err);
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to update the address." });
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: JSON.stringify(err) });
        return of(this.addressService.newAddress());
      }
    );
  }

  addAddressToDB(address: Address): Observable<Address> {
    if (this.validateAddress(address) === false) {
      const msg = "An invalid address was passed to add an address";
      console.log(msg);
      this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
      return of(address);
    }
    this.addressService.addAddress(address).subscribe(
      (resp) => {
        if (resp.success === true) {
          resp.statusMessages.forEach(function (value) {
            this.messageService.add({ severity: "success", summary: "Address Service Message", detail: value });
          });
          return of(resp.address);
        } else {
          this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to add the address." });
          resp.statusMessages.forEach(function (msg) {
            this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
          });
          return of(resp.address);
        }
      },
      (err) => {
        console.log(err);
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to add the address." });
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: JSON.stringify(err) });
        return of(this.addressService.newAddress());
      }
    );
  }

  deleteAddressFromDB(address: Address): Observable<Address> {
    if (address.id === null) {
      const msg = "An incomplete address was passed to delete address";
      console.log(msg);
      this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
      return of(address);
    }
    this.addressService.deleteAddress(address.id).subscribe(
      (resp) => {
        if (resp.success === true) {
          this.messageService.add({ severity: "success", summary: "Address Service Message", detail: resp.statusMessages[0] });
          return of(resp.address);
        } else {
          this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to delete the address." });
          resp.statusMessages.forEach(function (msg) {
            this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
          });
          return of(resp.address);
        }
      },
      (err) => {
        console.log(err);
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An error occurred while attempting to delete the address." });
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: JSON.stringify(err) });
        return of(this.addressService.newAddress());
      }
    );
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
          return of(err);
        }
      );
    }
  }
}
