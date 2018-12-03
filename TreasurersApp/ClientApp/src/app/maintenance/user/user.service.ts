import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { User } from '../../models/User';
import { UserClaim } from '../../models/UserClaim';
import { UserActionResult } from '../../models/UserActionResult';

const USER_API_URL = '/api/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newUser(): User {
    return new User(null, null, null, null);
  }

  getAddresses(forceReload: boolean = false): Observable<User[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<User[]>(USER_API_URL + "/get", options);
  }

  getUserById(id: number, forceReload: boolean = false): Observable<User> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
        .set('forceReload', forceReload.toString())
    };
    return this.http.get<User>(USER_API_URL + "/getbyid", options);
  }

  validateAddress(user: User): boolean {
    if (!user.userName || !user.displayName || !user.password || !user.claims || user.claims.length === 0) {
      return false;
    }
    return true;
  }

  updateAddress(address: User): Observable<UserActionResult> {
    return this.http.put<UserActionResult>(USER_API_URL + "/put", address, this.httpOptions);
  }

  addAddress(user: User): Observable<UserActionResult> {
    user.userId = null;
    return this.http.post<UserActionResult>(USER_API_URL + "/post", user, this.httpOptions);
  }

  deleteAddress(userId: string): Observable<UserActionResult> {
    const options = {
      params: new HttpParams().set('id', userId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<UserActionResult>(USER_API_URL + "/delete", options);
  }
}
