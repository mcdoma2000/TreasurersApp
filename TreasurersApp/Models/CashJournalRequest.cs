using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class CashJournalRequest : WebRequest<CashJournal>
    {
        public CashJournalRequest(string userName, CashJournal cashJournal)
            : base(userName, cashJournal)
        {

        }
    }
}
