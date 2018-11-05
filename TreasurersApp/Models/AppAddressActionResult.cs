using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class AppAddressActionResult
    {
        public bool Success { get; set; } = false;
        public List<string> StatusMessages { get; set; } = new List<string>();
        public AppAddress Address { get; set; } = new AppAddress();

        public AppAddressActionResult(bool success, List<string> statusMessages, AppAddress address)
        {
            Success = success;
            StatusMessages.AddRange(statusMessages);
            Address = address;
        }
    }
}
