using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day10 : Day
    {
        List<int> data;

        public Day10(List<string> d)
        {
            List<int> temp = new List<int>();
            foreach(string s in d)
            {
                temp.Add(int.Parse(s));
            }
            data = temp;
        }

        public string Answer1()
        {
            data.Add(0);
            int amount1 = 0, amount3 = 0;
            data.Sort();
            for(int i = 1; i < data.Count; i++)
            {
                if (data[i] == data[i - 1] + 1)
                    amount1++;
                if (data[i] == data[i - 1] + 3)
                    amount3++;
            }
            amount3++;

            return (amount1*amount3).ToString();
        }

        public string Answer2()
        {
            int count = 0;
            List<int> nums = new List<int>();
            for(int i = 1; i< data.Count; i++)
            {
                if (data[i] == data[i - 1] + 1)
                    count++;
                if (data[i] == data[i - 1] + 3)
                {
                    if(count > 0)
                    {
                        nums.Add(PossibleArangements(count, 3));
                        count = 0;
                    }
                }
            }

            if (count > 0)
            {
                nums.Add(PossibleArangements(count, 3));
                count = 0;
            }

            long answer = 1;
            foreach(int j in nums)
            {
                answer *= j;
            }

            return answer.ToString();
        }

        int PossibleArangements(int gaps, int maxjump)
        {
            //there seems to be a pattern but i didn't feel like exploring it further right now
            if (gaps <= maxjump)
                return (int)(Math.Pow(2, gaps - 1));

            else if (gaps <= (2 * maxjump))
                return (int)(Math.Pow(2, gaps - 1) - 1);
            else
                return -1;
        }
    }
}
