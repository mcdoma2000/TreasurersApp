export class Address {
  id: number = null;
  addressLine1: string = null;
  addressLine2: string = null;
  addressLine3: string = null;
  city: string = null;
  state: string = null;
  postalCode: string = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
    addressLine1: string,
    addressLine2: string,
    addressLine3: string,
    city: string,
    state: string,
    postalCode: string,
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date) {
    this.id = id;
    this.addressLine1 = addressLine1;
    this.addressLine2 = addressLine2;
    this.addressLine3 = addressLine3;
    this.city = city;
    this.state = state;
    this.postalCode = postalCode;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }

  asAddressString() {
    return this.addressLine1 + ', ' + this.city + ', ' + this.state + ' ' + this.postalCode;
  }
}
