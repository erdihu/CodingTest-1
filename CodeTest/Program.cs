using CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetSampleData();

            var filteredData = FilterData(data);

            Console.WriteLine($"Account id {data.AccountId} has {GetBalance(data)} balance");


        }

        public static double GetBalance(BankModel bank)
        {
            return bank.AccountTransactions.Sum(x => x.TransactionAmount);
        }

        public static double GetBalance(CcModel card)
        {
            return card.CardTransactions.Sum(x => x.Amount) * -1;
        }

        public static BankModel FilterData(BankModel data)
        {
            var toBeDeleted = new List<BankTransactionModel>();
            foreach (var item in data.AccountTransactions)
            {
                if (item.TransactionAmount > 0)
                {
                    toBeDeleted.Add(item);
                }
            }

            //Delete
            foreach (var item in toBeDeleted)
            {
                data.AccountTransactions.Remove(item);
            }


            if (!data.AccountTransactions.Any())
            {
                return null;
            }

            return data;
        }

        public static BankModel GetSampleData()
        {
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            var bank = new BankModel
            {
                AccountId = 1,
                AccountOwner = "Owner1",
                AccountTransactions = new List<BankTransactionModel>()
            };

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -200,
                TransactionDate = DateTime.ParseExact("2016-08-01", format, provider),
                TransactionText = "Gym"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Transaction,
                Recipient = String.Empty,
                TransactionAmount = -99,
                TransactionDate = DateTime.ParseExact("2016-07-23", format, provider),
                TransactionText = "Video streaming"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -200,
                TransactionDate = DateTime.ParseExact("2016-07-18", format, provider),
                TransactionText = "Gym"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -200,
                TransactionDate = DateTime.ParseExact("2016-07-04", format, provider),
                TransactionText = "Gym"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -50,
                TransactionDate = DateTime.ParseExact("2016-06-28", format, provider),
                TransactionText = "Gym"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Transaction,
                Recipient = String.Empty,
                TransactionAmount = 1337,
                TransactionDate = DateTime.ParseExact("2016-06-25", format, provider),
                TransactionText = "Salary"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Transaction,
                Recipient = String.Empty,
                TransactionAmount = -99,
                TransactionDate = DateTime.ParseExact("2016-06-22", format, provider),
                TransactionText = "Video streaming"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -50,
                TransactionDate = DateTime.ParseExact("2016-06-20", format, provider),
                TransactionText = "Gym"
            });

            bank.AccountTransactions.Add(new BankTransactionModel
            {
                EventType = EventType.Payment,
                Recipient = "123-456",
                TransactionAmount = -99,
                TransactionDate = DateTime.ParseExact("2016-05-23", format, provider),
                TransactionText = "Video streaming"
            });

            return bank;
        }
    }
}
