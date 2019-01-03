import { TestBed } from '@angular/core/testing';

import { TransactionTypeService } from './transaction-type.service';

describe('TransactionTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TransactionTypeService = TestBed.get(TransactionTypeService);
    expect(service).toBeTruthy();
  });
});
