import { CashJournal } from './CashJournal';

export class Contributor {
  id: number = null;
  firstName: string = null;
  middleName: string = null;
  lastName: string = null;
  addressId: number = null;
  contributions: CashJournal[];

  constructor(id: number, firstName: string, middleName: string, lastName: string, contributions: CashJournal[], addressId?: number ) {
    this.id = id;
    this.firstName = firstName;
    this.middleName = middleName;
    this.lastName = lastName;
    this.addressId = addressId;
    this.contributions = contributions;
  }
}
