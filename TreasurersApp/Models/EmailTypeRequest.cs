using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class EmailTypeRequest : WebRequest<EmailType>
    {
        public EmailTypeRequest(string userName, EmailType emailType)
            : base(userName, emailType)
        {

        }
    }
}
