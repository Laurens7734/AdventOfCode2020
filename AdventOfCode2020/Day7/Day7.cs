using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day7 : Day
    {
        List<Bag> bags;

        public Day7(List<string> d)
        {
            List<Bag> knownBags = new List<Bag>();
            foreach (string s in d)
            {
                List<string> splitter = new List<string>(s.Split(" contain"));
                string bag = splitter[0].Substring(0, splitter[0].Length - 5);

                if (splitter[1].Equals(" no other bags."))
                {
                    continue;
                }

                splitter = new List<string>(splitter[1].Split(','));

                Dictionary<Bag, int> inside = new Dictionary<Bag, int>();

                foreach (string st in splitter)
                {
                    int amount = int.Parse(st.Substring(0, 3));
                    string name = st.Substring(3, st.LastIndexOf(' ') - 3);
                    Bag a = knownBags.Find(b => b.colour.Equals(name));
                    if (a == null)
                    {
                        a = new Bag(name);
                        knownBags.Add(a);
                    }

                    inside.Add(a, amount);
                }

                Bag b = knownBags.Find(a => a.colour.Equals(bag));
                if (b == null)
                {
                    b = new Bag(bag, inside);
                    knownBags.Add(b);
                }
                else
                {
                    b.AddBags(inside);
                }
            }
            bags = knownBags;
        }

        public string Answer1()
        {
            Bag target = new Bag("shiny gold");
            int count = 0;
            foreach(Bag b in bags)
            {
                if (b.Search(target))
                    count++;
            }
            return count.ToString();
        }

        public string Answer2()
        {
            Bag mine = bags.Find(b => b.colour.Equals("shiny gold"));
            return (mine.BagCount()-1).ToString();
        }
    }
}