using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class AddressRequest : WebRequest<Address>
    {
        public AddressRequest(string userName, Address address)
            : base(userName, address)
        {

        }
    }
}