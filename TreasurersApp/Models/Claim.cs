using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("Claim", Schema = "Security")]
    public class Claim
    {
        [Required()]
        [Key()]
        [JsonProperty("id")]
        public Guid ClaimID { get; set; }

        [Required()]
        [StringLength(50)]
        [JsonProperty("claimName")]
        public string ClaimName { get; set; }

        [Required()]
        [StringLength(50)]
        [JsonProperty("claimValue")]
        public string ClaimValue { get; set; }
    }

}
