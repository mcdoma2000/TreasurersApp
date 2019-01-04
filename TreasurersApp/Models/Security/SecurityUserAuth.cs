using Newtonsoft.Json;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class SecurityUserAuth
    {
        public SecurityUserAuth() : base()
        {
            UserName = "";
            BearerToken = string.Empty;
            IsAuthenticated = false;
            Claims = new List<ClaimViewModel>();
        }

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
