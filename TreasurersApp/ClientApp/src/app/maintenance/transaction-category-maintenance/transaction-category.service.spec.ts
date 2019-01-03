import { TestBed } from '@angular/core/testing';

import { TransactionCategoryService } from './transaction-category.service';

describe('TransactionCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TransactionCategoryService = TestBed.get(TransactionCategoryService);
    expect(service).toBeTruthy();
  });
});
