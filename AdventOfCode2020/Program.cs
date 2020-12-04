using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToList datareader = new DataToList("InputData/Day4Data.txt");

            //List<int> data = datareader.GetIntList();
            List<string> data = datareader.GetStringList();
            //List<string> data = TestData();

            Day d = new Day4(data);
            Console.WriteLine(d.Answer1());
            Console.WriteLine(d.Answer2());
            Console.ReadKey();
        }

        static List<string> TestData()
        {
            List<string> s = new List<string>();
            s.Add("..##.......");
            s.Add("#...#...#..");
            s.Add(".#....#..#.");

            return s;
        }
    }
}
