export class TransactionCategory {
  id: number = null;
  name: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;

  constructor(id: number, name: string, description: string, displayOrder: number, active: boolean) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
  }
}
