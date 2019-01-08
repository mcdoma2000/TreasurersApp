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
        public Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; } // LastModifiedDate

        /// <summary>
        /// Child CashJournals where [CashJournal].[ContributorID] point to this entity (FK_CashJournal_Contributor)
        /// </summary>
        [JsonProperty("cashJournals")]
        public ICollection<CashJournal> CashJournals { get; set; } // CashJournal.FK_CashJournal_Contributor

        /// <summary>
        /// Child ContributorAddresses where [ContributorAddress].[ContributorID] point to this entity (FK_ContributorAddress_Contributor)
        /// </summary>
        [JsonProperty("contributorAddresses")]
        public ICollection<ContributorAddress> ContributorAddresses { get; set; } // ContributorAddress.FK_ContributorAddress_Contributor

        /// <summary>
        /// Child ContributorEmailAddresses where [ContributorEmailAddress].[ContributorID] point to this entity (FK_ContributorEmailAddress_Contributor)
        /// </summary>
        [JsonProperty("contributorEmailAddresses")]
        public ICollection<ContributorEmailAddress> ContributorEmailAddresses { get; set; } // ContributorEmailAddress.FK_ContributorEmailAddress_Contributor

        /// <summary>
        /// Child ContributorPhoneNumbers where [ContributorPhoneNumber].[ContributorID] point to this entity (FK_ContributorPhoneNumber_Contributor)
        /// </summary>
        [JsonProperty("contributorPhoneNumbers")]
        public ICollection<ContributorPhoneNumber> ContributorPhoneNumbers { get; set; } // ContributorPhoneNumber.FK_ContributorPhoneNumber_Contributor

        [JsonProperty("contributions")]
        public ICollection<CashJournal> CashJournal { get; set; }
    }
}
