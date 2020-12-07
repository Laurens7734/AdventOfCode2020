using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Bag
    {
        public string colour;
        Dictionary<Bag, int> inside;

        public Bag(string c, Dictionary<Bag, int> i)
        {
            colour = c;
            inside = i;
        }

        public Bag(string c)
        {
            colour = c;
            inside = new Dictionary<Bag, int>();
        }

        public void AddBags(Dictionary<Bag, int> b)
        {
            inside = b;
        }

        public bool Search(Bag a)
        {
            if (inside.ContainsKey(a))
                return true;
            else
            {
                foreach(KeyValuePair<Bag, int> k in inside)
                {
                    if (k.Key.Search(a))
                        return true;
                }
            }
            return false;
        }

        public int BagCount()
        {
            int total = 1;

            foreach(KeyValuePair<Bag, int> k in inside)
            {
                total += k.Key.BagCount() * k.Value;
            }

            return total;
        }

        public override int GetHashCode()
        {
            int answer = 0;

            foreach(char c in colour)
            {
                answer += c;
            }
            return answer;
        }

        public override bool Equals(object obj)
        {
            Bag b;
            if (obj is Bag)
                b = (Bag)obj;
            else
                return false;
            if (colour.Equals(b.colour))
                return true;
            else
                return false;
        }
    }
}
