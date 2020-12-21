using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Picture
    {
        List<Tile> unUsed = new List<Tile>();
        Dictionary<string, Tile> bordersLeft = new Dictionary<string, Tile>();
        Tile[,] grid;

        public Picture()
        {
        }

        public List<Tile> GetCornerTiles()
        {
            List<Tile> answer = new List<Tile>();
            answer.Add(grid[0, 0]);
            int x = grid.GetLength(0);
            int y = grid.GetLength(1);
            answer.Add(grid[0, y - 1]);
            answer.Add(grid[x - 1, 0]);
            answer.Add(grid[x - 1, y - 1]);
            return answer;
        }

        public string GetFullImage()
        {
            StringBuilder finalAssembly = new StringBuilder();
            for(int i = 0; i < grid.GetLength(0); i++)
            {
                List<List<string>> tileRow = new List<List<string>>();
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    tileRow.Add(grid[j, i].GetBorderlessImage());
                }
                for(int j = 0; j < tileRow[0].Count; j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        finalAssembly.Append(tileRow[k][j]);
                    }
                    finalAssembly.Append("\n");
                }
            }
            string s = finalAssembly.ToString().TrimEnd('\n');
            return s;
        }

        public void AddTile(Tile t)
        {
            unUsed.Add(t);
            foreach(string s in t.GetBorders())
            {
                if (bordersLeft.ContainsKey(s))
                    bordersLeft.Remove(s);
                else
                    bordersLeft.Add(s, t);
            }
        }

        public void Assemble()
        {
            Tile TopLeft = FindCornerTile();
            List<Tile> firstRow = new List<Tile>();
            unUsed.Remove(TopLeft);
            firstRow.Add(TopLeft);
            string borderleft = TopLeft.GetBorders()[3];
            for(int i = 0; i < unUsed.Count; i++)
            {
                Tile t = unUsed[i];
                if (t.IsNextPiece(borderleft, 3))
                {
                    borderleft = t.GetBorders()[3];
                    firstRow.Add(t);
                    unUsed.RemoveAt(i);
                    i = 0;
                }
                    
            }
            int x = firstRow.Count;
            int y = 1 +(unUsed.Count / x);
            grid = new Tile[x, y];
            for(int i = 0; i < x; i++)
            {
                grid[i, 0] = firstRow[i];
            }
            string borderdown = TopLeft.GetBorders()[1];
            for (int j = 1; j < y; j++)
            {
                foreach(Tile t in unUsed)
                {
                    if (t.IsNextPiece(borderdown, 1))
                    {
                        borderleft = t.GetBorders()[3];
                        borderdown = t.GetBorders()[1];
                        unUsed.Remove(t);
                        grid[0, j] = t;
                        break;
                    }
                }
                
                for (int i = 1; i < x; i++)
                {
                    foreach (Tile t in unUsed)
                    {
                        if (t.IsNextPiece(borderleft, 3))
                        {
                            borderleft = t.GetBorders()[3];
                            grid[i, j] = t;
                            unUsed.Remove(t);
                            break;
                        }
                    }
                }
            }
        }

        

        Tile FindCornerTile()
        {
            Tile t = null;
            Dictionary<Tile, int> count = new Dictionary<Tile, int>();
            foreach(KeyValuePair<string,Tile> k in bordersLeft)
            {
                if (count.ContainsKey(k.Value))
                {
                    count[k.Value] += 1;
                    if(count[k.Value] == 4)
                    {
                        t = k.Value;
                        break;
                    }

                }
                else
                    count.Add(k.Value, 1);
            }
            List<int> borders = new List<int>();
            for(int i = 0; i < 8; i++)
            {
                if (bordersLeft.ContainsKey(t.GetBorders()[i]))
                    borders.Add(i);
            }
            if (borders.Contains(0))
            {
                if (borders.Contains(4))
                {
                    t.Lock();
                }
                else if (borders.Contains(6))
                {
                    t.RotateClockwise(270);
                    t.Lock();
                }
            }
            else if (borders.Contains(2))
            {
                if (borders.Contains(4))
                {
                    t.RotateClockwise(90);
                    t.Lock();
                }
                else if (borders.Contains(6))
                {
                    t.RotateClockwise(180);
                    t.Lock();
                }
            }
            return t;
        }

    }
}
