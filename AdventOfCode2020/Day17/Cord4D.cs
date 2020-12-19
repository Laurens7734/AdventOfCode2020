using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Cord4D
    {
        readonly int x, y, z, w;

        public Cord4D(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public List<Cord4D> GetNeighbourCords()
        {
            List<Cord3D> temp = new Cord3D(x, y, z).GetNeighbourCords();
            List<Cord4D> answer = new List<Cord4D>();
            foreach(Cord3D c in temp)
            {
                answer.Add(new Cord4D(c.x, c.y, c.z, w - 1));
                answer.Add(new Cord4D(c.x, c.y, c.z, w));
                answer.Add(new Cord4D(c.x, c.y, c.z, w + 1));
            }
            answer.Add(new Cord4D(x, y, z, w - 1));
            answer.Add(new Cord4D(x, y, z, w + 1));
            return answer;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cord4D c)
            {
                if (c.x == x && c.y == y && c.z == z && c.w == w)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return x + y + z + w;
        }

        public override string ToString()
        {
            return $"x:{x} y:{y} z:{z} w:{w}";
        }
    }
}
