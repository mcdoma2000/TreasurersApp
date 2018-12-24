import { ContributionCategory } from './ContributionCategory';
import { ContributionCategoryService } from '../maintenance/contribution-category-maintenance/contribution-category.service';

export class ContributionCategoryActionResult {
  success = false;
  statusMessages: string[] = [];
  contributionCategory: ContributionCategory = null;

  constructor(private contributionCategoryService: ContributionCategoryService) {
    this.success = false;
    this.contributionCategory = this.contributionCategoryService.newContributionCategory();
    this.statusMessages = [];
  }
}
