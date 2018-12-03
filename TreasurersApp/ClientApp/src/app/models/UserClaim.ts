export class UserClaim {
  claimId: string = null;
  userId: string = null;
  claimType: string = null;
  claimValue: string = null;

  constructor(claimId: string, userId: string, claimType: string, claimValue: string) {
    this.claimId = claimId;
    this.userId = userId;
    this.claimType = claimType;
    this.claimValue = claimValue;
  }
}
