import { CashJournal } from './CashJournal';

export class Contributor {
  id: number = null;
  firstName: string = null;
  middleName: string = null;
  lastName: string = null;
  bahaiId: string = null;
  contributions: CashJournal[];
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
    firstName: string,
    middleName: string,
    lastName: string,
    bahaiId: string,
    contributions: CashJournal[],
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date) {
    this.id = id;
    this.firstName = firstName;
    this.middleName = middleName;
    this.lastName = lastName;
    this.bahaiId = bahaiId;
    this.contributions = contributions;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
 }
}
