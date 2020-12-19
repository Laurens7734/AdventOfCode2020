using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day18:Day
    {
        List<string> data;
        public Day18(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            long result = 0;
            foreach(string s in data)
            {
                Calculator c = new Calculator(s);
                result += c.Resolve();
            }
            return result.ToString();
        }

        public string Answer2()
        {
            long result = 0;
            foreach (string s in data)
            {
                Calculator c = new Calculator(s, true);
                result += c.Resolve2();
            }
            return result.ToString();
        }
    }
}
