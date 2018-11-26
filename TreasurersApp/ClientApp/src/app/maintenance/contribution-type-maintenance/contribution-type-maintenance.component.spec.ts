import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContributionTypeMaintenanceComponent } from './contribution-type-maintenance.component';

describe('ContributionTypeMaintenanceComponent', () => {
  let component: ContributionTypeMaintenanceComponent;
  let fixture: ComponentFixture<ContributionTypeMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContributionTypeMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContributionTypeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
