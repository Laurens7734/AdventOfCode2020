using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day13 : Day
    {
        List<string> data;
        public Day13(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            int toa = int.Parse(data[0]);
            List<int> busnumbers = new List<int>();
            foreach (string s in data[1].Split(','))
            {
                if (s.Equals("x"))
                    continue;
                busnumbers.Add(int.Parse(s));
            }

            int shortestTime = 100;
            int bussnumber = 0;
            foreach(int i in busnumbers)
            {
                int timeLeft = i - (toa % i);
                if(timeLeft < shortestTime)
                {
                    shortestTime = timeLeft;
                    bussnumber = i;
                }
            }
            return (bussnumber*shortestTime).ToString();
        }

        public string Answer2()
        {
            List<Tuple<int,int>> busnumbers = new List<Tuple<int, int>>();
            int count = 0;
            foreach (string s in data[1].Split(','))
            {
                if (!s.Equals("x"))
                    busnumbers.Add(Tuple.Create(int.Parse(s), count));

                count++;
            }

            int elements = 2;
            long step = busnumbers[0].Item1;

            for(long l = 0; l < long.MaxValue; l += step)
            {
                if (CheckAll(l, new List<Tuple<int, int>>(busnumbers.Take(elements))))
                {
                    if(elements == busnumbers.Count)
                    {
                        return l.ToString();
                    }
                    else 
                    {
                        step *= busnumbers[elements - 1].Item1;
                        elements++;
                    }
                }
            }
            
            return "error";
        }

        bool CheckAll(long num, List<Tuple<int, int>> busnums)
        {
            foreach(Tuple<int,int> t in busnums)
            {
                if (!((num + t.Item2) % t.Item1 == 0))
                    return false;
            }
            return true;
        }
    }
}
