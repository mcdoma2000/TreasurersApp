using Microsoft.EntityFrameworkCore;
using TreasurersApp.Database;
using TreasurersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Utilities.Reports
{
    public class ReportFactory
    {
        Dictionary<string, Report> reportsByName = new Dictionary<string, Report>();

        public ReportFactory(DbContext db)
        {
            var dc = db as BTAContext;
            foreach (var rpt in dc.Report.Where(x => x.Active ?? false).OrderBy(x => x.DisplayOrder).ToList())
            {
                reportsByName.Add(rpt.Name, rpt);
            }
        }

        public IReportHandler Create(string reportName)
        {
            switch (reportName.ToLower())
            {
                case "cashjournal":
                    return new CashJournalReportHandler();
                case "cashjournalbydaterange":
                    return new CashJournalByDateRangeReportHandler();
                case "cashjournalbycontributor":
                    return new CashJournalByContributorReportHandler();
                case "voidedchecks":
                    return new VoidedChecksReportHandler();
                case "receiptsbycontribution":
                    return new ReceiptsByContributionReportHandler();
                case "receiptsbygregorianyear":
                    return new ReceiptsByGregorianYearReportHandler();
                default:
                    return null;
            }
        }
    }
}
