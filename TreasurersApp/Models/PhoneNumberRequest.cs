using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class PhoneNumberRequest : WebRequest<PhoneNumber>
    {
        public PhoneNumberRequest(string userName, PhoneNumber phoneNumber)
            : base(userName, phoneNumber)
        {

        }
    }
}
