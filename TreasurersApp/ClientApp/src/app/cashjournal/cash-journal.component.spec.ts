import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CashJournalComponent } from './cash-journal.component';

describe('CashJournalComponent', () => {
  let component: CashJournalComponent;
  let fixture: ComponentFixture<CashJournalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CashJournalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CashJournalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
