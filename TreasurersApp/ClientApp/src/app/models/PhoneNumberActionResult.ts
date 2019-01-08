import { PhoneNumber } from './PhoneNumber';
import { PhoneService } from '../maintenance/phone-maintenance/phone.service';

export class PhoneNumberActionResult {
  success = false;
  statusMessages: string[] = [];
  phoneNumber: PhoneNumber = null;

  constructor(private phoneService: PhoneService) {
    this.success = false;
    this.phoneNumber = this.phoneService.newPhoneNumber();
    this.statusMessages = [];
  }
}
