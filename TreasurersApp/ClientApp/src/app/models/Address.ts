export class Address {
  id: number = null;
  addressLine1: string = null;
  addressLine2: string = null;
  addressLine3: string = null;
  city: string = null;
  state: string = null;
  postalCode: string = null;

  constructor(id: number, addressLine1: string, addressLine2: string, addressLine3: string, city: string, state: string, postalCode: string) {
    this.id = id;
    this.addressLine1 = addressLine1;
    this.addressLine2 = addressLine2;
    this.addressLine3 = addressLine3;
    this.city = city;
    this.state = state;
    this.postalCode = postalCode;
  }
}
