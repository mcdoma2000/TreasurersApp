import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SecurityService } from './security.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private securityService: SecurityService,
    private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    // Get property name on security object to check
    // const claimType: string = next.data['"claimType"'];
    const claimType: string = next.data.claimType;

    const isAuth = this.securityService.securityObject.isAuthenticated;
    const hasClaim = this.securityService.hasClaim(claimType);

    console.log('Is Authenticated: ' + isAuth);
    console.log('Has Claim for ' + claimType + ' is ' + hasClaim);

    if (isAuth && hasClaim) {
      return true;
    } else {
      this.router.navigate(['login'],
        { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
}
