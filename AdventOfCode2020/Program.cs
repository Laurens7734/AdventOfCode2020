using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToList datareader = new DataToList("InputData/Day2Data.txt");

            //List<int> data = datareader.GetIntList();
            List<string> data = datareader.GetStringList();
            //List<string> data = TestData();

           Day2 d = new Day2(data);
            Console.WriteLine(d.Answer1());
            Console.WriteLine(d.Answer2());
            Console.ReadKey();
        }

        static List<string> TestData()
        {
            List<string> s = new List<string>();
            s.Add("1-3 a: abcde");
            s.Add("1-3 b: cdefg");
            s.Add("2-9 c: ccccccccc");

            return s;
        }
    }
}
