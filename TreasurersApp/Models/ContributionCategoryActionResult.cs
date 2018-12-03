using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class ContributionCategoryActionResult : WebResult<ContributionCategory>
    {
        public ContributionCategoryActionResult(bool success, List<string> statusMessages, ContributionCategory contributionCategory)
            :base(success, statusMessages, contributionCategory)
        {
        }
    }
}
