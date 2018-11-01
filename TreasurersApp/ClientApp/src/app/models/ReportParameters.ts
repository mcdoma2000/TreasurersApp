import { Contributor } from './Contributor';

export class ReportParameters {
  reportName = '';
  startDate: Date = null;
  endDate: Date = null;
  contributors: Contributor[] = [];
  gregorianYear: number = null;
}
