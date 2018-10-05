import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportReceiptsByContributionComponent } from './report-receipts-by-contribution.component';

describe('ReportReceiptsByContributionComponent', () => {
  let component: ReportReceiptsByContributionComponent;
  let fixture: ComponentFixture<ReportReceiptsByContributionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportReceiptsByContributionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportReceiptsByContributionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
