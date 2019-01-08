using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TreasurersApp.Models
{
    public partial class TransactionTypeRequest : WebRequest<TransactionType>
    {
        public TransactionTypeRequest(string userName, TransactionType transactionType)
            : base(userName, transactionType)
        {

        }
    }
}
