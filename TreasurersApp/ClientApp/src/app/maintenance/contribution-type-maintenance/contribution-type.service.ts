import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as moment from 'moment';

import { ContributionType } from '../../models/ContributionType';
import { ContributionTypeViewModel } from '../../models/ContributionTypeViewModel';
import { ContributionTypeActionResult } from '../../models/ContributionTypeActionResult';

const CONTRIBUTIONTYPE_API_URL = '/api/contributiontype';

@Injectable({
  providedIn: 'root'
})
export class ContributionTypeService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newContributionType(): ContributionType {
    return new ContributionType(0, null, null, null, null, null);
  }

  getContributionTypeViewModels(forceReload: boolean = false, includeInactive: boolean = false): Observable<ContributionTypeViewModel[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<ContributionTypeViewModel[]>(CONTRIBUTIONTYPE_API_URL + '/getviewmodels', options);
  }

  getContributionTypes(forceReload: boolean = false, includeInactive: boolean = false): Observable<ContributionType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<ContributionType[]>(CONTRIBUTIONTYPE_API_URL + '/get', options);
  }

  getContributionTypeById(id: number, forceReload: boolean = false): Observable<ContributionType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<ContributionType>(CONTRIBUTIONTYPE_API_URL + '/getbyid', options);
  }

  validateContributionType(contributionType: ContributionType): boolean {
    if (!contributionType.contributionCategoryId ||
        !contributionType.contributionTypeName ||
        !contributionType.description ||
         contributionType.active === null) {
      return false;
    }
    return true;
  }

  updateContributionType(contributionType: ContributionType): Observable<ContributionTypeActionResult> {
    if (this.validateContributionType(contributionType) === false) {
      console.log('Attempted to update an invalid contribution type.');
      console.log(JSON.stringify(contributionType));
    }
    return this.http.put<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + '/put', contributionType, this.httpOptions);
  }

  addContributionType(contributionType: ContributionType): Observable<ContributionTypeActionResult> {
    contributionType.id = 0;
    if (this.validateContributionType(contributionType) === false) {
      console.log('Attempted to add an invalid contribution type.');
      console.log(JSON.stringify(contributionType));
    }
    return this.http.post<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + '/post', contributionType, this.httpOptions);
  }

  deleteContributionType(contributionTypeId: number): Observable<ContributionTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', contributionTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + '/delete', options);
  }
}
