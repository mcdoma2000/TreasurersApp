import { TestBed } from '@angular/core/testing';

import { ContributionCategoryService } from './contribution-category.service';

describe('ContributionCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ContributionCategoryService = TestBed.get(ContributionCategoryService);
    expect(service).toBeTruthy();
  });
});
