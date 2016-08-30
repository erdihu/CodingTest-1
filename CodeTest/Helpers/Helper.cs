using CodeTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeTest.Helpers
{
    public static class Helper
    {
        //This method applies Grubbs' test on a target in a given series. 
        //Implementation taken from http://graphpad.com/support/faqid/1598/ and http://www.real-statistics.com/students-t-distribution/identifying-outliers-using-t-distribution/grubbs-test/
        //This test seems to work for single outlier but may not work for multiples.

        public static bool IsOutlier(IEnumerable<double> sample, double target)
        {
            var numbers = sample.ToArray();
            var mean = numbers.Average();
            var sumOfSquaresOfDifferences = numbers.Select(val => Math.Pow((val - mean), 2)).Sum();
            var size = numbers.Length;
            var stdDev = Math.Sqrt(sumOfSquaresOfDifferences / size); 
            const double alpha = 0.05; //or 0.01 for stricter test
            var sig = alpha / 2*size; //2-sided
            var degreeFreedom = size - 2;
            var tCriticalVal = MathNet.Numerics.ExcelFunctions.TInv((1 - sig) / 100, degreeFreedom);
            var x = Math.Pow(tCriticalVal, 2);
            var gCriticalVal = (size - 1) * tCriticalVal / Math.Sqrt(size * (degreeFreedom + x));
            var g = (mean - target) / stdDev;

            return Math.Abs(g) > Math.Abs(gCriticalVal);
        }

        public static string GetTimeInterval(BankModel bankAccount, string transactionText)
        {

            var transactions = bankAccount.AccountTransactions.ToArray();

            var grouped = transactions
                .Where(t => t.TransactionText.Equals(transactionText))
                .OrderBy(dt => dt.TransactionDate)
                .ToList();

            //Take only the transaction amounts
            var amounts = grouped.Select(a => a.TransactionAmount);

            var possibleOutliers = new List<BankTransactionModel>();

            foreach (var item in grouped)
            {
                if (IsOutlier(amounts, item.TransactionAmount))
                {
                    possibleOutliers.Add(item);
                }
            }

            //Remove outliers.
            if (possibleOutliers.Any())
            {
                foreach (var item in possibleOutliers)
                {
                    grouped.Remove(item);
                }
            }

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
