  import { AddressType } from './AddressType';
  import { AddressTypeService } from '../maintenance/address-type-maintenance/address-type.service';

  export class AddressTypeActionResult {
    success = false;
    statusMessages: string[] = [];
    addressType: AddressType = null;

    constructor(private addressTypeService: AddressTypeService) {
      this.success = false;
      this.addressType = this.addressTypeService.newAddressType();
      this.statusMessages = [];
    }
  }
