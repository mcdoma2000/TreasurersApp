using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class User
    {
        public User()
        {
            UserClaims = new HashSet<UserClaim>();
        }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("userClaims")]
        public ICollection<UserClaim> UserClaims { get; set; }
    }
}
