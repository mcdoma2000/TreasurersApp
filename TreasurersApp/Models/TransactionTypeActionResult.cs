using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class TransactionTypeActionResult : WebResult<TransactionType>
    {
        public TransactionTypeActionResult(bool success, List<string> statusMessages, TransactionType transactionType)
            :base(success, statusMessages, transactionType)
        {
        }
    }
}
