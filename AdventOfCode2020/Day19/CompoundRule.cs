using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class CompoundRule: Rule
    {
        List<List<Rule>> posibilities = new List<List<Rule>>();
        string rulesWaitlist;

        public CompoundRule(int id, string rules) : base(id)
        {
            rulesWaitlist = rules;
        }

        public override void FindRules()
        {
            string[] sets = rulesWaitlist.Split('|');
            foreach (string s in sets)
            {
                string[] nums = s.Split(' ');
                List<Rule> pos = new List<Rule>();
                foreach (string a in nums)
                {
                    if(a != "")
                    {
                        Rule r = allRules.Find(x => x.isRule(int.Parse(a)));
                        pos.Add(r);
                    }
                }
                posibilities.Add(pos);
            }
        }

        public override void UpdateRule(string s)
        {
            posibilities = new List<List<Rule>>();
            rulesWaitlist = s;
            this.FindRules();
        }

        public override string[] Evaluate(string s)
        {
            List<string> answer = new List<string>();
            foreach(List<Rule> lr in posibilities)
            {
                string[] remainder = new string[] { s };

                foreach(Rule r in lr)
                {
                    List<string> result = new List<string>();
                    foreach(string st in remainder)
                    {
                        result.AddRange(r.Evaluate(st));
                    }
                    result.RemoveAll(x => x == null);
                    if (result.Count > 0)
                        remainder = result.ToArray();
                    else
                        remainder = new string[0];
                }
                answer.AddRange(remainder);
            }
            answer.RemoveAll(x => x == null);
            if (answer.Count > 0)
                return answer.ToArray();
            else
                return new string[] { null };
        }
    }
}
