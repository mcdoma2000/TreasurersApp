  import { PhoneType } from './PhoneType';
  import { PhoneTypeService } from '../maintenance/phone-type-maintenance/phone-type.service';

  export class PhoneTypeActionResult {
    success = false;
    statusMessages: string[] = [];
    phoneType: PhoneType = null;

    constructor(private phoneTypeService: PhoneTypeService) {
      this.success = false;
      this.phoneType = this.phoneTypeService.newPhoneType();
      this.statusMessages = [];
    }
  }
