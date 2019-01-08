import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailTypeMaintenanceComponent } from './email-type-maintenance.component';

describe('EmailTypeMaintenanceComponent', () => {
  let component: EmailTypeMaintenanceComponent;
  let fixture: ComponentFixture<EmailTypeMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailTypeMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailTypeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
