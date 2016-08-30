using CodeTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;


namespace CodeTest.Tests
{
    [TestClass]
    public class HelperTests
    {
        private BankModel[] data;
        [TestInitialize]
        public void TestInitialize()
        {
            data = JsonConvert.DeserializeObject<BankModel[]>(File.ReadAllText("C:\\Logs\\data.json"));
        }

        [TestMethod]
        public void FilterData_ListIncludesPositiveAndNegative_ShouldReturnNegatives()
        {
            var after = Helpers.Helper.FilterData(data);

        }
    }
}
