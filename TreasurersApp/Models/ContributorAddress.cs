namespace TreasurersApp.Models
{
    using Newtonsoft.Json;

    public class ContributorAddress
    {
        [JsonProperty("id")]
        public int ContributorAddressId { get; set; } // ContributorAddressID (Primary key)

        [JsonProperty("contributorId")]
        public int ContributorId { get; set; } // ContributorID

        [JsonProperty("addressId")]
        public int AddressId { get; set; } // AddressID

        [JsonProperty("addressTypeId")]
        public int AddressTypeId { get; set; } // AddressTypeID

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
        /// Parent Address pointed by [ContributorAddress].([AddressId]) (FK_ContributorAddress_Address)
        /// </summary>
        public Address Address { get; set; } // FK_ContributorAddress_Address

        /// <summary>
        /// Parent Contributor pointed by [ContributorAddress].([ContributorId]) (FK_ContributorAddress_Contributor)
        /// </summary>
        public Contributor Contributor { get; set; } // FK_ContributorAddress_Contributor
    }

}
// </auto-generated>
