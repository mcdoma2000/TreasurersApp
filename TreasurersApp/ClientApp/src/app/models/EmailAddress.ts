export class EmailAddress {
  id: number = null;
  email: string = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
              email: string,
              createdBy: string,
              createdDate: Date,
              lastModifiedBy: string,
              lastModifiedDate: Date) {
    this.id = id;
    this.email = email;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
