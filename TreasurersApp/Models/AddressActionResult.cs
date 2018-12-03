using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class AddressActionResult : WebResult<Address>
    {
        public AddressActionResult(bool success, List<string> statusMessages, Address address)
            :base(success, statusMessages, address)
        {
        }
    }
}
