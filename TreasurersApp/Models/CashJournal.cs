using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class CashJournal
    {
        [JsonProperty("id")]
        public int CashJournalId { get; set; }

        [JsonProperty("checkNumber")]
        public int? CheckNumber { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("contributorId")]
        public int ContributorId { get; set; }

        [JsonProperty("transactionTypeId")]
        public int TransactionTypeId { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        [JsonProperty("createdBy")]
        public Guid CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("lastModifiedBy")]
        public Guid LastModifiedBy { get; set; }

        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }


        [JsonProperty("transactionType")]
        public TransactionType TransactionType { get; set; }

        [JsonProperty("contributor")]
        public Contributor Contributor { get; set; }
    }
}
