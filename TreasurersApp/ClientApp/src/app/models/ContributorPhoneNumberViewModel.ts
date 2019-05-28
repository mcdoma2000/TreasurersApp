import { PhoneType } from './PhoneType';
import { PhoneNumber } from './PhoneNumber';
import { Contributor } from './Contributor';

export class ContributorPhoneNumberViewModel {
  id: number = null;
  contributorId: number = null;
  phoneNumberId: number = null;
  phoneTypeId: number = null;
  preferred: boolean = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  phoneNumber: PhoneNumber = null;
  phoneNumberType: PhoneType = null;

  alsoUsedBy: Contributor[] = [];

  constructor(
    id: number,
    contributorId: number,
    phoneNumberId: number = null,
    phoneTypeId: number = null,
    preferred: boolean = null,
    createdBy: string = null,
    createdDate: Date = null,
    lastModifiedBy: string = null,
    lastModifiedDate: Date) {
    this.id = id;
    this.contributorId = contributorId;
    this.phoneNumberId = phoneNumberId;
    this.phoneTypeId = phoneTypeId,
    this.preferred = preferred;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
