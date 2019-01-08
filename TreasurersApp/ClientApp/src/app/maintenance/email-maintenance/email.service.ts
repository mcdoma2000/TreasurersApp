import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { EmailAddress } from '../../models/EmailAddress';
import { EmailAddressActionResult } from '../../models/EmailAddressActionResult';
import { EmailAddressRequest } from 'src/app/models/EmailAddressRequest';

const EMAIL_API_URL = '/api/email';


@Injectable({
  providedIn: 'root'
})
export class EmailService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newEmailAddress(): EmailAddress {
    return new EmailAddress(0, null, null, null, null, null);
  }

  getEmails(forceReload: boolean = false): Observable<EmailAddress[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<EmailAddress[]>(EMAIL_API_URL + '/get', options);
  }

  getEmailById(id: number, forceReload: boolean = false): Observable<EmailAddress> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
                    .set('forceReload', forceReload.toString())
    };
    return this.http.get<EmailAddress>(EMAIL_API_URL + '/getbyid', options);
  }

  validateEmail(email: EmailAddress): boolean {
    if (!email.email) {
      return false;
    }
    return true;
  }

  updateEmail(request: EmailAddressRequest): Observable<EmailAddressActionResult> {
    return this.http.put<EmailAddressActionResult>(EMAIL_API_URL + '/put', request, this.httpOptions);
  }

  addEmail(request: EmailAddressRequest): Observable<EmailAddressActionResult> {
    return this.http.post<EmailAddressActionResult>(EMAIL_API_URL + '/post', request, this.httpOptions);
  }

  deleteEmail(emailAddressId: number): Observable<EmailAddressActionResult> {
    const options = {
      params: new HttpParams().set('id', emailAddressId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<EmailAddressActionResult>(EMAIL_API_URL + '/delete', options);
  }
}
