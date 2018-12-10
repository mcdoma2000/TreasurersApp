import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Contributor } from '../../models/Contributor';
import { ContributorActionResult } from '../../models/ContributorActionResult';

const API_URL = '/api/contributor';

@Injectable({
  providedIn: 'root'
})
export class ContributorService implements OnInit, OnDestroy {

  private defaultHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  });
  private defaultOptions = {
    headers: this.defaultHeaders
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  newContributor(): Contributor {
    return new Contributor(0, null, null, null, null);
  }

  validateContributor(contributor: Contributor): boolean {
    let isValid = false;
    if (contributor) {
      isValid = (contributor.firstName !== null && contributor.lastName !== null);
    }
    return isValid;
  }

  getContributors(): Observable<Contributor[]> {
    return this.http.get<Contributor[]>(API_URL + '/get');
  }

  getContributorById(id: number): Observable<Contributor> {
    const options = {
      headers: this.defaultHeaders,
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.get<Contributor>(API_URL + '/getbyid', options);
  }

  updateContributor(contributor: Contributor): Observable<ContributorActionResult>  {
    if (this.validateContributor(contributor) === false) {
      console.log('Attempted to update an invalid contributor.');
      console.log(JSON.stringify(contributor));
      const errResult = new ContributorActionResult(this);
      errResult.contributor = contributor;
      errResult.statusMessages.push('Attempted to update an invalid contributor.');
      errResult.success = false;
      return of(errResult);
    }
    return this.http.put<ContributorActionResult>(API_URL + '/put', contributor, this.defaultOptions);
  }

  addContributor(contributor: Contributor): Observable<ContributorActionResult> {
    if (this.validateContributor(contributor) === false) {
      console.log('Attempted to add an invalid contributor.');
      console.log(JSON.stringify(contributor));
      const errResult = new ContributorActionResult(this);
      errResult.contributor = contributor;
      errResult.statusMessages.push('Attempted to add an invalid contributor.');
      errResult.success = false;
      return of(errResult);
    }
    return this.http.post<ContributorActionResult>(API_URL, contributor, this.defaultOptions);
  }

  deleteContributor(id: number): Observable<ContributorActionResult> {
    const options = {
      headers: this.defaultHeaders,
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.delete<ContributorActionResult>(API_URL, options);
  }
}
