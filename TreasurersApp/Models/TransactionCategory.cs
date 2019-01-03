using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class TransactionCategory
    {
        public TransactionCategory()
        {
            TransactionTypes = new HashSet<TransactionType>();
        }

        [JsonProperty("id")]
        public int TransactionCategoryId { get; set; }

        [JsonProperty("contributionCategoryName")]
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

        [JsonProperty("transactionTypes")]
        public ICollection<TransactionType> TransactionTypes { get; set; }
    }
}
