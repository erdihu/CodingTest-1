using CodeTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetSampleData();
            BankModel[] model = { data };

            var filteredData = FilterData(model);

            Console.WriteLine($"Account id {data.AccountId} has {GetBalance(data)} balance.");




        }

        public static int GetTimeInterval(BankModel bankAccount, string transactionText)
        {
            var transactions = bankAccount.AccountTransactions.ToArray();

            for (int i = transactions.Length; i-- > 0;)
            {
                transactions[i]
            }


        }

        public static double GetBalance(BankModel bank)
        {
            return bank.AccountTransactions.Sum(x => x.TransactionAmount);
        }

        public static double GetBalance(CcModel card)
        {
            return card.CardTransactions.Sum(x => x.Amount) * -1; //Card amounts are always positive but balance should be calculated as negative
        }

        public static BankModel[] FilterData(BankModel[] data)
        {
            foreach (var bankObject in data)
            {
                var toBeDeleted = new List<BankTransactionModel>();
                foreach (var item in bankObject.AccountTransactions)
                {
                    if (item.TransactionAmount > 0)
                    {
                        toBeDeleted.Add(item);
                    }
                }

                //Delete
                foreach (var item in toBeDeleted)
                {
                    bankObject.AccountTransactions.Remove(item);
                }


                if (!bankObject.AccountTransactions.Any())
                {
                    return null;
                }
            }

            return data;
        }

        public static BankModel GetSampleData()
        {
            return JsonConvert.DeserializeObject<BankModel>(File.ReadAllText("C:\\Logs\\data.json"));
            #region hardcoded
            //string format = "yyyy-MM-dd";
            //CultureInfo provider = CultureInfo.InvariantCulture;

            //var bank = new BankModel
            //{
            //    AccountId = 1,
            //    AccountOwner = "Owner1",
            //    AccountTransactions = new List<BankTransactionModel>()
            //};

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -200,
            //    TransactionDate = DateTime.ParseExact("2016-08-01", format, provider),
            //    TransactionText = "Gym"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Transaction,
            //    Recipient = String.Empty,
            //    TransactionAmount = -99,
            //    TransactionDate = DateTime.ParseExact("2016-07-23", format, provider),
            //    TransactionText = "Video streaming"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -200,
            //    TransactionDate = DateTime.ParseExact("2016-07-18", format, provider),
            //    TransactionText = "Gym"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -200,
            //    TransactionDate = DateTime.ParseExact("2016-07-04", format, provider),
            //    TransactionText = "Gym"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -50,
            //    TransactionDate = DateTime.ParseExact("2016-06-28", format, provider),
            //    TransactionText = "Gym"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Transaction,
            //    Recipient = String.Empty,
            //    TransactionAmount = 1337,
            //    TransactionDate = DateTime.ParseExact("2016-06-25", format, provider),
            //    TransactionText = "Salary"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Transaction,
            //    Recipient = String.Empty,
            //    TransactionAmount = -99,
            //    TransactionDate = DateTime.ParseExact("2016-06-22", format, provider),
            //    TransactionText = "Video streaming"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -50,
            //    TransactionDate = DateTime.ParseExact("2016-06-20", format, provider),
            //    TransactionText = "Gym"
            //});

            //bank.AccountTransactions.Add(new BankTransactionModel
            //{
            //    EventType = EventType.Payment,
            //    Recipient = "123-456",
            //    TransactionAmount = -99,
            //    TransactionDate = DateTime.ParseExact("2016-05-23", format, provider),
            //    TransactionText = "Video streaming"
            //});

            //return bank;
            #endregion
        }
    }
}
