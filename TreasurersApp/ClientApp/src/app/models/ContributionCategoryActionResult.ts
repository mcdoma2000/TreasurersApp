import { ContributionCategory } from './ContributionCategory';
import { ContributionCategoryService } from '../maintenance/contribution-category-component/contribution-category.service';

export class ContributionCategoryActionResult {
  success = false;
  statusMessages: string[] = [];
  contributionCategory: ContributionCategory = null;

  constructor(private contributionTypeService: ContributionCategoryService) {
    this.success = false;
    this.contributionCategory = this.contributionTypeService.newContributionCategory();
    this.statusMessages = [];
  }
}
