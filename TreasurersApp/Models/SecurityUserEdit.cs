using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class SecurityUserEdit
    {
        [JsonProperty("userId")]
        public Guid UserID { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("claims")]
        public ICollection<Claim> Claims { get; set; }

        public SecurityUserEdit()
        {
            this.Claims = new List<Claim>();
            this.UserName = "unknown";
            this.DisplayName = "Unknown, User";
        }
    }
}
