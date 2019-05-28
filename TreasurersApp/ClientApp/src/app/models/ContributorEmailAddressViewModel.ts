import { EmailType } from './EmailType';
import { EmailAddress } from './EmailAddress';
import { Contributor } from './Contributor';

export class ContributorEmailAddressViewModel {
  id: number = null;
  contributorId: number = null;
  emailAddressId: number = null;
  emailTypeId: number = null;
  preferred: boolean = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  email: EmailAddress = null;
  emailType: EmailType = null;

  alsoUsedBy: Contributor[] = [];

  constructor(
    id: number,
    contributorId: number,
    emailAddressId: number = null,
    emailTypeId: number = null,
    preferred: boolean = null,
    createdBy: string = null,
    createdDate: Date = null,
    lastModifiedBy: string = null,
    lastModifiedDate: Date) {
    this.id = id;
    this.contributorId = contributorId;
    this.emailAddressId = emailAddressId;
    this.emailTypeId = emailTypeId,
    this.preferred = preferred;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
