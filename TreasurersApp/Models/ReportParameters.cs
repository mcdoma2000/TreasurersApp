using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class ReportParameters
    {
        [Required]
        public string ReportName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<string> Contributors { get; set; }

        [Range(2000, 2100, ErrorMessage = "Gregorian Year must be greater than 1999 and less than 2101.")]
        public int GregorianYear { get; set; }

    }
}
