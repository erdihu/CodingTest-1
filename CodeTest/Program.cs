using CodeTest.Helpers;
using System;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.GetSampleData();

            foreach (var item in data)
            {
                Console.WriteLine($"Account id {item.AccountId} has {Helper.GetBalance(item)} balance.");

            }

            var filteredData = Helper.FilterData(data);

            Console.WriteLine(Helper.GetTimeInterval(filteredData[0], "Video streaming"));
            Console.WriteLine(Helper.GetTimeInterval(filteredData[0], "Gym"));
        }


    }
}
