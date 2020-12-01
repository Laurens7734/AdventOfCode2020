using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToList datareader = new DataToList("InputData/Day1Data.txt");
            List<int> data = datareader.GetIntList();
            Day1 d = new Day1(data);
            Console.WriteLine(d.answer1());
            Console.WriteLine(d.answer2());
            Console.ReadKey();
        }
    }
}
