using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Grid4D
    {
        Dictionary<Cord4D, Node> nodes = new Dictionary<Cord4D, Node>();
        int minX = 0, minY = 0, minZ = 0, minW = 0, maxX, maxY, maxZ, maxW;

        public Grid4D(int x, int y, int z, int w)
        {
            maxX = x;
            maxY = y;
            maxZ = z;
            maxW = w;
        }

        public void AddNode(Cord4D c, Node n)
        {
            foreach (Cord4D co in c.GetNeighbourCords())
            {
                if (nodes.ContainsKey(co))
                {
                    nodes[co].AddNeighbour(n);
                    n.AddNeighbour(nodes[co]);
                }
            }
            nodes.Add(c, n);
        }

        public List<Node> RunCycle()
        {
            List<Node> toUpdate = new List<Node>();
            foreach(KeyValuePair<Cord4D,Node> n in nodes)
            {
                if (n.Value.NeedsUpdating()) 
                    toUpdate.Add(n.Value);
            }
            return toUpdate;
        }

        public void CheckGrid()
        {
            int countMin = 0;
            int countMax = 0;

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    for (int k = minW; k <= maxW; k++)
                    {
                        if (nodes[new Cord4D(i, j, minZ, k)].IsActive)
                            countMin++;
                        if (nodes[new Cord4D(i, j, maxZ, k)].IsActive)
                            countMax++;
                    }
                }
            }

            if (countMin > 2)
            {
                minZ -= 1;
                GenerateZ(minZ);
            }
            if (countMax > 2)
            {
                maxZ += 1;
                GenerateZ(maxZ);
            }

            countMin = 0;
            countMax = 0;

            for(int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    for (int k = minW; k <= maxW; k++)
                    {
                        if (nodes[new Cord4D(i, minY, j, k)].IsActive)
                            countMin++;
                        if (nodes[new Cord4D(i, maxY, j, k)].IsActive)
                            countMax++;
                    }
                }
            }

            if (countMin > 2)
            {
                minY -= 1;
                GenerateY(minY);
            }
            if (countMax > 2)
            {
                maxY += 1;
                GenerateY(maxY);
            }

            countMin = 0;
            countMax = 0;

            for(int i = minZ; i <= maxZ; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    for (int k = minW; k <= maxW; k++) 
                    {
                        if (nodes[new Cord4D(minX, j, i, k)].IsActive)
                             countMin++;
                        if (nodes[new Cord4D(maxX, j, i, k)].IsActive)
                            countMax++;
                    }
                }
            }

            if (countMin > 2)
            {
                minX -= 1;
                GenerateX(minX);
            }
            if (countMax > 2)
            {
                maxX += 1;
                GenerateX(maxX);
            }

            for(int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    for (int k = minY; k <= maxY; k++)
                    {
                        if (nodes[new Cord4D(i, k, j, minW)].IsActive)
                            countMin++;
                        if (nodes[new Cord4D(i, k, j, maxW)].IsActive)
                            countMax++;
                    }
                }
            }

            if (countMin > 2)
            {
                minW -= 1;
                GenerateW(minW);
            }
            if (countMax > 2)
            {
                maxW += 1;
                GenerateW(maxW);
            }
        }

        public int AmountActive()
        {
            int count = 0;
            foreach(KeyValuePair<Cord4D,Node> x in nodes)
            {
                if (x.Value.IsActive)
                {
                    count++;
                }
            }
            return count;
        }

        void GenerateZ(int z)
        {
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    for (int k = minW; k <= maxW; k++)
                    {
                        Cord4D cord = new Cord4D(i, j, z, k);
                        Node n = new Node(false);
                        this.AddNode(cord, n);
                    }
                }
            }
        }

        void GenerateY(int y)
        {
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    for (int k = minW; k <= maxW; k++)
                    {
                        Cord4D cord = new Cord4D(i, y, j, k);
                        Node n = new Node(false);
                        this.AddNode(cord, n);
                    }
                }
            }
        }

        void GenerateX(int x)
        {
            for (int i = minZ; i <= maxZ; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    for (int k = minW; k <= maxW; k++)
                    {
                        Cord4D cord = new Cord4D(x, j, i, k);
                        Node n = new Node(false);
                        this.AddNode(cord, n);
                    }
                }
            }
        }

        void GenerateW(int w)
        {
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    for (int k = minY; k <= maxY; k++)
                    {
                        Cord4D cord = new Cord4D(i, k, j, w);
                        Node n = new Node(false);
                        this.AddNode(cord, n);
                    }
                }
            }
        }
    }
}
