using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day06 : Day
    {
        List<string> data;

        public Day06(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            Dictionary<char, int> questions = new Dictionary<char, int>();
            int total = 0;

            foreach(string s in data)
            {
                if (s.Equals(""))
                {
                    total += questions.Count;
                    questions = new Dictionary<char, int>();
                }
                else
                {
                    foreach(char c in s)
                    {
                        if (questions.ContainsKey(c))
                            questions[c] += 1;
                        else
                            questions.Add(c, 1);
                    }
                }
            }
            total += questions.Count;

            return total.ToString();
        }

        public string Answer2()
        {
            Dictionary<char, int> questions = new Dictionary<char, int>();
            int people = 0;
            int total = 0;

            foreach (string s in data)
            {
                if (s.Equals(""))
                {
                    foreach(KeyValuePair<char, int> k in questions)
                    {
                        if (k.Value == people)
                            total++;
                    }
                    people = 0;
                    questions = new Dictionary<char, int>();
                }
                else
                {
                    people++;
                    foreach (char c in s)
                    {
                        if (questions.ContainsKey(c))
                            questions[c] += 1;
                        else
                            questions.Add(c, 1);
                    }
                }
            }
            foreach (KeyValuePair<char, int> k in questions)
            {
                if (k.Value == people)
                    total++;
            }

            return total.ToString();
        }
    }
}
