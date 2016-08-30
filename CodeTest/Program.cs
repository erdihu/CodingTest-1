using CodeTest.Helpers;
using System;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.GetSampleData();

            var filteredData = Helper.FilterData(data);

            foreach (var item in filteredData)
            {
                Console.WriteLine($"Account id {item.AccountId} has {Helper.GetBalance(item)} balance.");

            }

            Console.WriteLine(Helper.GetTimeInterval(filteredData[0], "Video streaming"));
            Console.WriteLine(Helper.GetTimeInterval(filteredData[0], "Gym"));
        }


    }
}
