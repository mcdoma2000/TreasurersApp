export class PhoneNumber {
  id: number = null;
  phoneNumber: string = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
              phoneNumber: string,
              createdBy: string,
              createdDate: Date,
              lastModifiedBy: string,
              lastModifiedDate: Date) {
    this.id = id;
    this.phoneNumber = phoneNumber;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
