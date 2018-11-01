using TreasurersApp.Database;
using TreasurersApp.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace TreasurersApp.Utilities.Reports
{
    public class CashJournalReportHandler : IReportHandler
    {
        public void ProcessReport(ExcelPackage excel, ReportParameters reportParameters, DbContext db)
        {
            var workSheet = excel.Workbook.Worksheets[reportParameters.ReportName];
            workSheet.Cells[1, 1].Value = "I have successfully processed the report!";
            TreasurersAppDbContext dc = (TreasurersAppDbContext)db;
            var reports = dc.Reports;
            int whichRow = 2;
            foreach (var rpt in reports)
            {
                string cellContents = string.Format("ID: {0} | Name: {1} | Display Name: {2} | Active: {3} | Display Order: {4}",
                    rpt.Id, rpt.Name, rpt.DisplayName, rpt.Active, rpt.DisplayOrder);
                workSheet.Cells[whichRow, 1].Value = cellContents;
                whichRow++;
            }
        }
    }
}
