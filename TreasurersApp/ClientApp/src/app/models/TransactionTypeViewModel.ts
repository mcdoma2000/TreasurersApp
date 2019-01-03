export class TransactionTypeViewModel {
  id: number = null;
  transactionCategoryId: number = null;
  transactionCategoryDescription: string;
  name: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;

  constructor(id: number,
    transactionCategoryId: number,
    transactionCategoryDescription: string,
    name: string,
    description: string,
    displayOrder: number,
    active: boolean) {
    this.id = id;
    this.transactionCategoryId = transactionCategoryId;
    this.transactionCategoryDescription = transactionCategoryDescription;
    this.name = name;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
  }
}
