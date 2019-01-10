using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TreasurersApp.Models
{
    public class ReportParametersViewModel
    {
        [JsonProperty("json")]
        public string Json { get; set; }

        public ReportParametersViewModel()
        {
            Json = "";
        }

        public ReportParametersViewModel(string json)
        {
            Json = json;
        }

        public ReportParameters ToReportParameters()
        {
            if (!string.IsNullOrEmpty(Json))
            {
                return JsonConvert.DeserializeObject<ReportParameters>(Json);
            }
            else
            {
                return null;
            }
        }

    }
}
