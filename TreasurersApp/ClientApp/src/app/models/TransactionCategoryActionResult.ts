import { TransactionCategory } from './TransactionCategory';
import { TransactionCategoryService } from '../maintenance/transaction-category-maintenance/transaction-category.service';

export class TransactionCategoryActionResult {
  success = false;
  statusMessages: string[] = [];
  transactionCategory: TransactionCategory = null;

  constructor(private transactionCategoryService: TransactionCategoryService) {
    this.success = false;
    this.transactionCategory = this.transactionCategoryService.newTransactionCategory();
    this.statusMessages = [];
  }
}
