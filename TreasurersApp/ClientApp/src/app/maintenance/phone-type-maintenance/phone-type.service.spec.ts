import { TestBed } from '@angular/core/testing';

import { PhoneTypeService } from './phone-type.service';

describe('PhoneTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PhoneTypeService = TestBed.get(PhoneTypeService);
    expect(service).toBeTruthy();
  });
});
