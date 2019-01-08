  import { EmailType } from './EmailType';
  import { EmailTypeService } from '../maintenance/email-type-maintenance/email-type.service';

  export class EmailTypeActionResult {
    success = false;
    statusMessages: string[] = [];
    emailType: EmailType = null;

    constructor(private emailTypeService: EmailTypeService) {
      this.success = false;
      this.emailType = this.emailTypeService.newEmailType();
      this.statusMessages = [];
    }
  }
