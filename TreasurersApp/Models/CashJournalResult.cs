using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class CashJournalResult : WebResult<CashJournal>
    {
        public CashJournalResult(bool success, List<string> statusMessages, CashJournal cashJournal)
            :base(success, statusMessages, cashJournal)
        {
        }
    }
}
