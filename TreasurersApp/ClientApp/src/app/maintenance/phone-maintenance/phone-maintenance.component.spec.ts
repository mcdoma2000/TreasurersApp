import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneMaintenanceComponent } from './phone-maintenance.component';

describe('PhoneMaintenanceComponent', () => {
  let component: PhoneMaintenanceComponent;
  let fixture: ComponentFixture<PhoneMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhoneMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhoneMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
