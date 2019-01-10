﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public class AddressTypeRequest : WebRequest<AddressType>
    {
        public AddressTypeRequest(string userName, AddressType addressType) : base(userName, addressType)
        {

        }
    }
}
