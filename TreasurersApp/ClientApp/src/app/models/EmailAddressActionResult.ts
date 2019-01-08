import { EmailAddress } from './EmailAddress';
import { EmailService } from '../maintenance/email-maintenance/email.service';

export class EmailAddressActionResult {
  success = false;
  statusMessages: string[] = [];
  emailAddress: EmailAddress = null;

  constructor(private emailService: EmailService) {
    this.success = false;
    this.emailAddress = this.emailService.newEmailAddress();
    this.statusMessages = [];
  }
}
