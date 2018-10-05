using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Models
{
    public class ReportStatus
    {
        public bool success { get; set; }
        public List<string> messages { get; set; }

        public ReportStatus()
        {
            this.success = false;
            this.messages = new List<string>();
        }
    }
}
