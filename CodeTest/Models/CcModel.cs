using System.Collections.Generic;

namespace CodeTest.Models
{
    public class CcModel
    {
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public List<CcTransactionModel> CardTransactions { get; set; }

    }
}
