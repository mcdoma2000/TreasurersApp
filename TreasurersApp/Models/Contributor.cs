using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class Contributor
    {
        public Contributor()
        {
            CashJournal = new HashSet<CashJournal>();
        }

        [JsonProperty("id")]
        public int ContributorId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("addressId")]
        public int? AddressId { get; set; }

        [JsonProperty("contributions")]
        public ICollection<CashJournal> CashJournal { get; set; }
    }
}
