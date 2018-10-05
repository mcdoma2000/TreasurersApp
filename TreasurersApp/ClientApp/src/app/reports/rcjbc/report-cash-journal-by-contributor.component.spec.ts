import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportCashJournalByContributorComponent } from './report-cash-journal-by-contributor.component';

describe('ReportCashJournalByContributorComponent', () => {
  let component: ReportCashJournalByContributorComponent;
  let fixture: ComponentFixture<ReportCashJournalByContributorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportCashJournalByContributorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportCashJournalByContributorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
