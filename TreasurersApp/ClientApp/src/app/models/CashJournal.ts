import { TransactionType } from './TransactionType';
import { Contributor } from './Contributor';

export class CashJournal {
  id: number;
  checkNumber: number;
  amount: number;
  contributorId: number;
  bahaiId: string;
  transactionTypeId: number;
  effectiveDate: Date;
  createdBy: string;
  createdDate: Date;
  lastModifiedBy: string;
  lastModifiedDate: Date;
  transactionType: TransactionType;
  contributor: Contributor;

  constructor(id: number,
    checkNumber: number,
    amount: number,
    contributorId: number,
    bahaiId: string,
    transactionTypeId: number,
    effectiveDate: Date,
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date,
    transactionType: TransactionType,
    contributor: Contributor,
    ) {
      this.id = id;
      this.checkNumber = checkNumber;
      this.amount = amount;
      this.contributorId = contributorId;
      this.bahaiId = bahaiId;
      this.transactionTypeId = transactionTypeId;
      this.effectiveDate = effectiveDate;
      this.createdBy = createdBy;
      this.createdDate = createdDate;
      this.lastModifiedBy = lastModifiedBy;
      this.lastModifiedDate = lastModifiedDate;
      this.transactionType = transactionType;
      this.contributor = contributor;
    }
}
