import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Address } from '../../models/Address';
import { AddressRequest } from '../../models/AddressRequest';
import { AddressActionResult } from '../../models/AddressActionResult';

const ADDRESS_API_URL = '/api/address';


@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newAddress(): Address {
    return new Address(0, null, null, null, null, null, null, null, null, null, null);
  }

  getAddresses(forceReload: boolean = false): Observable<Address[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<Address[]>(ADDRESS_API_URL + '/get', options);
  }

  getAddressById(id: number, forceReload: boolean = false): Observable<Address> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<Address>(ADDRESS_API_URL + '/getbyid', options);
  }

  validateAddress(address: Address): boolean {
    if (!address.addressLine1 || !address.city || !address.state || !address.postalCode) {
      return false;
    }
    return true;
  }

  updateAddress(addressRequest: AddressRequest): Observable<AddressActionResult> {
    return this.http.put<AddressActionResult>(ADDRESS_API_URL + '/put', addressRequest, this.httpOptions);
  }

  addAddress(addressRequest: AddressRequest): Observable<AddressActionResult> {
    return this.http.post<AddressActionResult>(ADDRESS_API_URL + '/post', addressRequest, this.httpOptions);
  }

  deleteAddress(addressId: number): Observable<AddressActionResult> {
    const options = {
      params: new HttpParams().set('id', addressId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<AddressActionResult>(ADDRESS_API_URL + '/delete', options);
  }
}
