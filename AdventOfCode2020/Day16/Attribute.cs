using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    class Attribute
    {
        static List<Attribute> allAtributes = new List<Attribute>();

        public string name;
        List<Tuple<int, int>> ranges;
        List<int> legalPositions;
        public Attribute(string n, List<string> r)
        {
            name = n;
            ranges = new List<Tuple<int, int>>();
            foreach(string s in r)
            {
                string[] split = s.Split('-');
                int low = int.Parse(split[0]);
                int high = int.Parse(split[1]);
                ranges.Add(Tuple.Create(low, high));
            }
            allAtributes.Add(this);
        }

        public bool IsValid(int i)
        {
            foreach(Tuple<int,int> t in ranges)
            {
                if (i >= t.Item1 && i <= t.Item2)
                    return true;
            }
            return false;
        }

        public bool PositionFound()
        {
            if(legalPositions != null)
                if (legalPositions.Count == 1)
                    return true;
            return false;
        }

        public int Position()
        {
            if (this.PositionFound())
            {
                return legalPositions[0];
            }

            return -1;
        }

        public void UpdateLegalPositions(List<int> ticket)
        {
            if(legalPositions == null)
                legalPositions = new List<int>(Enumerable.Range(0, ticket.Count));
 
            List<int> toRemove = new List<int>();
            foreach(int i in legalPositions)
                if (!IsValid(ticket[i]))
                    toRemove.Add(i);

            foreach (int j in toRemove)
                legalPositions.Remove(j);

            if(legalPositions.Count == 1)
            {
                int myposition = legalPositions[0];
                allAtributes.ForEach(a => a.RemovePosition(myposition));
                legalPositions.Add(myposition);
            }
                
        }

        public void RemovePosition(int i)
        {
            if (legalPositions.Contains(i)) { 
                legalPositions.Remove(i);

                if (legalPositions.Count == 1)
                {
                    int myposition = legalPositions[0];
                    allAtributes.ForEach(a => a.RemovePosition(myposition));
                    legalPositions.Add(myposition);
                }
            }
        }
    }
}
