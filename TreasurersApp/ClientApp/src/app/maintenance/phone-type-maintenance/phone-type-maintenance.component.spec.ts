import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneTypeMaintenanceComponent } from './phone-type-maintenance.component';

describe('PhoneTypeMaintenanceComponent', () => {
  let component: PhoneTypeMaintenanceComponent;
  let fixture: ComponentFixture<PhoneTypeMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhoneTypeMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhoneTypeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
