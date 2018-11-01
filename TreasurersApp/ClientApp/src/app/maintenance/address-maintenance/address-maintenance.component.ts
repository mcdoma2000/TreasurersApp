import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';

import { Address } from '../../models/Address';
import { AddressService } from './address.service';

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

  constructor(private addressService: AddressService) { }

  ngOnInit() {
    this.getAddresses$ = this.addressService.getAddresses(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
    if (this.getAddresses$ !== null) {
      this.getAddresses$.unsubscribe();
    }
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
