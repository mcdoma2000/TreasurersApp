using System;
using System.Linq;
using TreasurersApp.Database;
using TreasurersApp.Models;
using TreasurersApp.Utilities.Reports;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace TreasurersApp.Controllers
{
    [Route("[controller]")]
    // [Authorize(Policy = "CanAccessReports")]

    public class ReportDownloadController : BaseController
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public ReportDownloadController(IHostingEnvironment env, ILoggerFactory loggerFactory) : base(env)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<ReportDownloadController>();
        }

        [HttpPost("report", Name = "DownloadReport", Order = 1)]
        public IActionResult ReportView([FromForm]string reportParameters)
        {
            ReportParameters reportParms = JsonConvert.DeserializeObject<ReportParameters>(reportParameters);
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add(reportParms.ReportName);
            using (var db = new TreasurersAppDbContext(GetDatabasePath()))
            {
                var rpt = db.Reports.SingleOrDefault(x => x.Name == reportParms.ReportName);
                if (rpt == null)
                {
                    throw new ArgumentException("An invalid report name was passed", "Report Name");
                }
                var rptFactory = new ReportFactory(db);
                var rptHandler = rptFactory.Create(reportParms.ReportName);
                rptHandler.ProcessReport(excel, reportParms, db);
            }
            string fileName = string.Format("{0}_{1:yyyyMMdd_hhmmss}.xlsx", reportParms.ReportName, DateTime.Now);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var mstream = new MemoryStream();
            excel.SaveAs(mstream);
            _logger.LogDebug("Content Type: {0} | File Name: {1} | File Size: {2}", contentType, fileName, mstream.Length);
            var fsr = new FileStreamResult(mstream, contentType)
            {
                FileDownloadName = fileName
            };
            return fsr;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}