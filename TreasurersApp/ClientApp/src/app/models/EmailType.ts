export class EmailType {
  id: number = null;
  name: string = null;
  description: string = null;
  active: boolean = null;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
    name: string,
    description: string,
    active: boolean,
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.active = active;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
