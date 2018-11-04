import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { Address } from '../../models/Address';
import { AddressService } from './address.service';
import { ConfirmationMessage } from '../../models/confirmationMessage';

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
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = new ConfirmationMessage("warn", "Service Message", "Deleting address...");
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg);
  }

  confirmAdd() {
    const confMsg = new ConfirmationMessage("warn", "Service Message", "Adding address...");
    this.showConfirmation("Are you certain that you want to add this record?", "Add Confirmation", "Add", confMsg);
  }

  confirmUpdate() {
    const confMsg = new ConfirmationMessage("warn", "Service Message", "Updating address...");
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg);
  }

  showConfirmation(message: string, header: string, acceptLabel: string, confirmationMessage: ConfirmationMessage) {
    this.confirmationService.confirm({
      message: message,
      header: header,
      icon: 'pi pi-info-circle',
      acceptLabel: acceptLabel,
      rejectLabel: "Cancel",
      accept: () => {
        this.messageService.add(confirmationMessage);
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

  saveEditClick($event) {
    console.log("Save Edit was clicked");
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      console.log("Save edit");
    } else {
      console.log("Don't save edit");
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
    this.addressToEdit = this.newAddress();
  }

  newAddress() {
    return {
      id: null,
      addressLine1: null,
      addressLine2: null,
      addressLine3: null,
      city: null,
      state: null,
      postalCode: null
    };
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

  updateAddressToDB(address: Address): Observable<Address> {
    if (address.id === null) {
      console.log("an empty address was passed to saveAddress()");
      return of(address);
    }
    this.addressService.updateAddress(address.id, address).subscribe(
      (resp) => {
        return of(this.rowData);
      },
      (err) => {
        console.log(err);
        this.rowData = [];
        return of(this.rowData);
      }
    );
  }

  addAddressToDB(address: Address): Observable<Address> {
    if (address.id === null) {
      console.log("an empty address was passed to addAddress()");
      return of(address);
    }
    this.addressService.addAddress(address).subscribe(
      (resp) => {
        return of(address);
      },
      (err) => {
        console.log(err);
        return of(address);
      }
    );
  }

  deleteAddressFromDB(address: Address): Observable<Address> {
    return of(address);
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
