import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContributionCategoryComponentComponent } from './contribution-category-component.component';

describe('ContributionCategoryComponentComponent', () => {
  let component: ContributionCategoryComponentComponent;
  let fixture: ComponentFixture<ContributionCategoryComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContributionCategoryComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContributionCategoryComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
