import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { PhoneNumber } from '../../models/PhoneNumber';
import { PhoneNumberActionResult } from '../../models/PhoneNumberActionResult';
import { PhoneNumberRequest } from 'src/app/models/PhoneNumberRequest';

const PHONE_API_URL = '/api/phone';


@Injectable({
  providedIn: 'root'
})
export class PhoneService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newPhoneNumber(): PhoneNumber {
    return new PhoneNumber(0, null, null, null, null, null);
  }

  getPhones(forceReload: boolean = false): Observable<PhoneNumber[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<PhoneNumber[]>(PHONE_API_URL + '/get', options);
  }

  getPhoneById(id: number, forceReload: boolean = false): Observable<PhoneNumber> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<PhoneNumber>(PHONE_API_URL + '/getbyid', options);
  }

  validatePhone(phone: PhoneNumber): boolean {
    if (!phone.phoneNumber) {
      return false;
    }
    return true;
  }

  updatePhone(request: PhoneNumberRequest): Observable<PhoneNumberActionResult> {
    return this.http.put<PhoneNumberActionResult>(PHONE_API_URL + '/put', request, this.httpOptions);
  }

  addPhone(request: PhoneNumberRequest): Observable<PhoneNumberActionResult> {
    return this.http.post<PhoneNumberActionResult>(PHONE_API_URL + '/post', request, this.httpOptions);
  }

  deletePhone(addressId: number): Observable<PhoneNumberActionResult> {
    const options = {
      params: new HttpParams().set('id', addressId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<PhoneNumberActionResult>(PHONE_API_URL + '/delete', options);
  }
}
