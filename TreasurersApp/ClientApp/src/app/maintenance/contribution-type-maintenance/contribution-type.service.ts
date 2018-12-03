import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ContributionType } from '../../models/ContributionType';
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
    return new ContributionType(0, null, null, null);
  }

  getContributionTypes(forceReload: boolean = false): Observable<ContributionType[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<ContributionType[]>(CONTRIBUTIONTYPE_API_URL + "/get", options);
  }

  getContributionTypeById(id: number, forceReload: boolean = false): Observable<ContributionType> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
        .set('forceReload', forceReload.toString())
    };
    return this.http.get<ContributionType>(CONTRIBUTIONTYPE_API_URL + "/getbyid", options);
  }

  validateContributionType(contributionType: ContributionType): boolean {
    if (!contributionType.categoryId || !contributionType.contributionTypeName || !contributionType.description) {
      return false;
    }
    return true;
  }

  updateContributionType(contributionType: ContributionType): Observable<ContributionTypeActionResult> {
    if (this.validateContributionType(contributionType) === false) {
      console.log("Attempted to update an invalid contribution type.");
      console.log(JSON.stringify(contributionType));
    }
    var result = this.http.put<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + "/put", contributionType, this.httpOptions);
    return result
  }

  addContributionType(contributionType: ContributionType): Observable<ContributionTypeActionResult> {
    contributionType.id = 0;
    if (this.validateContributionType(contributionType) === false) {
      console.log("Attempted to add an invalid contribution type.");
      console.log(JSON.stringify(contributionType));
    }
    var result = this.http.post<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + "/post", contributionType, this.httpOptions);
    return result;
  }

  deleteContributionType(contributionTypeId: number): Observable<ContributionTypeActionResult> {
    const options = {
      params: new HttpParams().set('id', contributionTypeId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    var result = this.http.delete<ContributionTypeActionResult>(CONTRIBUTIONTYPE_API_URL + "/delete", options);
    return result;
  }
}
