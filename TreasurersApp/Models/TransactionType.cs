using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            CashJournal = new HashSet<CashJournal>();
        }

        [JsonProperty("id")]
        public int TransactionTypeId { get; set; }

        [JsonProperty("transactionCategoryId")]
        public int TransactionCategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate

        [JsonProperty("transactionCategory")]
        public TransactionCategory TransactionCategory { get; set; }

        [JsonProperty("cashJournals")]
        public ICollection<CashJournal> CashJournal { get; set; }
    }
}
