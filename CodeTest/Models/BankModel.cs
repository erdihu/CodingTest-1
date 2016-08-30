using System.Collections.Generic;

namespace CodeTest.Models
{
    public class BankModel
    {
        public int AccountId { get; set; }
        public string AccountOwner { get; set; }
        public List<BankTransactionModel> AccountTransactions { get; set; }
    }
}
