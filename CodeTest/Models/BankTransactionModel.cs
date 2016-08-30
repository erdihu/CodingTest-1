using System;

namespace CodeTest.Models
{
    public class BankTransactionModel
    {

        public DateTime TransactionDate { get; set; }
        public string TransactionText { get; set; }
        public EventType EventType { get; set; }
        public string Recipient { get; set; }
        public double TransactionAmount { get; set; }
    }
}
