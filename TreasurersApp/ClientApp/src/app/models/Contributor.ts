export class Contributor {
  id: number = null;
  firstName: string = null;
  middleName: string = null;
  lastName: string = null;
  addressId: number = null;

  constructor(id: number, firstName: string, middleName: string, lastName: string, addressId?: number) {
    this.id = id;
    this.firstName = firstName;
    this.middleName = middleName;
    this.lastName = lastName;
    this.addressId = addressId;
  }
}
