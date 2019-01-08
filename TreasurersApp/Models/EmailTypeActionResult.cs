using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class EmailTypeActionResult : WebResult<EmailType>
    {
        public EmailTypeActionResult(bool success, List<string> statusMessages, EmailType emailType)
            :base(success, statusMessages, emailType)
        {
        }
    }
}
