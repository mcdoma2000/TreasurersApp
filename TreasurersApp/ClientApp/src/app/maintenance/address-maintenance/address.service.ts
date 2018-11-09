import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Address } from '../../models/Address';
import { AddressActionResult } from '../../models/AddressActionResult';

//const ADDRESS_API_URL = 'http://localhost:55000/api/address/';
const ADDRESS_API_URL = '/api/address/';


@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  newAddress(): Address {
    return {
      Id: 0,
      AddressLine1: null,
      AddressLine2: null,
      AddressLine3: null,
      City: null,
      State: null,
      PostalCode: null
    };
  }

  getAddresses(forceReload: boolean = false): Observable<Address[]> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<Address[]>(ADDRESS_API_URL, options);
  }

  getAddressById(id: number, forceReload: boolean = false): Observable<Address> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<Address>(ADDRESS_API_URL, options);
  }

  validateAddress(address: Address): boolean {
    if (!address.AddressLine1 || !address.City || !address.State || !address.PostalCode) {
      return false;
    }
    return true;
  }

  updateAddress(address: Address): Observable<AddressActionResult> {
    let result = new AddressActionResult(this);
    this.http.put<AddressActionResult>(ADDRESS_API_URL, address, this.httpOptions).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result.success = false;
        result.statusMessages.push(JSON.stringify(err));
        result.address = address;
      }
    );
    return of(result);
  }

  addAddress(address: Address): Observable<AddressActionResult> {
    let result = new AddressActionResult(this);
    address.Id = 0;
    this.http.post<AddressActionResult>(ADDRESS_API_URL, address, this.httpOptions).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result.success = false;
        result.statusMessages.push(JSON.stringify(err));
        result.address = address;
      }
    );
    return of(result);
  }

  deleteAddress(addressId: number): Observable<AddressActionResult> {
    let result: AddressActionResult = new AddressActionResult(this);
    const deleteUrl = `${ADDRESS_API_URL}/${addressId}`;
    this.http.delete<AddressActionResult>(deleteUrl, this.httpOptions).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result.success = false;
        result.statusMessages.push(JSON.stringify(err));
        result.address = this.newAddress();
      }
    );
    return of(result);
  }
}
