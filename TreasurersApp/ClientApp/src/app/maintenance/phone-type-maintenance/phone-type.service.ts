import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { PhoneType } from '../../models/PhoneType';
import { PhoneTypeActionResult } from '../../models/PhoneTypeActionResult';
import { PhoneTypeRequest } from 'src/app/models/PhoneTypeRequest';

const PHONETYPE_API_URL = '/api/phonetype';


@Injectable({
  providedIn: 'root'
})
export class PhoneTypeService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newPhoneType(): PhoneType {
    return new PhoneType(0, null, null, null, null, null, null, null);
  }

  getPhoneTypes(forceReload: boolean = false): Observable<PhoneType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<PhoneType[]>(PHONETYPE_API_URL + '/get', options);
  }

  getPhoneTypeById(id: number, forceReload: boolean = false): Observable<PhoneType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<PhoneType>(PHONETYPE_API_URL + '/getbyid', options);
  }

  validatePhoneType(phoneType: PhoneType): boolean {
    if (!phoneType.name || !phoneType.description || phoneType.active === null) {
      return false;
    }
    return true;
  }

  updatePhoneType(request: PhoneTypeRequest): Observable<PhoneTypeActionResult> {
    return this.http.put<PhoneTypeActionResult>(PHONETYPE_API_URL + '/put', request, this.httpOptions);
  }

  addPhoneType(request: PhoneTypeRequest): Observable<PhoneTypeActionResult> {
    return this.http.post<PhoneTypeActionResult>(PHONETYPE_API_URL + '/post', request, this.httpOptions);
  }

  deletePhoneType(phoneTypeId: number): Observable<PhoneTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', phoneTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<PhoneTypeActionResult>(PHONETYPE_API_URL + '/delete', options);
  }
}
