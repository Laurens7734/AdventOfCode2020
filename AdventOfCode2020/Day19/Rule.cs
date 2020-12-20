using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    abstract class Rule
    {
        public static List<Rule> allRules = new List<Rule>();
        int id;

        public Rule(int id)
        {
            this.id = id;
            allRules.Add(this);
        }

        public abstract string[] Evaluate(string s);
        public abstract void UpdateRule(string s);
        public virtual void FindRules()
        {
        }

        public bool isRule(int id)
        {
            if (id == this.id)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if(obj is Rule r)
                if(r.id == id)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}
