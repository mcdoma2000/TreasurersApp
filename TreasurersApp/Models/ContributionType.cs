using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("ContributionType", Schema = "dbo")]
    public partial class ContributionType
    {
        [Key]
        [JsonProperty("id")]
        public int ContributionTypeID { get; set; }

        [Required()]
        [JsonProperty("categoryId")]
        public string CategoryID { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("contributionTypeName")]
        public string ContributionTypeName { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
