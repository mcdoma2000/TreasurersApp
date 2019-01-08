using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class PhoneTypeRequest : WebRequest<PhoneType>
    {
        public PhoneTypeRequest(string userName, PhoneType phoneType)
            : base(userName, phoneType)
        {

        }
    }
}
