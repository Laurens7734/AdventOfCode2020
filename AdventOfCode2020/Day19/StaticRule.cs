using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class StaticRule : Rule
    {
        string toMatch;
        public StaticRule(int id, string toMatch) : base(id)
        {
            this.toMatch = toMatch;
        }

        public override string[] Evaluate(string s)
        {
            string[] answer = new string[1];
            if (s == null)
                answer[0] = s;
            else if (s.StartsWith(toMatch))
            {
                answer[0] = s.Substring(toMatch.Length);
            }
            else
            {
                answer[0] = null;
            }
            return answer;
        }

        public override void UpdateRule(string s)
        {
            toMatch = s;
        }
    }
}
