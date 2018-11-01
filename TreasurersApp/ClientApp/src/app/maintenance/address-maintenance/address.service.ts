import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Address } from '../../models/Address';

const ADDRESS_API_URL = 'http://localhost:5000/api/address/';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  constructor(private http: HttpClient) { }

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

  updateAddress(id: number, address: Address) {
    const options = {
      params: new HttpParams()
        .set('id', id.toString())
        .set('address', JSON.stringify(address))
    };
    return this.http.put<Address>(ADDRESS_API_URL, address, options);
  }

  addAddress(address: Address) {
    const options = {
      params: new HttpParams().set('address', JSON.stringify(address))
    };
    return this.http.post<Address>(ADDRESS_API_URL, address, options);
  }

  deleteAddress(id: number) {
    const options = {
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.delete<Address>(ADDRESS_API_URL, options);
  }
}
