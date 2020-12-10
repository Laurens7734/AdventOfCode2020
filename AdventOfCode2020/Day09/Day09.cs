using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day09 : Day
    {
        List<long> data;

        public Day09(List<string> d)
        {
            data = new List<long>();
            foreach(string s in d)
            {
                data.Add(long.Parse(s));
            }
            //data = d;
        }

        public string Answer1()
        {
            List<long> valid25 = new List<long>();
            foreach (long l in data)
            {
                if (valid25.Count < 25) 
                {
                    valid25.Add(l);
                    continue;
                }

                if (Validate(l, valid25))
                {
                    valid25.RemoveAt(0);
                    valid25.Add(l);
                }
                else
                    return l.ToString();

            }
            return "all are valid";
        }

        public string Answer2()
        {
            List<long> sum = FindSum(long.Parse(Answer1()));
            sum.Sort();
            long answer = sum[0] + sum[^1];

            return answer.ToString();
        }

        bool Validate(long l, List<long> valid25)
        {
            foreach(long a in valid25)
            {
                if (l - a == a)
                    continue;

                if (valid25.Contains(l - a))
                    return true;
            }
            return false;
        }

        List<long> FindSum(long mistake)
        {
            List<long> possibleSum = new List<long>();
            long currentSum = 0;

            foreach (long l in data)
            {
                currentSum += l;
                possibleSum.Add(l);

                if (currentSum > mistake)
                {
                    while (currentSum > mistake)
                    {
                        currentSum -= possibleSum[0];
                        possibleSum.RemoveAt(0);
                    }
                }

                if (currentSum == mistake && possibleSum.Count > 1)
                {
                    return possibleSum;
                }
            }
            return null;
        }
    }
}