using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TreasurersApp.Database;
using TreasurersApp.Utilities.Reports;
using TreasurersApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        public ReportsController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "ReportsGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Report> list = new List<Report>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.Reports.Count() > 0)
                    {
                        list = db.Reports.Where(r => r.Active ?? false).OrderBy(r => r.DisplayOrder).ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Reports");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all reports");
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "ReportsGetById")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            Report entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.Reports.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                                        "Can't Find Report: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single report.");
            }

            return ret;
        }

        [HttpPost("execute", Name = "ReportExecute")]
        public IActionResult ExecuteReport([FromBody]ReportParameters reportParameters)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add(reportParameters.ReportName);

            using (var db = new BTAContext())
            {
                var rpt = db.Reports.SingleOrDefault(x => x.Name == reportParameters.ReportName);
                if (rpt == null)
                {
                    throw new ArgumentException("An invalid report name was passed", "Report Name");
                }
                var rptFactory = new ReportFactory(db);
                var rptHandler = rptFactory.Create(reportParameters.ReportName);
                rptHandler.ProcessReport(excel, reportParameters, db);
            }

            string fileName = string.Format("{0}_{1:yyyyMMdd_hhmmss}.xlsx", reportParameters.ReportName, DateTime.Now);
            var mstream = new MemoryStream();
            excel.SaveAs(mstream);
            return new FileStreamResult(mstream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = string.Format("{0}_{1:yyyyMMdd_hhmmss}.xlsx", reportParameters.ReportName, DateTime.Now)
            };

        }
    }
}