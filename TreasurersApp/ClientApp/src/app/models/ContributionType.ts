export class ContributionType {
  id: number = null;
  categoryId: number = null;
  contributionTypeName: string = null;
  description: string = null;

  constructor(id: number, categoryId: number, contributionTypeName: string, description: string) {
    this.id = id;
    this.categoryId = categoryId;
    this.contributionTypeName = contributionTypeName;
    this.description = description;
  }
}
