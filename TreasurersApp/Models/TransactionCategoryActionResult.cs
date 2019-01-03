using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    public class TransactionCategoryActionResult : WebResult<TransactionCategory>
    {
        public TransactionCategoryActionResult(bool success, List<string> statusMessages, TransactionCategory transactionCategory)
            :base(success, statusMessages, transactionCategory)
        {
        }
    }
}
