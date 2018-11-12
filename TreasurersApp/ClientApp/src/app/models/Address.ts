export class Address {
  Id: number = null;
  AddressLine1: string = null;
  AddressLine2: string = null;
  AddressLine3: string = null;
  City: string = null;
  State: string = null;
  PostalCode: string = null;

  constructor(id: number, addressLine1: string, addressLine2: string, addressLine3: string, city: string, state: string, postalCode: string) {
    this.Id = id;
    this.AddressLine1 = addressLine1;
    this.AddressLine2 = addressLine2;
    this.AddressLine3 = addressLine3;
    this.City = city;
    this.State = state;
    this.PostalCode = postalCode;
  }
}
