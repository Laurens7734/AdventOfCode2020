using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Day2 : Day
    {
        List<string> database;

        public Day2(List<string> s)
        {
            database = s;
        }

        public string Answer1()
        {
            int answer = 0;
            foreach(string s in database)
            {
                char needed = s[s.IndexOf(':') - 1];
                string password = s.Substring(s.IndexOf(':') + 2);
                int min = int.Parse(s.Substring(0, s.IndexOf('-')));
                int max = int.Parse(s.Substring(s.IndexOf('-')+1, s.IndexOf(' ')- (s.IndexOf('-')+1)));
                int amount = 0;
                
                foreach(char c in password)
                {
                    if(c == needed)
                    {
                        amount++;
                    }
                }

                if(amount <= max && amount >= min)
                {
                    answer++;
                }
            }
            return answer.ToString();
        }

        public string Answer2()
        {
            int answer = 0;
            foreach (string s in database)
            {
                char needed = s[s.IndexOf(':') - 1];
                string password = s.Substring(s.IndexOf(':') + 2);
                int position1 = int.Parse(s.Substring(0, s.IndexOf('-')));
                int position2 = int.Parse(s.Substring(s.IndexOf('-') + 1, s.IndexOf(' ') - (s.IndexOf('-') + 1)));
                int matches = 0;
                
                if(position1 <= password.Length)
                {
                    if(password[position1 - 1] == needed)
                        matches++;
                }
                if (position2 <= password.Length)
                {
                    if (password[position2 - 1] == needed)
                        matches++;
                }

                if (matches == 1)
                    answer++;
            }
            return answer.ToString();
        }
    }
}
