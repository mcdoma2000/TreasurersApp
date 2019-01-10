using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class SecurityUserAuth
    {
        public SecurityUserAuth() : base()
        {
            UserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            UserName = "";
            BearerToken = string.Empty;
            IsAuthenticated = false;
            Claims = new List<ClaimViewModel>();
        }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("bearerToken")]
        public string BearerToken { get; set; }

        [JsonProperty("isAuthenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty("claims")]
        public List<ClaimViewModel> Claims { get; set; }
    }
}
