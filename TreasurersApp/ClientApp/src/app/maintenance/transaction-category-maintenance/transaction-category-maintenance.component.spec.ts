import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionCategoryMaintenanceComponent } from './transaction-category-maintenance.component';

describe('TransactionCategoryMaintenanceComponent', () => {
  let component: TransactionCategoryMaintenanceComponent;
  let fixture: ComponentFixture<TransactionCategoryMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionCategoryMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionCategoryMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
