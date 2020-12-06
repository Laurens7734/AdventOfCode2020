using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToList datareader = new DataToList("InputData/Day6Data.txt");

            //List<int> data = datareader.GetIntList();
            List<string> data = datareader.GetStringList();
            //List<string> data = TestData();

            Day d = new Day6(data);
            Console.WriteLine(d.Answer1());
            Console.WriteLine(d.Answer2());
            Console.ReadKey();
        }
    }
}
