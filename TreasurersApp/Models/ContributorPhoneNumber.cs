namespace TreasurersApp.Models
{
    using Newtonsoft.Json;

    public class ContributorPhoneNumber
    {
        [JsonProperty("id")]
        public int ContributorPhoneNumberId { get; set; } // ContributorPhoneNumberID (Primary key)

        [JsonProperty("contributorId")]
        public int ContributorId { get; set; } // ContributorID

        [JsonProperty("phoneNumberId")]
        public int PhoneNumberId { get; set; } // PhoneNumberID

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
        /// Parent Contributor pointed by [ContributorPhoneNumber].([ContributorId]) (FK_ContributorPhoneNumber_Contributor)
        /// </summary>
        [JsonProperty("contributor")]
        public Contributor Contributor { get; set; } // FK_ContributorPhoneNumber_Contributor

        /// <summary>
        /// Parent PhoneNumber pointed by [ContributorPhoneNumber].([PhoneNumberId]) (FK_ContributorPhoneNumber_PhoneNumber)
        /// </summary>
        [JsonProperty("phoneNumber")]
        public PhoneNumber PhoneNumber { get; set; } // FK_ContributorPhoneNumber_PhoneNumber
    }

}
// </auto-generated>
