using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class EmailAddressActionResult : WebResult<EmailAddress>
    {
        public EmailAddressActionResult(bool success, List<string> statusMessages, EmailAddress emailAddress)
            :base(success, statusMessages, emailAddress)
        {
        }
    }
}
