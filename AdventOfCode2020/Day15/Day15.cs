using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day15:Day
    {
        List<string> data;
        public Day15(List<string> d)
        {
            data = new List<string>(d[0].Split(','));
        }

        public string Answer1()
        {
            Dictionary<int, int> spokenNumbers = new Dictionary<int, int>();
            int nextnum = -1;
            for(int i = 0; i < 2019; i++)
            {
                if (i < data.Count)
                {
                    int dataread = int.Parse(data[i]);
                    if (spokenNumbers.ContainsKey(dataread))
                    {
                        nextnum = i - spokenNumbers[dataread];
                        spokenNumbers[dataread] = i;
                    }
                    else
                    {
                        nextnum = 0;
                        spokenNumbers.Add(dataread, i);
                    }
                }
                else
                {
                    if (!spokenNumbers.ContainsKey(nextnum)){
                        spokenNumbers.Add(nextnum, i);
                        nextnum = 0;
                    }
                    else
                    {
                        int temp = i - spokenNumbers[nextnum];
                        spokenNumbers[nextnum] = i;
                        nextnum = temp;
                    }
                }
            }
            return nextnum.ToString();
        }

        public string Answer2()
        {
            //not really efficient but fast eanough for this one
            Dictionary<int, int> spokenNumbers = new Dictionary<int, int>();
            int nextnum = -1;
            for (int i = 0; i < 29999999; i++)
            {
                if (i < data.Count)
                {
                    int dataread = int.Parse(data[i]);
                    if (spokenNumbers.ContainsKey(dataread))
                    {
                        nextnum = i - spokenNumbers[dataread];
                        spokenNumbers[dataread] = i;
                    }
                    else
                    {
                        nextnum = 0;
                        spokenNumbers.Add(dataread, i);
                    }
                }
                else
                {
                    if (!spokenNumbers.ContainsKey(nextnum))
                    {
                        spokenNumbers.Add(nextnum, i);
                        nextnum = 0;
                    }
                    else
                    {
                        int temp = i - spokenNumbers[nextnum];
                        spokenNumbers[nextnum] = i;
                        nextnum = temp;
                    }
                }
            }
            return nextnum.ToString();
        }
    }
}
