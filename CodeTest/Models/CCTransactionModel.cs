using System;

namespace CodeTest.Models
{
    public class CcTransactionModel
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionText { get; set; }
        public string BankEventType => "Credit card transaction";
        public double Amount { get; set; }
    }
}
