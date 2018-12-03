using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("CashJournal", Schema = "dbo")]
    public partial class CashJournal
    {
        [Key()]
        [Required()]
        [JsonProperty("id")]
        public int CashJournalID { get; set; }

        [JsonProperty("checkNumber")]
        public int CheckNumber { get; set; }

        [Required()]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [Required()]
        [JsonProperty("contributorId")]
        public int ContributorId { get; set; }

        [Required()]
        [JsonProperty("bahaiId")]
        public string BahaiId { get; set; }

        [Required()]
        [JsonProperty("contributionTypeId")]
        public int ContributionTypeId { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        [Required()]
        [JsonProperty("createdBy")]
        public int CreatedBy { get; set; }

        [Required()]
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Required()]
        [JsonProperty("lastModifedBy")]
        public int LastModifiedBy { get; set; }

        [Required()]
        [JsonProperty("lastModifedDate")]
        public DateTime LastModifiedDate { get; set; }

    }
}
