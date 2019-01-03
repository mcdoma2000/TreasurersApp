using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class Address
    {
        [JsonProperty("id")]
        public int AddressId { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate

        /// <summary>
        /// Child ContributorAddresses where [ContributorAddress].[AddressID] point to this entity (FK_ContributorAddress_Address)
        /// </summary>
        public System.Collections.Generic.ICollection<ContributorAddress> ContributorAddresses { get; set; } // ContributorAddress.FK_ContributorAddress_Address

        public Address()
        {
            ContributorAddresses = new System.Collections.Generic.List<ContributorAddress>();
        }

    }
}
