using CodeTest.Models;
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

        public HelperTests()
        {
            _data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));
        }

        [Test]
        public void FilterData_ListIncludesPositiveAndNegative_ShouldReturnNegatives()
        {
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

            var bm = new BankModel
            {
                AccountTransactions = new List<BankTransactionModel>()
            };

            var filtered = Helpers.Helper.FilterData(new[] { bm });

            Assert.IsEmpty(filtered);
        }

        [Test]
        public void GetBalance_ListHasPositivesAndNegatives_ShouldReturnSum()
        {
            Assert.IsTrue(Helpers.Helper.GetBalance(_data[0]).Equals(190));
        }
    }
}
