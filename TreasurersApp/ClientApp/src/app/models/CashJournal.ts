import { ContributionType } from './ContributionType';
import { Contributor } from './Contributor';

export class CashJournal {
  id: number;
  checkNumber: number;
  amount: number;
  contributorId: number;
  bahaiId: string;
  contributionTypeId: number;
  effectiveDate: Date;
  createdBy: string;
  createdDate: Date;
  lastModifiedBy: string;
  lastModifiedDate: Date;
  contributionType: ContributionType;
  contributor: Contributor;

  constructor(id: number,
    checkNumber: number,
    amount: number,
    contributorId: number,
    bahaiId: string,
    contributionTypeId: number,
    effectiveDate: Date,
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date,
    contributionType: ContributionType,
    contributor: Contributor,
    ) {
      this.id = id;
      this.checkNumber = checkNumber;
      this.amount = amount;
      this.contributorId = contributorId;
      this.bahaiId = bahaiId;
      this.contributionTypeId = contributionTypeId;
      this.effectiveDate = effectiveDate;
      this.createdBy = createdBy;
      this.createdDate = createdDate;
      this.lastModifiedBy = lastModifiedBy;
      this.lastModifiedDate = lastModifiedDate;
      this.contributionType = contributionType;
      this.contributor = contributor;
    }
}
