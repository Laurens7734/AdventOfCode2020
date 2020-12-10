using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Day01 : Day
    {
        List<int> numbers = new List<int>();

        public Day01(List<string> lines)
        {
            foreach(string s in lines)
            {
                numbers.Add(int.Parse(s));
            }
        }

        public string Answer1()
        {
            foreach(int i in numbers)
            {
                int a = 2020 - i;
                if (numbers.Contains(a))
                {
                    return "" + (i * a);
                }
            }
            return "error stuff not found";
        }

        public string Answer2()
        {
            for(int i = 0; i < numbers.Count; i++)
            {
                for(int j = i+1; j < numbers.Count; j++)
                {
                    int a = 2020 - (numbers[i] + numbers[j]);
                    if (numbers.Contains(a))
                    {
                        return "" + (numbers[i] * numbers[j] * a);
                    }
                }
            }
            return "error stuff not found";
        }
    }
}
