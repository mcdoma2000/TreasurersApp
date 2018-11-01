import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StandardReportFormComponent } from './standard-report-form.component';

describe('StandardReportFormComponent', () => {
  let component: StandardReportFormComponent;
  let fixture: ComponentFixture<StandardReportFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StandardReportFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StandardReportFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
