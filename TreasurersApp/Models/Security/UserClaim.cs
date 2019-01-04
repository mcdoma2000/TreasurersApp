using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class UserClaim
    {
        [JsonProperty("id")]
        public Guid UserClaimId { get; set; }

        [JsonProperty("claimId")]
        public Guid ClaimId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }


        [JsonProperty("claim")]
        public Claim Claim { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public UserClaim()
        {
            UserClaimId = System.Guid.NewGuid();
        }
    }
}
