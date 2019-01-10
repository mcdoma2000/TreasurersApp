import { AppUserClaim } from './app-user-claim';

export class AppUserAuth {
  userId = '';
  userName = '';
  bearerToken = '';
  isAuthenticated = false;
  claims: AppUserClaim[] = [];
}
