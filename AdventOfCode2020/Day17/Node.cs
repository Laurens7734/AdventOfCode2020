using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Node
    {
        public bool IsActive { get; private set; }
        List<Node> neighbours;
        public Node(bool startingCondition)
        {
            IsActive = startingCondition;
            neighbours = new List<Node>();
        }

        public bool NeedsUpdating()
        {
            int count = 0;

            foreach(Node n in neighbours)
            {
                if (n.IsActive) 
                    count++;
            }

            if (IsActive && (count == 2 || count == 3))
                return false;
            else if (IsActive)
                return true;
            else if (count == 3)
                return true;
            else 
                return false;
        }

        public void AddNeighbour(Node n)
        {
            neighbours.Add(n);
        }

        public void Update()
        {
            if (IsActive)
                IsActive = false;
            else
                IsActive = true;
        }
    }
}
