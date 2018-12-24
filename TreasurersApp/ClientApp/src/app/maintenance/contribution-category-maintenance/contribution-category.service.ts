import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ContributionCategory } from '../../models/ContributionCategory';
import { ContributionCategoryActionResult } from '../../models/ContributionCategoryActionResult';
import * as moment from 'moment';


const CONTRIBUTIONCATEGORY_API_URL = '/api/contributioncategory';

@Injectable({
  providedIn: 'root'
})
export class ContributionCategoryService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  newContributionCategory(): ContributionCategory {
    return new ContributionCategory(0, null, null, null, null);
  }

  getContributionCategories(forceReload: boolean = false, includeInactive: boolean = false): Observable<ContributionCategory[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams()
        .set('includeInactive', includeInactive.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<ContributionCategory[]>(CONTRIBUTIONCATEGORY_API_URL + '/get', options);
  }

  getContributionCategoryById(id: number, forceReload: boolean = false): Observable<ContributionCategory> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
    };
    if (forceReload === true) {
      options.params.set('cacheBuster', moment().format('X'));
    }
    return this.http.get<ContributionCategory>(CONTRIBUTIONCATEGORY_API_URL + '/getbyid', options);
  }

  validateContributionCategory(contributionCategory: ContributionCategory): boolean {
    if (!contributionCategory.contributionCategoryName || !contributionCategory.description) {
      return false;
    }
    return true;
  }

  updateContributionCategory(contributionCategory: ContributionCategory): Observable<ContributionCategoryActionResult> {
    if (this.validateContributionCategory(contributionCategory) === false) {
      console.log('Attempted to update an invalid contribution type.');
      console.log(JSON.stringify(contributionCategory));
    }
    return this.http.put<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + '/put', contributionCategory, this.httpOptions);
  }

  addContributionCategory(contributionCategory: ContributionCategory): Observable<ContributionCategoryActionResult> {
    contributionCategory.id = 0;
    if (this.validateContributionCategory(contributionCategory) === false) {
      console.log('Attempted to add an invalid contribution type.');
      console.log(JSON.stringify(contributionCategory));
    }
    return this.http.post<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + '/post', contributionCategory, this.httpOptions);
  }

  deleteContributionCategory(contributionCategoryId: number): Observable<ContributionCategoryActionResult> {
    const options = {
      params: new HttpParams().set('id', contributionCategoryId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + '/delete', options);
  }
}
