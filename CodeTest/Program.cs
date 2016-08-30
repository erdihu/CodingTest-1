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

            var filteredData = Helper.FilterData(data);

            Console.Write("Video streaming: ");
            Console.Write(Helper.GetTimeInterval(filteredData[0], "Video streaming"));

            Console.WriteLine();

            Console.Write("Gym: ");
            Console.Write(Helper.GetTimeInterval(filteredData[0], "Gym")); //Fails

            Console.WriteLine();
        }


    }
}
