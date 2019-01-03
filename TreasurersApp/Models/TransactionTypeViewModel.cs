using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class TransactionTypeViewModel
    {
        [JsonProperty("id")]
        public int TransactionTypeID { get; set; }

        [JsonProperty("transactionCategoryId")]
        public int TransactionCategoryID { get; set; }

        [JsonProperty("contributionCategoryDescription")]
        public string TransactionCategoryDescription { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
