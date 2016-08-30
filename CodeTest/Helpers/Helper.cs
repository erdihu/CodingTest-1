using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTest.Models;
using Newtonsoft.Json;

namespace CodeTest.Helpers
{
    public static class Helper
    {
        public static List<BankTransactionModel> FilterOutliers(List<BankTransactionModel> data)
        {
            foreach (var item in data)
            {
                //Grubbs' test or any other 
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

            //Here we should call FilterOutliers() method and continue on working with the returned list

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

        public static BankModel[] FilterData(IEnumerable<BankModel> data)
        {
            var results = new List<BankModel>();

            foreach (var bankObject in data)
            {
                bankObject.AccountTransactions.RemoveAll(x => x.TransactionAmount > 0);

                if (!bankObject.AccountTransactions.Any())
                    continue;

                results.Add(bankObject);
            }

            return results.ToArray();
        }

        public static BankModel[] GetSampleData()
        {
            return JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));
        }
    }
}
