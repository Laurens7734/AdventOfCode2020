using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Grid3D
    {
        Dictionary<Cord3D, Node> nodes = new Dictionary<Cord3D, Node>();
        int minX= 0, minY=0, minZ=0, maxX, maxY, maxZ;

        public Grid3D(int x, int y, int z)
        {
            maxX = x;
            maxY = y;
            maxZ = z;
        }

        public void AddNode(Cord3D c, Node n)
        {
            foreach(Cord3D co in c.GetNeighbourCords())
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
            foreach(KeyValuePair<Cord3D,Node> n in nodes)
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

            for(int i = minX; i <= maxX; i++)
            { 
                for(int j = minY; j <= maxY; j++)
                {
                    if (nodes[new Cord3D(i, j, minZ)].IsActive)
                        countMin++;
                    if (nodes[new Cord3D(i, j, maxZ)].IsActive)
                        countMax++;
                }
            }

            if(countMin > 2)
            {
                minZ -= 1;
                GenerateZ(minZ);
            }
            if(countMax > 2)
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
                    if (nodes[new Cord3D(i, minY, j)].IsActive)
                        countMin++;
                    if (nodes[new Cord3D(i, maxY, j)].IsActive)
                        countMax++;
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
                    if (nodes[new Cord3D(minX, j, i)].IsActive)
                        countMin++;
                    if (nodes[new Cord3D(maxX, j, i)].IsActive)
                        countMax++;
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
        }

        public int AmountActive()
        {
            int count = 0;
            foreach(KeyValuePair<Cord3D, Node> x in nodes)
            {
                if (x.Value.IsActive) 
                    count++;
            }
            return count;
        }

        void GenerateZ(int z)
        {
            for(int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    Cord3D cord = new Cord3D(i, j, z);
                    Node n = new Node(false);
                    this.AddNode(cord, n);
                }
            }
        }

        void GenerateY(int y)
        {
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    Cord3D cord = new Cord3D(i, y, j);
                    Node n = new Node(false);
                    this.AddNode(cord, n);
                }
            }
        }

        void GenerateX(int x)
        {
            for (int i = minZ; i <= maxZ; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    Cord3D cord = new Cord3D(x, j, i);
                    Node n = new Node(false);
                    this.AddNode(cord, n);
                }
            }
        }

    }
}
