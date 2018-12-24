export class ContributionType {
  id: number = null;
  contributionCategoryId: number = null;
  contributionTypeName: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;

  constructor(id: number,
    contributionCategoryId: number,
    contributionTypeName: string,
    description: string,
    displayOrder: number,
    active: boolean) {
    this.id = id;
    this.contributionCategoryId = contributionCategoryId;
    this.contributionTypeName = contributionTypeName;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
  }
}
