using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class PhoneNumberActionResult : WebResult<PhoneNumber>
    {
        public PhoneNumberActionResult(bool success, List<string> statusMessages, PhoneNumber phoneNumber)
            :base(success, statusMessages, phoneNumber)
        {
        }
    }
}
