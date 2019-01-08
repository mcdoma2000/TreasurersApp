import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as moment from 'moment';

import { TransactionType } from '../../models/TransactionType';
import { TransactionTypeViewModel } from '../../models/TransactionTypeViewModel';
import { TransactionTypeActionResult } from '../../models/TransactionTypeActionResult';
import { TransactionTypeRequest } from 'src/app/models/TransactionTypeRequest';

const TRANSACTIONTYPE_API_URL = '/api/transactiontype';

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
    return new TransactionType(0, null, null, null, null, null, null, null, null, null);
  }

  getTransactionTypes(forceReload: boolean = false, includeInactive: boolean = false): Observable<TransactionType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionType[]>(TRANSACTIONTYPE_API_URL + '/get', options);
  }

  getTransactionTypeById(id: number, forceReload: boolean = false): Observable<TransactionType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams().set('id', id.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionType>(TRANSACTIONTYPE_API_URL + '/getbyid', options);
  }

  validateTransactionType(transactionType: TransactionType): boolean {
    if (!transactionType.transactionCategoryId ||
      !transactionType.name ||
      !transactionType.description ||
      transactionType.active === null) {
      return false;
    }
    return true;
  }

  updateTransactionType(request: TransactionTypeRequest): Observable<TransactionTypeActionResult> {
    return this.http.put<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/put', request, this.httpOptions);
  }

  addTransactionType(request: TransactionTypeRequest): Observable<TransactionTypeActionResult> {
    return this.http.post<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/post', request, this.httpOptions);
  }

  deleteTransactionType(transactionTypeId: number): Observable<TransactionTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', transactionTypeId.toString()),
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json' })
    };
    return this.http.delete<TransactionTypeActionResult>(TRANSACTIONTYPE_API_URL + '/delete', options);
  }
}
