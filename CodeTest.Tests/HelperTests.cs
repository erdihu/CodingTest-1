﻿using CodeTest.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Assert = NUnit.Framework.Assert;


namespace CodeTest.Tests
{
    [TestFixture]
    public class HelperTests
    {
        private BankModel[] _data;

        [Test]
        public void FilterData_ListIncludesPositiveAndNegative_ShouldReturnNegatives()
        {
            _data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));

            var filtered = Helpers.Helper.FilterData(_data);

            Assert.IsTrue(!filtered.Equals(_data));

            foreach (var account in filtered)
            {
                foreach (var item in account.AccountTransactions)
                {
                    Assert.IsTrue(item.TransactionAmount < 0);
                }
            }
        }

        [Test]
        public void FilterData_ListIsEmtpy_ShouldReturnEmpty()
        {
            _data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));
            var bm = new BankModel
            {
                AccountTransactions = new List<BankTransactionModel>()
            };

            var filtered = Helpers.Helper.FilterData(new[] { bm });

            Assert.IsEmpty(filtered);
        }

        [Test]
        public void GetBalance_ListHasPositivesAndNegatives_ShouldReturn190()
        {
            _data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));

            Assert.AreEqual(Helpers.Helper.GetBalance(_data[0]), 190);

        }

        [Test]
        public void GetTimeInterval_PaymentIsBiWeekly_ShouldReturnBiWeekly()
        {
            _data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));

            //The following will fail
            Assert.AreEqual(Helpers.Helper.GetTimeInterval(_data[0], "Gym"), "Biweekly payment");
        }

        [Test]
        public void IsOutlier_GiveProvenData_ShouldReturnOutsider()
        {
            double[] data = { 199.31, 199.53, 200.19, 200.82, 201.92, 201.95, 202.18, 245.57 };
            bool[] actual = { false, false, false, false, false, false, false, true };

            bool[] test = new bool[8];

            for (int i = 0; i < data.Length; i++)
            {
                test[i] = Helpers.Helper.IsOutlier(data, data[i]);
            }

            Assert.AreEqual(actual, test);
        }
    }
}
