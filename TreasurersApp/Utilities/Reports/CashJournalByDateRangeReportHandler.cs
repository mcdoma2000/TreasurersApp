using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace TreasurersApp.Utilities.Reports
{
    public class CashJournalByDateRangeReportHandler : IReportHandler
    {
        public void ProcessReport(ExcelPackage excel, TreasurersApp.Models.ReportParameters reportParameters, DbContext db)
        {
        }
    }
}
