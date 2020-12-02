using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class DataToList
    {
        string datafile;
        public DataToList(string filename)
        {
            datafile = filename;
        }

        public List<int> GetIntList()
        {
            List<int> result = new List<int>();
            System.IO.StreamReader file = new System.IO.StreamReader(datafile);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(int.Parse(line));
            }
            return result;
        }

        public List<string> GetStringList()
        {
            List<string> result = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(datafile);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }
    }
}
