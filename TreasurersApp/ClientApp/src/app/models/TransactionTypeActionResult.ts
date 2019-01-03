import { TransactionType } from './TransactionType';
import { TransactionTypeService } from '../maintenance/transaction-type-maintenance/transaction-type.service';

export class TransactionTypeActionResult {
  success = false;
  statusMessages: string[] = [];
  transactionType: TransactionType = null;

  constructor(private transactionTypeService: TransactionTypeService) {
    this.success = false;
    this.transactionType = this.transactionTypeService.newTransactionType();
    this.statusMessages = [];
  }
}
