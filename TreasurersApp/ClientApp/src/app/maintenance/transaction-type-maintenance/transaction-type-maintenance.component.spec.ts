import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionTypeMaintenanceComponent } from './transaction-type-maintenance.component';

describe('TransactionTypeMaintenanceComponent', () => {
  let component: TransactionTypeMaintenanceComponent;
  let fixture: ComponentFixture<TransactionTypeMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionTypeMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionTypeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
