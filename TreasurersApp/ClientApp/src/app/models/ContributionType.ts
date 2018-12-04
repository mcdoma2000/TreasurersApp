export class ContributionType {
  id: number = null;
  contributionCategoryId: number = null;
  contributionTypeName: string = null;
  description: string = null;

  constructor(id: number, contributionCategoryId: number, contributionTypeName: string, description: string) {
    this.id = id;
    this.contributionCategoryId = contributionCategoryId;
    this.contributionTypeName = contributionTypeName;
    this.description = description;
  }
}
