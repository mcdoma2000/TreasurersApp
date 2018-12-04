using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class AppContributionTypeActionResult
    {
        public bool Success { get; set; } = false;
        public List<string> StatusMessages { get; set; } = new List<string>();
        public AppContributionType ContributionType { get; set; } = new AppContributionType();

        public AppContributionTypeActionResult(bool success, List<string> statusMessages, AppContributionType contributionType)
        {
            Success = success;
            StatusMessages.AddRange(statusMessages);
            ContributionType = contributionType;
        }
    }
}
