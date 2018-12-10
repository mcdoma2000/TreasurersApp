using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class Report
    {
        [JsonProperty("id")]
        public int ReportId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("configurationJson")]
        public string ConfigurationJson { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }
    }
}
