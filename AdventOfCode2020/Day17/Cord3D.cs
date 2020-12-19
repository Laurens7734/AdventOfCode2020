using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Cord3D
    {
        public readonly int x, y, z;

        public Cord3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public List<Cord3D> GetNeighbourCords()
        {
            Cord3D[] cords = new Cord3D[27];
            for(int i = 0; i < 27; i++)
            {
                    int a, b, c;

                    a = x - ((i % 3) - 1);

                    if ((i % 9) / 3 >= 2)
                        b = y + 1;
                    else if ((i % 9) / 3 >= 1)
                        b = y;
                    else
                        b = y - 1;

                    if (i / 18 >= 1)
                        c = z + 1;
                    else if (i / 9 >= 1)
                        c = z;
                    else
                        c = z - 1;

                    cords[i] = new Cord3D(a, b, c);
            }
            List<Cord3D> answer = new List<Cord3D>(cords);
            answer.RemoveAt(13);
            return answer;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cord3D c)
            {
                if (c.x == x && c.y == y && c.z == z)
                    return true;
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return x + y + z;
        }

        public override string ToString()
        {
            return $"x:{x} y:{y} z:{z}";
        }
    }
}
