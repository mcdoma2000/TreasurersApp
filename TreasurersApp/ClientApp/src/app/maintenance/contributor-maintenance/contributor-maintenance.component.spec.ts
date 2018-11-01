import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContributorMaintenanceComponent } from './contributor-maintenance.component';

describe('ContributorMaintenanceComponent', () => {
  let component: ContributorMaintenanceComponent;
  let fixture: ComponentFixture<ContributorMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContributorMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContributorMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
