export class ContributionType {
  id: number = null;
  contributionTypeCategory: string = null;
  contributionTypeName: string = null;
  description: string = null;

  constructor(id: number, contributionTypeCategory: string, contributionTypeName: string, description: string) {
    this.id = id;
    this.contributionTypeCategory = contributionTypeCategory;
    this.contributionTypeName = contributionTypeName;
    this.description = description;
  }
}
