using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class AddressTypeActionResult : WebResult<AddressType>
    {
        public AddressTypeActionResult(bool success, List<string> statusMessages, AddressType addressType)
            :base(success, statusMessages, addressType)
        {
        }
    }
}
