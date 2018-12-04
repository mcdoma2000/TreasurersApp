import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContributionCategoryMaintenanceComponent } from './contribution-category-maintenance.component';

describe('ContributionCategoryMaintenanceComponent', () => {
  let component: ContributionCategoryMaintenanceComponent;
  let fixture: ComponentFixture<ContributionCategoryMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContributionCategoryMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContributionCategoryMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
