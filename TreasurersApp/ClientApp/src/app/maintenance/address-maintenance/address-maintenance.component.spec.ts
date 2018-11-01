import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressMaintenanceComponent } from './address-maintenance.component';

describe('AddressMaintenanceComponent', () => {
  let component: AddressMaintenanceComponent;
  let fixture: ComponentFixture<AddressMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
