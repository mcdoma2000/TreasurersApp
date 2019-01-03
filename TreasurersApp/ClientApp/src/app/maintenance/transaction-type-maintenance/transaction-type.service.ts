import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as moment from 'moment';

import { TransactionType } from '../../models/TransactionType';
import { TransactionTypeViewModel } from '../../models/TransactionTypeViewModel';
import { TransactionTypeActionResult } from '../../models/TransactionTypeActionResult';

const TRANSACTIONTYPE_API_URL = '/api/contributiontype';

@Injectable({
  providedIn: 'root'
})
export class TransactionTypeService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newTransactionType(): TransactionType {
    return new TransactionType(0, null, null, null, null, null);
  }

  getTransactionTypeViewModels(forceReload: boolean = false, includeInactive: boolean = false): Observable<TransactionTypeViewModel[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionTypeViewModel[]>(TRANSACTIONTYPE_API_URL + '/getviewmodels', options);
  }

  getTransactionTypes(forceReload: boolean = false, includeInactive: boolean = false): Observable<TransactionType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionType[]>(TRANSACTIONTYPE_API_URL + '/get', options);
  }

  getTransactionTypeById(id: number, forceReload: boolean = false): Observable<TransactionType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionType>(TRANSACTIONTYPE_API_URL + '/getbyid', options);
  }

  validateTransactionType(contributionType: TransactionType): boolean {
    if (!contributionType.transactionCategoryId ||
      !contributionType.name ||
      !contributionType.description ||
      contributionType.active === null) {
      return false;
    }
    return true;
  }

  updateTransactionType(contributionType: TransactionType): Observable<TransactionTypeActionResult> {
    if (this.validateTransactionType(contributionType) === false) {
      console.log('Attempted to update an invalid contribution type.');
      console.log(JSON.stringify(contributionType));
    }
    return this.http.put<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/put', contributionType, this.httpOptions);
  }

  addTransactionType(contributionType: TransactionType): Observable<TransactionTypeActionResult> {
    contributionType.id = 0;
    if (this.validateTransactionType(contributionType) === false) {
      console.log('Attempted to add an invalid contribution type.');
      console.log(JSON.stringify(contributionType));
    }
    return this.http.post<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/post', contributionType, this.httpOptions);
  }

  deleteTransactionType(contributionTypeId: number): Observable<TransactionTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', contributionTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/delete', options);
  }
}
