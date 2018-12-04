using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class ContributionTypeActionResult : WebResult<ContributionType>
    {
        public ContributionTypeActionResult(bool success, List<string> statusMessages, ContributionType contributionType)
            :base(success, statusMessages, contributionType)
        {
        }
    }
}
