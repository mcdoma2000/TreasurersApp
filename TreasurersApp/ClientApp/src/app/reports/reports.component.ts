import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, ActivationStart } from '@angular/router';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit, OnDestroy {

  currentReportName: string = null;

  constructor(private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

}
