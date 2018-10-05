import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportCashJournalByDateRangeComponent } from './report-cash-journal-by-date-range.component';

describe('ReportCashJournalByDateRangeComponent', () => {
  let component: ReportCashJournalByDateRangeComponent;
  let fixture: ComponentFixture<ReportCashJournalByDateRangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportCashJournalByDateRangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportCashJournalByDateRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
