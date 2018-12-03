import { User } from './User';
import { UserClaim } from './UserClaim';

import { UserService } from '../maintenance/user/user.service';

export class UserActionResult {
  success = false;
  statusMessages: string[] = [];
  user: User = null;

  constructor(private userService: UserService) {
    this.success = false;
    this.user = this.userService.newUser();
    this.statusMessages = [];
  }
}
