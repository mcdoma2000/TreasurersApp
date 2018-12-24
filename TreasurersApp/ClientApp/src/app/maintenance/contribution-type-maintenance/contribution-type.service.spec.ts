import { TestBed } from '@angular/core/testing';

import { ContributionTypeService } from './contribution-type.service';

describe('ContributionTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ContributionTypeService = TestBed.get(ContributionTypeService);
    expect(service).toBeTruthy();
  });
});
