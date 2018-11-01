import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Contributor } from '../../models/Contributor';

const CONTRIBUTOR_API_URL = 'http://localhost:55000/api/contributor/';

@Injectable({
  providedIn: 'root'
})
export class ContributorService implements OnInit, OnDestroy {

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  getContributors(): Observable<Contributor[]> {
    return this.http.get<Contributor[]>(CONTRIBUTOR_API_URL);
  }

  getContributorById(id: number): Observable<Contributor> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      params: new HttpParams()
                    .set('id', id.toString())
    };
    return this.http.get<Contributor>(CONTRIBUTOR_API_URL, options);
  }

  updateContributor(id: number, contributor: Contributor) {
    const options = {
      params: new HttpParams()
        .set('id', id.toString())
        .set('contributor', JSON.stringify(Contributor))
    };
    return this.http.put<Contributor>(CONTRIBUTOR_API_URL, contributor, options);
  }

  addContributor(contributor: Contributor) {
    const options = {
      params: new HttpParams().set('contributor', JSON.stringify(Contributor))
    };
    return this.http.post<Contributor>(CONTRIBUTOR_API_URL, contributor, options);
  }

  deleteContributor(id: number) {
    const options = {
      params: new HttpParams().set('id', id.toString())
    };
    return this.http.delete<Contributor>(CONTRIBUTOR_API_URL, options);
  }
}
