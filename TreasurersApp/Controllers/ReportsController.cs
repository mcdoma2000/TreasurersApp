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
        public ReportsController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context)
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "ReportsGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Report> list = new List<Report>();

            try
            {
                if (Context.Report.Count() > 0)
                {
                    list = Context.Report.Where(r => r.Active ?? false).OrderBy(r => r.DisplayOrder).ToList();
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Reports");
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
                entity = Context.Report.Find(id);
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

            var rpt = Context.Report.SingleOrDefault(x => x.Name == reportParameters.ReportName);
            if (rpt == null)
            {
                throw new ArgumentException("An invalid report name was passed", "Report Name");
            }
            var rptFactory = new ReportFactory(Context);
            var rptHandler = rptFactory.Create(reportParameters.ReportName);
            rptHandler.ProcessReport(excel, reportParameters, Context);

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