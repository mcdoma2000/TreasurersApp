using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class Claim
    {
        public Claim()
        {
            UserClaim = new HashSet<UserClaim>();
        }

        [JsonProperty("id")]
        public Guid ClaimId { get; set; }

        [JsonProperty("claimType")]
        public string ClaimType { get; set; }

        [JsonProperty("claimValue")]
        public string ClaimValue { get; set; }

        [JsonProperty("userClaims")]
        public ICollection<UserClaim> UserClaim { get; set; }
    }
}
