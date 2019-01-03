using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class AddressType
    {
        [JsonProperty("id")]
        public int AddressTypeId { get; set; } // AddressTypeID (Primary key)

        [JsonProperty("name")]
        public string Name { get; set; } // Name (length: 10)

        [JsonProperty("description")]
        public string Description { get; set; } // Description (length: 50)

        [JsonProperty("active")]
        public bool Active { get; set; } // Active

        [JsonProperty("createdBy")]
        public System.Guid CreatedBy { get; set; } // CreatedBy

        [JsonProperty("createdDate")]
        public System.DateTime CreatedDate { get; set; } // CreatedDate

        [JsonProperty("lastModifiedBy")]
        public System.Guid LastModifiedBy { get; set; } // LastModifiedBy

        [JsonProperty("lastModifiedDate")]
        public System.DateTime LastModifiedDate { get; set; } // LastModifiedDate
    }
}
