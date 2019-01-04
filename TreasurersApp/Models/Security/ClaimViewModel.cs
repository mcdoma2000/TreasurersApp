using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class ClaimViewModel
    {
        [JsonProperty("id")]
        public Guid ClaimId { get; set; }

        [JsonProperty("claimType")]
        public string ClaimType { get; set; }

        [JsonProperty("claimValue")]
        public string ClaimValue { get; set; }

    }
}
