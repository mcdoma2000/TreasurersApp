import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ContributionCategory } from '../../models/ContributionCategory';
import { ContributionCategoryActionResult } from '../../models/ContributionCategoryActionResult';

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
    return new ContributionCategory(0, null, null);
  }

  getContributionCategories(forceReload: boolean = false): Observable<ContributionCategory[]> {
    const options = {
      headers: this.httpOptions.headers,
      params: new HttpParams().set('forceReload', forceReload.toString())
    };
    return this.http.get<ContributionCategory[]>(CONTRIBUTIONCATEGORY_API_URL + "/get", options);
  }

  getContributionCategoryId(id: number, forceReload: boolean = false): Observable<ContributionCategory> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
        .set('id', id.toString())
        .set('forceReload', forceReload.toString())
    };
    return this.http.get<ContributionCategory>(CONTRIBUTIONCATEGORY_API_URL + "/getbyid", options);
  }

  validateContributionCategory(category: ContributionCategory): boolean {
    if (!category.contributionCategoryName || !category.description) {
      return false;
    }
    return true;
  }

  updateContributionCategory(category: ContributionCategory): Observable<ContributionCategoryActionResult> {
    return this.http.put<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + "/put", category, this.httpOptions);
  }

  addAddress(category: ContributionCategory): Observable<ContributionCategoryActionResult> {
    category.id = 0;
    return this.http.post<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + "/post", category, this.httpOptions);
  }

  deleteAddress(categoryId: number): Observable<ContributionCategoryActionResult> {
    const options = {
      params: new HttpParams().set('id', categoryId.toString()),
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this.http.delete<ContributionCategoryActionResult>(CONTRIBUTIONCATEGORY_API_URL + "/delete", options);
  }

}
