import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportCashJournalComponent } from './report-cash-journal.component';

describe('ReportCashJournalComponent', () => {
  let component: ReportCashJournalComponent;
  let fixture: ComponentFixture<ReportCashJournalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportCashJournalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportCashJournalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
