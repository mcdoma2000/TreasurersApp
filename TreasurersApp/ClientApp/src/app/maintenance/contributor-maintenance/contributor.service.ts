import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as _ from 'lodash';

import { Contributor } from '../../models/Contributor';
import { ContributorViewModel } from '../../models/ContributorViewModel';
import { ContributorActionResult } from '../../models/ContributorActionResult';
import { ContributorRequest } from 'src/app/models/ContributorRequest';
import { SecurityService } from 'src/app/security/security.service';

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
    return new Contributor(0, null, null, null, null, null, null, null, null, null);
  }

  newContributorFromViewModel(viewModel: ContributorViewModel): Contributor {
    return new Contributor(
      viewModel.id,
      viewModel.firstName,
      viewModel.middleName,
      viewModel.lastName,
      viewModel.bahaiId,
      viewModel.contributions,
      viewModel.createdBy,
      viewModel.createdDate,
      viewModel.lastModifiedBy,
      viewModel.lastModifiedDate
    );
  }

  newViewModel(): ContributorViewModel {
    return new ContributorViewModel(0, null, null, null, null, null);
  }

  validateContributor(contributor: Contributor): boolean {
    let isValid = false;
    if (contributor) {
      isValid = (contributor.firstName !== null && contributor.lastName !== null);
    }
    return isValid;
  }

  validateContributorViewModel(contributor: ContributorViewModel): boolean {
    let isValid = false;
    if (contributor) {
      isValid = (contributor.firstName !== null && contributor.lastName !== null);
    }
    return isValid;
  }

  getContributors(): Observable<ContributorViewModel[]> {
    return this.http.get<ContributorViewModel[]>(API_URL + '/getvm');
  }

  getContributorById(id: number): Observable<ContributorViewModel> {
    const options = {
      headers: this.defaultHeaders,
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.get<ContributorViewModel>(API_URL + '/getvmbyid', options);
  }

  updateContributor(request: ContributorRequest): Observable<ContributorActionResult>  {
    return this.http.put<ContributorActionResult>(API_URL + '/put', request, this.defaultOptions);
  }

  addContributor(request: ContributorRequest): Observable<ContributorActionResult> {
    return this.http.post<ContributorActionResult>(API_URL + '/post', request, this.defaultOptions);
  }

  deleteContributor(id: number): Observable<ContributorActionResult> {
    const options = {
      headers: this.defaultHeaders,
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.delete<ContributorActionResult>(API_URL + '/delete', options);
  }
}
