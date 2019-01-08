import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { EmailType } from '../../models/EmailType';
import { EmailTypeRequest } from '../../models/EmailTypeRequest';
import { EmailTypeActionResult } from '../../models/EmailTypeActionResult';

const EMAILTYPE_API_URL = '/api/emailtype';


@Injectable({
  providedIn: 'root'
})
export class EmailTypeService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newEmailType(): EmailType {
    return new EmailType(0, null, null, null, null, null, null, null);
  }

  getEmailTypes(forceReload: boolean = false): Observable<EmailType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<EmailType[]>(EMAILTYPE_API_URL + '/get', options);
  }

  getEmailTypeById(id: number, forceReload: boolean = false): Observable<EmailType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<EmailType>(EMAILTYPE_API_URL + '/getbyid', options);
  }

  validateEmailType(emailType: EmailType): boolean {
    if (!emailType.name || !emailType.description || emailType.active === null) {
      return false;
    }
    return true;
  }

  updateEmailType(request: EmailTypeRequest): Observable<EmailTypeActionResult> {
    return this.http.put<EmailTypeActionResult>(EMAILTYPE_API_URL + '/put', request, this.httpOptions);
  }

  addEmailType(request: EmailTypeRequest): Observable<EmailTypeActionResult> {
    return this.http.post<EmailTypeActionResult>(EMAILTYPE_API_URL + '/post', request, this.httpOptions);
  }

  deleteEmailType(addressTypeId: number): Observable<EmailTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', addressTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<EmailTypeActionResult>(EMAILTYPE_API_URL + '/delete', options);
  }
}
