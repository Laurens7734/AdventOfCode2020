using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day17:Day
    {
        Grid3D grid1;
        Grid4D grid2;
        public Day17(List<string> d)
        {
            grid1 = new Grid3D(d[0].Length-1, d.Count-1, 0);
            grid2 = new Grid4D(d[0].Length - 1, d.Count - 1, 0, 0);
            int y = 0;
            foreach(string s in d)
            {
                int x = 0;
                foreach(char c in s)
                {
                    Cord3D cord = new Cord3D(x, y, 0);
                    Cord4D cord2 = new Cord4D(x, y, 0, 0);
                    Node n;
                    Node m;
                    if (c == '#') 
                    {
                        n = new Node(true);
                        m = new Node(true);
                    }
                    else 
                    {
                        n = new Node(false);
                        m = new Node(false);
                    }

                    grid1.AddNode(cord, n);
                    grid2.AddNode(cord2, m);
                    x++;
                }
                y++;
            }
        }

        public string Answer1()
        {
            for(int i = 0; i < 6; i++)
            {
                grid1.CheckGrid();
                grid1.RunCycle().ForEach(x => x.Update());
            }
            return grid1.AmountActive().ToString();
        }

        public string Answer2()
        {
            for (int i = 0; i < 6; i++)
            {
                grid2.CheckGrid();
                grid2.RunCycle().ForEach(x => x.Update());
            }
            return grid2.AmountActive().ToString();
        }
    }
}
