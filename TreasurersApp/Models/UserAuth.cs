using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class UserAuth
    {
        public UserAuth() : base()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
            IsAuthenticated = false;
            Claims = new List<Claim>();
        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
