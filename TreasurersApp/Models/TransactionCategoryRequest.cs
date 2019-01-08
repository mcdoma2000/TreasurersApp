using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class TransactionCategoryRequest : WebRequest<TransactionCategory>
    {
        public TransactionCategoryRequest(string userName, TransactionCategory transactionCategory)
            : base(userName, transactionCategory)
        {

        }
    }
}
