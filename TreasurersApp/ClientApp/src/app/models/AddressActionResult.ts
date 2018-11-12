import { Address } from './Address';
import { AddressService } from '../maintenance/address-maintenance/address.service';

export class AddressActionResult {
  success = false;
  statusMessages: string[] = [];
  address: Address = null;

  constructor(private addressService: AddressService) {
    this.success = false;
    this.address = this.addressService.newAddress();
    this.statusMessages = [];
  }
}
