using CodeTest.Helpers;
using System;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Copy data.json under C:\Logs

            var data = Helper.GetSampleData();

            foreach (var item in data)
            {
                Console.WriteLine($"Account id {item.AccountId} has {Helper.GetBalance(item)} balance.");

            }

            Console.WriteLine();

            var filteredData = Helper.FilterData(data);

            foreach (var account in filteredData)
            {
                Console.WriteLine($"### RESULT FOR {account.AccountOwner}\n");
                Console.Write("Video streaming: ");
                Console.Write(Helper.GetTimeInterval(account, "Video streaming"));

                Console.WriteLine();

                Console.Write("Gym: ");
                Console.Write(Helper.GetTimeInterval(account, "Gym"));
                Console.WriteLine();
                Console.WriteLine();
            }
            
        }


    }
}
