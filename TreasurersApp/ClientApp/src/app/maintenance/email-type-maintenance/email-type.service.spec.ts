import { TestBed } from '@angular/core/testing';

import { EmailTypeService } from './email-type.service';

describe('EmailTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmailTypeService = TestBed.get(EmailTypeService);
    expect(service).toBeTruthy();
  });
});
