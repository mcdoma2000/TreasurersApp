import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { AddressType } from '../../models/AddressType';
import { AddressTypeRequest } from '../../models/AddressTypeRequest';
import { AddressTypeActionResult } from '../../models/AddressTypeActionResult';

const ADDRESS_API_URL = '/api/addresstype';


@Injectable({
  providedIn: 'root'
})
export class AddressTypeService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newAddressType(): AddressType {
    return new AddressType(0, null, null, null, null, null, null, null);
  }

  getAddressTypes(forceReload: boolean = false): Observable<AddressType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<AddressType[]>(ADDRESS_API_URL + '/get', options);
  }

  getAddressTypeById(id: number, forceReload: boolean = false): Observable<AddressType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<AddressType>(ADDRESS_API_URL + '/getbyid', options);
  }

  validateAddressType(addressType: AddressType): boolean {
    if (!addressType.name || !addressType.description || addressType.active === null) {
      return false;
    }
    return true;
  }

  updateAddressType(request: AddressTypeRequest): Observable<AddressTypeActionResult> {
    return this.http.put<AddressTypeActionResult>(ADDRESS_API_URL + '/put', request, this.httpOptions);
  }

  addAddressType(request: AddressTypeRequest): Observable<AddressTypeActionResult> {
    return this.http.post<AddressTypeActionResult>(ADDRESS_API_URL + '/post', request, this.httpOptions);
  }

  deleteAddressType(addressTypeId: number): Observable<AddressTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', addressTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<AddressTypeActionResult>(ADDRESS_API_URL + '/delete', options);
  }
}
