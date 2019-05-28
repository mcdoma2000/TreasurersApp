import { CashJournal } from './CashJournal';
import { ContributorAddress } from './ContributorAddress';
import { ContributorEmailAddress } from './ContributorEmailAddress';
import { ContributorPhoneNumber } from './ContributorPhoneNumber';

export class ContributorViewModel {
  id: number = null;
  firstName: string = null;
  middleName: string = null;
  lastName: string = null;
  bahaiId: string = null;
  contributions: CashJournal[] = [];
  addresses: ContributorAddress[] = [];
  emails: ContributorEmailAddress[] = [];
  phoneNumbers: ContributorPhoneNumber[] = [];
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
              firstName: string,
              middleName: string,
              lastName: string,
              bahaiId?: string,
              contributions?: CashJournal[],
              addresses?: ContributorAddress[],
              emails?: ContributorEmailAddress[],
              phoneNumbers?: ContributorPhoneNumber[],
              createdBy?: string,
              createdDate?: Date,
              lastModifiedBy?: string,
              lastModifiedDate?: Date) {
              this.id = id;
    this.firstName = firstName;
    this.middleName = middleName;
    this.lastName = lastName;
    this.bahaiId = bahaiId;
    this.contributions = contributions;
    this.addresses = addresses;
    this.emails = emails;
    this.phoneNumbers = phoneNumbers;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
