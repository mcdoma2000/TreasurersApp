using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class ContributorRequest : WebRequest<Contributor>
    {
        public ContributorRequest(string userName, Contributor contributor)
            : base(userName, contributor)
        {

        }
    }
}
