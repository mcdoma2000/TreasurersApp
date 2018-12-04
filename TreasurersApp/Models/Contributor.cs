using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("Contributor", Schema = "dbo")]
    public partial class Contributor
    {
        [Key]
        [JsonProperty("id")]
        public int ContributorID { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [Required()]
        [JsonProperty("addressId")]
        public int? AddressId { get; set; }

    }
}
