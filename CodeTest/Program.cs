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

            Console.WriteLine(GetTimeInterval(model[0], "Video streaming"));
            Console.WriteLine(GetTimeInterval(model[0], "Gym"));



        }

        public static List<BankTransactionModel> FilterOutliers(List<BankTransactionModel> data)
        {
            foreach (var item in data)
            {
                //Grubbs' test
            }
            return null;
        }

        public static string GetTimeInterval(BankModel bankAccount, string transactionText)
        {

            var transactions = bankAccount.AccountTransactions.ToArray();

            var grouped = transactions
                .Where(t => t.TransactionText.Equals(transactionText))
                .OrderBy(dt => dt.TransactionDate)
                .ToList();

            var timeDiffs = new List<TimeSpan>();
            for (var i = 1; i < grouped.Count; i++)
            {

                var timeSpan = grouped[i].TransactionDate - grouped[i - 1].TransactionDate;
                timeDiffs.Add(timeSpan);
            }

            var average = timeDiffs.Average(d => d.Ticks);
            var longAverage = Convert.ToInt64(average);
            var averageTimeSpan = new TimeSpan(longAverage);

            //I assume average between 28-31 days are monthly and 12-16 are biweekly payments

            if (averageTimeSpan > new TimeSpan(28, 0, 0, 0) && averageTimeSpan < new TimeSpan(31, 0, 0, 0))
            {
                return "Monthly payment";
            }
            if (averageTimeSpan > new TimeSpan(12, 0, 0, 0) && averageTimeSpan < new TimeSpan(16, 0, 0, 0))
            {
                return "Biweekly payment";
            }
            return "Not recognized pattern";


        }

        public static double GetBalance(BankModel bank)
        {
            return bank.AccountTransactions.Sum(x => x.TransactionAmount);
        }

        public static double GetBalance(CcModel card)
        {
            //Card amounts are always positive but balance should be calculated as negative
            return card.CardTransactions.Sum(x => x.Amount) * -1;
        }

        public static List<BankModel> FilterData(IEnumerable<BankModel> data)
        {
            var results = new List<BankModel>();

            foreach (var bankObject in data)
            {
                bankObject.AccountTransactions.RemoveAll(x => x.TransactionAmount > 0);

                if (!bankObject.AccountTransactions.Any())
                    continue;

                results.Add(bankObject);
            }

            return results;
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
