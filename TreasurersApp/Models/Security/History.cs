using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class History
    {
        [JsonProperty("id")]
        public int HistoryId { get; set; } // HistoryID (Primary key)

        [JsonProperty("idChanged")]
        public int IdChanged { get; set; } // IdChanged

        [JsonProperty("recordJson")]
        public string RecordJson { get; set; } // RecordJson

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate
    }
}
