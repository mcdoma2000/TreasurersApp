using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class EmailRequest : WebRequest<EmailAddress>
    {
        public EmailRequest(string userName, EmailAddress emailAddress)
            : base(userName, emailAddress)
        {

        }
    }
}
