namespace TreasurersApp.Models
{
    using Newtonsoft.Json;

    public class PhoneNumber
    {
        [JsonProperty("id")]
        public int PhoneNumberId { get; set; } // PhoneNumberID (Primary key)

        [JsonProperty("phoneNumber")]
        public string PhoneNumber_ { get; set; } // PhoneNumber (length: 50)

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
        /// Child ContributorPhoneNumbers where [ContributorPhoneNumber].[PhoneNumberID] point to this entity (FK_ContributorPhoneNumber_PhoneNumber)
        /// </summary>
        public System.Collections.Generic.ICollection<ContributorPhoneNumber> ContributorPhoneNumbers { get; set; } // ContributorPhoneNumber.FK_ContributorPhoneNumber_PhoneNumber

        public PhoneNumber()
        {
            ContributorPhoneNumbers = new System.Collections.Generic.List<ContributorPhoneNumber>();
        }
    }

}
// </auto-generated>
