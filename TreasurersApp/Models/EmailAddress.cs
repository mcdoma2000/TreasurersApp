namespace TreasurersApp.Models
{
    using Newtonsoft.Json;

    public class EmailAddress
    {
        [JsonProperty("id")]
        public int EmailAddressId { get; set; } // EmailAddressID (Primary key)

        [JsonProperty("email")]
        public string Email { get; set; } // Email (length: 256)

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate

        // Reverse navigation

        /// <summary>
        /// Child ContributorEmailAddresses where [ContributorEmailAddress].[EmailAddressID] point to this entity (FK_ContributorEmailAddress_EmailAddress)
        /// </summary>
        public System.Collections.Generic.ICollection<ContributorEmailAddress> ContributorEmailAddresses { get; set; } // ContributorEmailAddress.FK_ContributorEmailAddress_EmailAddress

        public EmailAddress()
        {
            ContributorEmailAddresses = new System.Collections.Generic.List<ContributorEmailAddress>();
        }
    }

}
// </auto-generated>
