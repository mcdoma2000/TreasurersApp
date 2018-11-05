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

  constructor(private http: HttpClient) { }

  newAddress(): Address {
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
    if (!address.addressLine1 || !address.city || !address.state || !address.postalCode) {
      return false;
    }
    return true;
  }

  updateAddress(address: Address): Observable<AddressActionResult> {
    const options = {
      params: new HttpParams().set('address', JSON.stringify(address))
    };
    let result = new AddressActionResult(this);
    this.http.put<Address>(ADDRESS_API_URL, address, options).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result = err;
      }
    );
    return of(result);
  }

  addAddress(address: Address): Observable<AddressActionResult> {
    const options = {
      params: new HttpParams().set('address', JSON.stringify(address))
    };
    let result = new AddressActionResult(this);
    this.http.post<Address>(ADDRESS_API_URL, address, options).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result = err;
      }
    );
    return of(result);
  }

  deleteAddress(address: Address): Observable<AddressActionResult> {
    const options = {
      params: new HttpParams().set('address', JSON.stringify(address))
    };
    let result: AddressActionResult = null;
    this.http.delete<AddressActionResult>(ADDRESS_API_URL, options).subscribe(
      (resp) => {
        result = resp;
      },
      (err) => {
        result = err;
      }
    );
    return of(result);
  }
}
