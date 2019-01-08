using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class PhoneTypeActionResult : WebResult<PhoneType>
    {
        public PhoneTypeActionResult(bool success, List<string> statusMessages, PhoneType phoneType)
            :base(success, statusMessages, phoneType)
        {
        }
    }
}
