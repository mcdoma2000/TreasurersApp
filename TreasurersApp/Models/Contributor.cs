using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class Contributor
    {
        public Contributor()
        {
            CashJournals = new List<CashJournal>();
            ContributorAddresses = new List<ContributorAddress>();
            ContributorEmailAddresses = new List<ContributorEmailAddress>();
            ContributorPhoneNumbers = new List<ContributorPhoneNumber>();
        }

        [JsonProperty("id")]
        public int ContributorId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("bahaiId")]
        public string BahaiId { get; set; } // BahaiID (length: 50)

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate

        /// <summary>
        /// Child CashJournals where [CashJournal].[ContributorID] point to this entity (FK_CashJournal_Contributor)
        /// </summary>
        [JsonProperty("cashJournals")]
        public System.Collections.Generic.ICollection<CashJournal> CashJournals { get; set; } // CashJournal.FK_CashJournal_Contributor

        /// <summary>
        /// Child ContributorAddresses where [ContributorAddress].[ContributorID] point to this entity (FK_ContributorAddress_Contributor)
        /// </summary>
        [JsonProperty("contributorAddresses")]
        public System.Collections.Generic.ICollection<ContributorAddress> ContributorAddresses { get; set; } // ContributorAddress.FK_ContributorAddress_Contributor

        /// <summary>
        /// Child ContributorEmailAddresses where [ContributorEmailAddress].[ContributorID] point to this entity (FK_ContributorEmailAddress_Contributor)
        /// </summary>
        [JsonProperty("contributorEmailAddresses")]
        public System.Collections.Generic.ICollection<ContributorEmailAddress> ContributorEmailAddresses { get; set; } // ContributorEmailAddress.FK_ContributorEmailAddress_Contributor

        /// <summary>
        /// Child ContributorPhoneNumbers where [ContributorPhoneNumber].[ContributorID] point to this entity (FK_ContributorPhoneNumber_Contributor)
        /// </summary>
        [JsonProperty("contributorPhoneNumbers")]
        public System.Collections.Generic.ICollection<ContributorPhoneNumber> ContributorPhoneNumbers { get; set; } // ContributorPhoneNumber.FK_ContributorPhoneNumber_Contributor

        [JsonProperty("contributions")]
        public ICollection<CashJournal> CashJournal { get; set; }
    }
}
