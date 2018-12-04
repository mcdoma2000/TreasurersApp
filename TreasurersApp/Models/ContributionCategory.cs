using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("ContributionCategory", Schema = "dbo")]
    public partial class ContributionCategory
    {
        [Key]
        [JsonProperty("id")]
        public int ContributionCategoryID { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("contributionCategoryName")]
        public string ContributionCategoryName { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
