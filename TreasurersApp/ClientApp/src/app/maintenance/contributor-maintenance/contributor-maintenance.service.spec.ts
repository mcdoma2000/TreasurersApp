import { TestBed, inject } from '@angular/core/testing';

import { ContributorMaintenanceService } from './contributor-maintenance.service';

describe('ContributorMaintenanceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ContributorMaintenanceService]
    });
  });

  it('should be created', inject([ContributorMaintenanceService], (service: ContributorMaintenanceService) => {
    expect(service).toBeTruthy();
  }));
});
