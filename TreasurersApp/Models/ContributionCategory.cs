using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class ContributionCategory
    {
        public ContributionCategory()
        {
            ContributionType = new HashSet<ContributionType>();
        }

        [JsonProperty("id")]
        public int ContributionCategoryId { get; set; }

        [JsonProperty("contributionCategoryName")]
        public string ContributionCategoryName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("contributionTypes")]
        public ICollection<ContributionType> ContributionType { get; set; }
    }
}
