using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Seat
    {
        int x, y;
        List<Seat> neighbours = new List<Seat>();
        public bool Taken { get; private set; }
        public bool Taken2 { get; private set; }

        public Seat(int row, int collumn)
        {
            x = row;
            y = collumn;
            Taken = false;
        }

        public void AddNeighbour(Seat s)
        {
            neighbours.Add(s);
        }

        public void AddNeighbours(List<Seat> n)
        {
            neighbours.AddRange(n);
            foreach(Seat s in n)
            {
                s.AddNeighbour(this);
            }
        }

        public bool FindSeat(int a, int b)
        {
            if (a == x && b == y)
                return true;
            return false;
        }

        public void SaveState()
        {
            Taken2 = Taken;
        }

        public bool Update()
        {
            int count = 0;
            foreach(Seat s in neighbours)
            {
                if (s.Taken2)
                    count++;
            }
            if(Taken && count >= 5)
            {
                Taken = false;
                return true;
            }
            if(!Taken && count == 0)
            {
                Taken = true;
                return true;
            }
            return false;

        }
    }
}
