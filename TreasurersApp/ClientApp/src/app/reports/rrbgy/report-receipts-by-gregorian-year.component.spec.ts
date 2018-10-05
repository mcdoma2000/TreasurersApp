import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportReceiptsByGregorianYearComponent } from './report-receipts-by-gregorian-year.component';

describe('ReportReceiptsByGregorianYearComponent', () => {
  let component: ReportReceiptsByGregorianYearComponent;
  let fixture: ComponentFixture<ReportReceiptsByGregorianYearComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportReceiptsByGregorianYearComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportReceiptsByGregorianYearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
