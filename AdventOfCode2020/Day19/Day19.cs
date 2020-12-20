using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day19 : Day
    {
        Rule startingPoint;
        List<string> messages;
        public Day19(List<string> d)
        {
            bool messageStarted = false;
            messages = new List<string>();

            foreach(string s in d)
            {
                if (!messageStarted)
                {
                    if(s == "")
                    {
                        messageStarted = true;
                        Rule.allRules.ForEach(x => x.FindRules());
                        continue;
                    }

                    Rule r;

                    string[] parts = s.Split(": ");
                    int id = int.Parse(parts[0]);
                    if (parts[1].Contains('"')) 
                    {
                        string toMatch = parts[1].Substring(parts[1].IndexOf('"') + 1, parts[1].LastIndexOf('"') - (1 + parts[1].IndexOf('"')));
                        r = new StaticRule(id, toMatch);
                    }
                    else
                    {
                        r = new CompoundRule(id, parts[1]);
                    }
                    if (id == 0)
                        startingPoint = r;
                }
                else
                {
                    messages.Add(s);
                }
            }
        }

        public string Answer1()
        {
            long succes = 0;
            foreach(string s in messages)
            {
                List<string> tests = new List<string>(startingPoint.Evaluate(s));
                if (tests.Contains(""))
                    succes++;
            }
            return succes.ToString();
        }

        public string Answer2()
        {
            foreach(Rule r in Rule.allRules)
            {
                if (r.isRule(8))
                {
                    r.UpdateRule("42 | 42 8");
                }
                else if (r.isRule(11))
                {
                    r.UpdateRule("42 31 | 42 11 31");
                }
            }

            long succes = 0;
            foreach (string s in messages)
            {
                List<string> tests = new List<string>(startingPoint.Evaluate(s));
                if (tests.Contains(""))
                    succes++;
            }
            return succes.ToString();
        }
    }
}
