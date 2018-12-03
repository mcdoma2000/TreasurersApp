import { UserClaim } from './UserClaim';

export class User {
  userId: string = null;
  userName: string = null;
  displayName: string = null;
  password: string = null;
  claims: UserClaim[] = [];

  constructor(userId: string, userName: string, displayName: string, password: string) {
    this.userId = userId;
    this.userName = userName;
    this.displayName = displayName;
    this.password = password;
    this.claims = [];
  }
}
