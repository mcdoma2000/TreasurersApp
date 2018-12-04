using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class ContributorActionResult : WebResult<Contributor>
    {
        public ContributorActionResult(bool success, List<string> statusMessages, Contributor contributor)
            :base(success, statusMessages, contributor)
        {
        }
    }
}
