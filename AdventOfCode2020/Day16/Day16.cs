using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day16:Day
    {
        List<Attribute> attributes = new List<Attribute>();
        List<int> myTicket = new List<int>();
        List<List<int>> otherTickets = new List<List<int>>();
        public Day16(List<string> d)
        {
            bool fase1done = false, fase2done = false;
            foreach(string s in d)
            {
                if(!(fase1done || fase2done))
                {
                    if (s.Equals(""))
                    {
                        fase1done = true;
                        continue;
                    }
                    else
                    {
                        string[] temp = s.Split(": ");
                        string name = temp[0];
                        List<string> ranges = new List<string>(temp[1].Split(" or "));
                        Attribute a = new Attribute(name, ranges);
                        attributes.Add(a);
                    }
                }
                if(fase1done && !fase2done)
                {
                    if (s.Equals(""))
                    {
                        fase2done = true;
                        continue;
                    }
                    else if(s.StartsWith('y'))
                    {
                        continue;
                    }
                    else
                    {
                        myTicket = s.Split(',').Select(int.Parse).ToList<int>();
                    }
                }
                if (fase2done)
                {
                    if (s.StartsWith('n'))
                    {
                        continue;
                    }
                    else
                    {
                        otherTickets.Add(s.Split(',').Select(int.Parse).ToList<int>());
                    }
                }
            }
        }

        public string Answer1()
        {
            int total = 0;
            foreach(List<int> ticket in otherTickets)
            {
                foreach(int i in ticket)
                {
                    bool match = false;
                    foreach(Attribute a in attributes)
                    {
                        if (a.IsValid(i))
                            match = true;
                    }
                    if (!match)
                        total += i;
                }
            }
            return total.ToString();
        }

        public string Answer2()
        {
            List<List<int>> vailtTickets = new List<List<int>>();

            foreach(List<int> ticket in otherTickets)
            {
                if (TicketValidation(ticket))
                    vailtTickets.Add(ticket);
            }

            foreach(List<int> ticket in vailtTickets)
            {
                if (RefinePossision(ticket))
                    break;
            }

            long result = 1;
            foreach(Attribute a in attributes)
            {
                if (a.name.StartsWith("departure"))
                    result *= myTicket[a.Position()];
            }
            return result.ToString();
        }

        bool TicketValidation(List<int> ticket)
        {
            foreach (int i in ticket)
            {
                bool match = false;
                foreach (Attribute a in attributes)
                {
                    if (a.IsValid(i))
                        match = true;
                }
                if (!match)
                    return false;
            }
            return true;
        }

        bool RefinePossision(List<int> ticket)
        {
            int count = 0;
            foreach(Attribute a in attributes)
            {
                if (a.PositionFound())
                {
                    count++;
                    continue;
                }
                else
                {
                    a.UpdateLegalPositions(ticket);
                }
                    
            }
            if (count == attributes.Count)
                return true;
            return false;
        }
    }
}
