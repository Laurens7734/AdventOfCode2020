using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    class Day5 : Day
    {
        List<string> data;

        public Day5(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            int highestID = 0;
            foreach(string s in data)
            {
                int row = ReadRow(s.Substring(0, 7));
                int column = ReadColumn(s.Substring(7));
                int id = 8 * row + column;

                if (id > highestID)
                    highestID = id;
            }
            return highestID.ToString();
        }

        public string Answer2()
        {
            List<int> allIDs = new List<int>(Enumerable.Range(0,1024));
            int smalestID = 1000;

            foreach (string s in data)
            {
                int row = ReadRow(s.Substring(0, 7));
                int column = ReadColumn(s.Substring(7));
                int id = 8 * row + column;

                if (id < smalestID)
                    smalestID = id;

                int location = allIDs.BinarySearch(id);

                if (location >= 0)
                    allIDs.RemoveAt(location);
            }

            for (int index = 0; index < allIDs.Count; index++)
            {
                int i = allIDs[index];
                if (i > smalestID)
                {
                    if(!(i+1 == allIDs[index + 1]) || !(i - 1 == allIDs[index - 1]))
                    {
                        return i.ToString();
                    }
                }
            }
            return "error no id found";
        }

        int ReadRow(string s)
        {
            int response = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i] == 'B')
                {
                    response += (64/(int)Math.Pow(2 , i));
                }
            }

            return response;
        }

        int ReadColumn(string s)
        {
            int response = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'R')
                {
                    response += (4 / (int)Math.Pow(2, i));
                }
            }

            return response;
        }
    }
}
