import { ContributionType } from './ContributionType';
import { ContributionTypeService } from '../maintenance/contribution-type-maintenance/contribution-type.service';

export class ContributionTypeActionResult {
  success = false;
  statusMessages: string[] = [];
  contributionType: ContributionType = null;

  constructor(private contributionTypeService: ContributionTypeService) {
    this.success = false;
    this.contributionType = this.contributionTypeService.newContributionType();
    this.statusMessages = [];
  }
}
