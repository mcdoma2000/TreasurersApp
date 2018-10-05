import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { AppUser } from '../security/app-user';
import { AppUserAuth } from '../security/app-user-auth';
import { SecurityService } from '../security/security.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  user: AppUser = new AppUser();
  securityObject: AppUserAuth = null;
  returnUrl: string;
  loggedInEmitter = new EventEmitter(true);
  loggedOutEmitter = new EventEmitter(true);
  isLogin = true;
  loginSub = null;
  commandSub = null;
  securitySub = null;

  constructor(private securityService: SecurityService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
    this.isLogin = true;
    this.commandSub = this.route.data.subscribe(
      (data) => {
        if (data && data.methodName === 'login') {
          this.isLogin = true;
        } else if (data && data.methodName === 'logout') {
          this.isLogin = false;
        }
      }
    );
  }

  ngOnDestroy(): void {
    if (this.loginSub) {
      this.loginSub.unsubscribe();
    }
    if (this.commandSub) {
      this.commandSub.unsubscribe();
    }
    if (this.securitySub) {
      this.securitySub.unsubscribe();
    }
  }

  login() {
    this.securitySub = this.securityService.login(this.user)
      .subscribe(resp => {
        this.loggedInEmitter.emit(null);
        this.securityObject = resp;
        if (this.returnUrl) {
          this.router.navigateByUrl(this.returnUrl);
        } else {
          this.router.navigateByUrl('/dashboard');
        }
      },
        () => {
          // Initialize security object to display error message
          this.securityObject = new AppUserAuth();
          this.loggedOutEmitter.emit(null);
        });
  }

  logout() {
    this.securityService.logout();
    this.loggedOutEmitter.emit(null);
    this.router.navigateByUrl('/dashboard');
  }
}
