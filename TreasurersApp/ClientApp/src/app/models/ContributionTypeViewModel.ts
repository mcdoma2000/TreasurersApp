export class ContributionTypeViewModel {
  id: number = null;
  contributionCategoryId: number = null;
  contributionCategoryDescription: string;
  contributionTypeName: string = null;
  description: string = null;
  displayOrder: number = null;
  active = false;

  constructor(id: number,
    contributionCategoryId: number,
    contributionCategoryDescription: string,
    contributionTypeName: string,
    description: string,
    displayOrder: number,
    active: boolean) {
    this.id = id;
    this.contributionCategoryId = contributionCategoryId;
    this.contributionCategoryDescription = contributionCategoryDescription;
    this.contributionTypeName = contributionTypeName;
    this.description = description;
    this.displayOrder = displayOrder;
    this.active = active;
  }
}
