export class TransactionType {
  id: number = null;
  transactionCategoryId: number = null;
  name: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;
  createdBy: string = null;
  createdDate: Date = null;
  lastModifiedBy: string = null;
  lastModifiedDate: Date = null;

  constructor(id: number,
    transactionCategoryId: number,
    name: string,
    description: string,
    displayOrder: number,
    active: boolean,
    createdBy: string,
    createdDate: Date,
    lastModifiedBy: string,
    lastModifiedDate: Date) {
    this.id = id;
    this.transactionCategoryId = transactionCategoryId;
    this.name = name;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.lastModifiedBy = lastModifiedBy;
    this.lastModifiedDate = lastModifiedDate;
  }
}
