namespace TreasurersApp.Models
{
    using Newtonsoft.Json;

    public class ContributorEmailAddress
    {
        [JsonProperty("id")]
        public int ContributorEmailAddressId { get; set; } // ContributorEmailAddressID (Primary key)

        [JsonProperty("contributorId")]
        public int ContributorId { get; set; } // ContributorID

        [JsonProperty("emailAddressId")]
        public int EmailAddressId { get; set; } // EmailAddressID

        [JsonProperty("preferred")]
        public bool Preferred { get; set; } // Preferred

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate

        // Foreign keys

        /// <summary>
        /// Parent Contributor pointed by [ContributorEmailAddress].([ContributorId]) (FK_ContributorEmailAddress_Contributor)
        /// </summary>
        public Contributor Contributor { get; set; } // FK_ContributorEmailAddress_Contributor

        /// <summary>
        /// Parent EmailAddress pointed by [ContributorEmailAddress].([EmailAddressId]) (FK_ContributorEmailAddress_EmailAddress)
        /// </summary>
        public EmailAddress EmailAddress { get; set; } // FK_ContributorEmailAddress_EmailAddress
    }

}
// </auto-generated>
