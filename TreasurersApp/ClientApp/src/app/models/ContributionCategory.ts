export class ContributionCategory {
  id: number = null;
  contributionCategoryName: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;

  constructor(id: number, contributionCategoryName: string, description: string, displayOrder: number, active: boolean) {
    this.id = id;
    this.contributionCategoryName = contributionCategoryName;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
  }
}
