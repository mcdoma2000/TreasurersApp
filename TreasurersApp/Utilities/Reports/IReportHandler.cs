using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TreasurersApp.Models;

namespace TreasurersApp.Utilities.Reports
{
    public interface IReportHandler
    {
        void ProcessReport(ExcelPackage excel, ReportParameters reportParameters, DbContext db);
    }
}
