using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class ContributionTypeViewModel
    {
        [JsonProperty("id")]
        public int ContributionTypeID { get; set; }

        [JsonProperty("contributionCategoryId")]
        public int CategoryID { get; set; }

        [JsonProperty("contributionCategoryDescription")]
        public string CategoryDescription { get; set; }

        [JsonProperty("contributionTypeName")]
        public string ContributionTypeName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
