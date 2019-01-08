import { CashJournal } from './CashJournal';

export class ContributorViewModel {
  id: number = null;
  firstName: string = null;
  middleName: string = null;
  lastName: string = null;
  bahaiId: string = null;
  contributions: CashJournal[] = [];
  addressId: number = null;
  addressText: string = null;
  emailAddressId: number = null;
  emailAddress: string = null;
  phoneNumberId: number = null;
  phoneNumber: string = null;
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
              addressId?: number,
              addressText?: string,
              emailAddressId?: number,
              emailAddress?: string,
              phoneNumberId?: number,
              phoneNumber?: string,
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
    this.addressId = addressId;
    this.addressText = addressText;
    this.emailAddressId = emailAddressId;
    this.emailAddress = emailAddress;
    this.phoneNumberId = phoneNumberId;
    this.phoneNumber = phoneNumber;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
