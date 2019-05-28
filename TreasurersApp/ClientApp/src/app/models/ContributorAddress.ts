import { AddressType } from './AddressType';
import { Address } from './Address';

export class ContributorAddress {
  id: number = null;
  contributorId: number = null;
  addressId: number = null;
  addressTypeId: number = null;
  preferred: boolean = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  address: Address = null;
  addressType: AddressType = null;

  constructor(
    id: number,
    contributorId: number,
    addressId: number = null,
    addressTypeId: number = null,
    preferred: boolean = null,
    createdBy: string = null,
    createdDate: Date = null,
    lastModifiedBy: string = null,
    lastModifiedDate: Date) {
    this.id = id;
    this.contributorId = contributorId;
    this.addressId = addressId;
    this.addressTypeId = addressTypeId,
    this.preferred = preferred;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;

 }
}
