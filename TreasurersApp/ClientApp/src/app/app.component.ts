import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { AppUserAuth } from './security/app-user-auth';
import { SecurityService } from './security/security.service';
import { ReportsService } from './reports/reports.service';
import { Report } from './reports/Report';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'the Bah&aacute;\'&iacute; Treasurer\'s Application';
  reportsList: Report[] = null;
  securityObject: AppUserAuth = null;
  sub = null;
  emitterSub = null;
  rlSub = null;

  constructor(private securityService: SecurityService,
              private reportsService: ReportsService,
              private loginComponent: LoginComponent,
              private router: Router) {
    this.securityObject = securityService.securityObject;
    this.rlSub = this.emitterSub = loginComponent.loggedInEmitter.subscribe(
      resp => {
        reportsService.getReportsList().subscribe(
          data => {
            this.reportsList = data;
          }
        );
      }
    );
  }

  onNgInit(): void {
    this.sub = this.router.events.subscribe((evt) => { console.log(evt); });
  }

  onNgDestroy(): void {
    this.sub.unsubscribe();
    this.emitterSub.unsubscribe();
    this.rlSub.unsubscribe();
  }

  logout(): void {
    this.securityService.logout();
    this.router.navigateByUrl('/dashboard');
  }
}
