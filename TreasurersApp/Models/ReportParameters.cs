using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TreasurersApp.Models
{
    public class ReportParameters
    {
        [Required]
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [Required]
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("contributors")]
        public List<Contributor> Contributors { get; set; }

        [JsonProperty("gregorianYear")]
        [Range(2000, 2100, ErrorMessage = "Gregorian Year must be greater than 1999 and less than 2101.")]
        public int GregorianYear { get; set; }

    }
}
