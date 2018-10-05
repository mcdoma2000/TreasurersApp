import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportVoidedChecksComponent } from './report-voided-checks.component';

describe('ReportVoidedChecksComponent', () => {
  let component: ReportVoidedChecksComponent;
  let fixture: ComponentFixture<ReportVoidedChecksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportVoidedChecksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportVoidedChecksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
