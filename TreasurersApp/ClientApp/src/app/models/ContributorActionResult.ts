import { Contributor } from './Contributor';
import { ContributorService } from '../maintenance/contributor-maintenance/contributor.service';

export class ContributorActionResult {
  success = false;
  statusMessages: string[] = [];
  contributor: Contributor = null;

  constructor(private contributorService: ContributorService) {
    this.success = false;
    this.contributor = this.contributorService.newContributor();
    this.statusMessages = [];
  }
}
