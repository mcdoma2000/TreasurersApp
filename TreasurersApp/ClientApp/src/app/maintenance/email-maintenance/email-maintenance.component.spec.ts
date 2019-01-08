import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailMaintenanceComponent } from './email-maintenance.component';

describe('EmailMaintenanceComponent', () => {
  let component: EmailMaintenanceComponent;
  let fixture: ComponentFixture<EmailMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
