using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class SecurityUserAuth
    {
        public SecurityUserAuth() : base()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
            IsAuthenticated = false;
            Claims = new List<SecurityClaim>();
        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }

        public List<SecurityClaim> Claims { get; set; }
    }
}
