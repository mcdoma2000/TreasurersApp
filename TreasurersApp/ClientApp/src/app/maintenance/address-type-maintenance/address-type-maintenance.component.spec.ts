import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressTypeMaintenanceComponent } from './address-type-maintenance.component';

describe('AddressTypeMaintenanceComponent', () => {
  let component: AddressTypeMaintenanceComponent;
  let fixture: ComponentFixture<AddressTypeMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressTypeMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressTypeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
