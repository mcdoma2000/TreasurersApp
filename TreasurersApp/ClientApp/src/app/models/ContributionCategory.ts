export class ContributionCategory {
  id: number = null;
  contributionCategoryName: string = null;
  description: string = null;

  constructor(id: number, contributionCategoryName: string, description: string) {
    this.id = id;
    this.contributionCategoryName = contributionCategoryName;
    this.description = description;
  }
}
