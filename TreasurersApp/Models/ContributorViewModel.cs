using Newtonsoft.Json;

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

        public ContributorViewModel()
        {

        }

        public ContributorViewModel(Contributor contributor)
        {
            this.ContributorId = contributor.ContributorId;
            this.FirstName = contributor.FirstName;
            this.MiddleName = contributor.MiddleName;
            this.LastName = contributor.LastName;
            this.AddressText = null;
        }

    }
}
