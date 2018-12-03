using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("Address", Schema = "dbo")]
    public partial class Address
    {
        [Key]
        [JsonProperty("id")]
        public int AddressID { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("city")]
        public string City { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("state")]
        public string State { get; set; }

        [Required()]
        [StringLength(100)]
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

    }
}
