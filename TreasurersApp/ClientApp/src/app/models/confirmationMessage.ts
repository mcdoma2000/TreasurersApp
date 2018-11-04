export class ConfirmationMessage {
  severity: string = null;
  summary: string = null;
  detail: string = null;

  constructor(severity: string, summary: string, detail: string) {
    this.severity = severity;
    this.summary = summary;
    this.detail = detail;
  }
}
