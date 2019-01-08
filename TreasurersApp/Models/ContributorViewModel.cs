using Newtonsoft.Json;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class ContributorViewModel
    {
        [JsonProperty("id")]
        public int ContributorId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("addressId")]
        public int? AddressId { get; set; }

        [JsonProperty("addressText")]
        public string AddressText { get; set; }

        [JsonProperty("emailAddressId")]
        public int? EmailAddressId { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("phoneNumberId")]
        public int? PhoneNumberId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        public ContributorViewModel()
        {

        }

        public ContributorViewModel(Contributor contributor)
        {
            Address adr = null;
            EmailAddress email = null;
            PhoneNumber phone = null;
            foreach (var cadr in contributor.ContributorAddresses)
            {
                if (cadr.Preferred)
                {
                    adr = cadr.Address;
                }
            }
            foreach (var ceml in contributor.ContributorEmailAddresses)
            {
                if (ceml.Preferred)
                {
                    email = ceml.EmailAddress;
                }
            }
            foreach (var cphn in contributor.ContributorPhoneNumbers)
            {
                if (cphn.Preferred)
                {
                    phone = cphn.PhoneNumber;
                }
            }
            this.ContributorId = contributor.ContributorId;
            this.FirstName = contributor.FirstName;
            this.MiddleName = contributor.MiddleName;
            this.LastName = contributor.LastName;
            this.AddressId = adr?.AddressId;
            this.AddressText = adr == null ? "" : string.Format("{0}, {1}, {2} {3}", adr.AddressLine1, adr.City, adr.State, adr.PostalCode);
            this.PhoneNumberId = phone?.PhoneNumberId;
            this.PhoneNumber = phone?.PhoneNumber_;
            this.EmailAddressId = email?.EmailAddressId;
            this.EmailAddress = email?.Email;
        }

    }
}
