using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class ContributionType
    {
        public ContributionType()
        {
            CashJournal = new HashSet<CashJournal>();
        }

        [JsonProperty("id")]
        public int ContributionTypeId { get; set; }

        [JsonProperty("contributionCategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("contributionTypeName")]
        public string ContributionTypeName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("category")]
        public ContributionCategory Category { get; set; }

        [JsonProperty("cashJournals")]
        public ICollection<CashJournal> CashJournal { get; set; }
    }
}
