import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

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
      Id: this.selectedAddress.Id,
      AddressLine1: this.selectedAddress.AddressLine1,
      AddressLine2: this.selectedAddress.AddressLine2,
      AddressLine3: this.selectedAddress.AddressLine3,
      City: this.selectedAddress.City,
      State: this.selectedAddress.State,
      PostalCode: this.selectedAddress.PostalCode
    };
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = new ConfirmationMessage("warn", "Address Service Message", "Deleting address...");
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.addressService.deleteAddress(this.addressToEdit.Id).subscribe(
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
      this.addAddressToDB(this.addressToEdit).subscribe(
        (resp) => {
          console.log(resp);
        },
        (err) => {
          console.log(err);
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
      Id: this.selectedAddress.Id,
      AddressLine1: this.selectedAddress.AddressLine1,
      AddressLine2: this.selectedAddress.AddressLine2,
      AddressLine3: this.selectedAddress.AddressLine3,
      City: this.selectedAddress.City,
      State: this.selectedAddress.State,
      PostalCode: this.selectedAddress.PostalCode
    };
  }

  saveAddClick($event) {
    console.log("Save Add was clicked");
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: "info", summary: "Address Maintenance: Add", detail: "No data changed." });
    }
  }

  saveEditClick($event) {
    console.log("Save Edit was clicked");
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: "info", summary: "Address Maintenance: Edit", detail: "No data changed." });
    }
  }

  private shouldSaveAdd(): boolean {
    let save = false;
    if (this.addressToEdit.AddressLine1 && this.addressToEdit.City && this.addressToEdit.State && this.addressToEdit.PostalCode) {
      save = true;
    }
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedAddress.Id !== this.addressToEdit.Id ||
        this.selectedAddress.AddressLine1 !== this.addressToEdit.AddressLine1 ||
        this.selectedAddress.AddressLine2 !== this.addressToEdit.AddressLine2 ||
        this.selectedAddress.AddressLine3 !== this.addressToEdit.AddressLine3 ||
        this.selectedAddress.City !== this.addressToEdit.City ||
        this.selectedAddress.State !== this.addressToEdit.State ||
        this.selectedAddress.PostalCode !== this.addressToEdit.PostalCode) {
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
    if (!address.AddressLine1 || !address.City || !address.State || !address.PostalCode) {
      return false;
    }
    return true;
  }

  updateAddressToDB(address: Address): Observable<Address> {
    if (address.Id === null) {
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
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: "An exception occurred while attempting to add the address." });
        this.messageService.add({ severity: "error", summary: "Address Service Message", detail: JSON.stringify(err) });
        return of(this.addressService.newAddress());
      }
    );
  }

  deleteAddressFromDB(address: Address): Observable<Address> {
    if (address.Id === null) {
      const msg = "An incomplete address was passed to delete address";
      console.log(msg);
      this.messageService.add({ severity: "error", summary: "Address Service Message", detail: msg });
      return of(address);
    }
    this.addressService.deleteAddress(address.Id).subscribe(
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
      this.foundAddress = this.rowData.find(x => x.Id === id);
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
