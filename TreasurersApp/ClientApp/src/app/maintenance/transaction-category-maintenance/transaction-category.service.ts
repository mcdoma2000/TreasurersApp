import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { TransactionCategory } from '../../models/TransactionCategory';
import { TransactionCategoryActionResult } from '../../models/TransactionCategoryActionResult';
import * as moment from 'moment';
import { TransactionCategoryRequest } from 'src/app/models/TransactionCategoryRequest';


const TRANSACTIONCATEGORY_API_URL = '/api/TransactionCategory';

@Injectable({
  providedIn: 'root'
})
export class TransactionCategoryService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newTransactionCategory(): TransactionCategory {
    return new TransactionCategory(0, null, null, null, null, null, null, null, null);
  }

  getTransactionCategories(forceReload: boolean = false, includeInactive: boolean = false): Observable<TransactionCategory[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionCategory[]>(TRANSACTIONCATEGORY_API_URL + '/get', options);
  }

  getTransactionCategoryById(id: number, forceReload: boolean = false): Observable<TransactionCategory> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<TransactionCategory>(TRANSACTIONCATEGORY_API_URL + '/getbyid', options);
  }

  validateTransactionCategory(transactionCategory: TransactionCategory): boolean {
    if (!transactionCategory.name || !transactionCategory.description || !transactionCategory.displayOrder || transactionCategory.active === null) {
      return false;
    }
    return true;
  }

  updateTransactionCategory(request: TransactionCategoryRequest): Observable<TransactionCategoryActionResult> {
    return this.http.put<TransactionCategoryActionResult>(TRANSACTIONCATEGORY_API_URL + '/put', request, this.httpOptions);
  }

  addTransactionCategory(request: TransactionCategoryRequest): Observable<TransactionCategoryActionResult> {
    return this.http.post<TransactionCategoryActionResult>(TRANSACTIONCATEGORY_API_URL + '/post', request, this.httpOptions);
  }

  deleteTransactionCategory(transactionCategoryId: number): Observable<TransactionCategoryActionResult> {
    const options = {
      params: new HttpParams().set('id', transactionCategoryId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<TransactionCategoryActionResult>(TRANSACTIONCATEGORY_API_URL + '/delete', options);
  }
}
