using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  [Table("Claim", Schema = "Security")]
  public class SecurityClaim
  {
        [Required()]
        [Key()]
        [JsonProperty("id")]
        public Guid ClaimID { get; set; }

        [Required()]
        [StringLength(50)]
        [JsonProperty("claimType")]
        public string ClaimType { get; set; }

        [Required()]
        [StringLength(50)]
        [JsonProperty("claimValue")]
        public string ClaimValue { get; set; }
    }

}
