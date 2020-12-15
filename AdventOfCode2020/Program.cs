using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToList datareader = new DataToList("InputData/Day15Data.txt");
            List<string> data = datareader.GetStringList();

            Day d = new Day15(data);
            Console.WriteLine(d.Answer1());
            Console.WriteLine(d.Answer2());
            Console.ReadKey();
        }
    }
}
