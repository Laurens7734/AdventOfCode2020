using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class FloorTile
    {
        static Dictionary<string, FloorTile> allTiles = new Dictionary<string, FloorTile>();

        readonly string location;
        public bool IsBlack { get; private set; }
        List<FloorTile> neighbours = null;
        public FloorTile(string location)
        {
            this.location = location;
            IsBlack = false;
            allTiles.Add(location, this);
        }

        public static FloorTile GenerateFloorTile(string s)
        {
            string path = ShortestPath(s);
            if (allTiles.ContainsKey(path))
                return allTiles[path];
            else
                return new FloorTile(path);
        }

        public static void UpdateAllTiles()
        {
            List<FloorTile> needsFlip = new List<FloorTile>();
            foreach(KeyValuePair<string, FloorTile> k in allTiles)
            {
                if (k.Value.NeedsUpdate())
                    needsFlip.Add(k.Value);
            }
            foreach(FloorTile f in needsFlip)
            {
                f.Flip();
            }
        }

        public static bool Exists(string path)
        {
            string s = ShortestPath(path);
            if (allTiles.ContainsKey(s))
                return true;
            else
                return false;
        }

        public static List<FloorTile> GetAllTiles()
        {
            List<FloorTile> tiles = new List<FloorTile>(allTiles.Values);
            return tiles;
        }

        public void Flip()
        {
            if (IsBlack)
                IsBlack = false;
            else
                IsBlack = true;
        }

        public bool NeedsUpdate()
        {
            if(neighbours != null)
            {
                int count = 0;
                foreach(FloorTile f in neighbours)
                {
                    if (f.IsBlack)
                        count++;
                }
                if (IsBlack && (count == 1 || count == 2))
                    return false;
                else if ((!IsBlack) && count == 2)
                    return true;
                else if (IsBlack)
                    return true;
               
            }
            return false;
        }

        public void GenerateNeighbours()
        {
            if (neighbours == null)
            {
                neighbours = new List<FloorTile>
                {
                    GenerateFloorTile(location + "e"),
                    GenerateFloorTile(location + "ne"),
                    GenerateFloorTile(location + "se"),
                    GenerateFloorTile(location + "w"),
                    GenerateFloorTile(location + "nw"),
                    GenerateFloorTile(location + "sw")
                };
            }
        }

        public bool IsMe(string s)
        {
            if (s == location)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return location;
        }

        public override bool Equals(object obj)
        {
            if(obj is FloorTile ft)
            {
                if (IsMe(ft.location))
                    return true;
            }
            return false;
        }

        public List<FloorTile> GetNeighbours()
        {
            List<FloorTile> answer = new List<FloorTile>();
            FloorTile e = new FloorTile(location + "e");
            answer.Add(e);
            FloorTile w = new FloorTile(location + "w");
            answer.Add(w);
            FloorTile ne = new FloorTile(location + "ne");
            answer.Add(ne);
            FloorTile se = new FloorTile(location + "se");
            answer.Add(se);
            FloorTile nw = new FloorTile(location + "nw");
            answer.Add(nw);
            FloorTile sw = new FloorTile(location + "sw");
            answer.Add(sw);
            return answer;
        }

        public override int GetHashCode()
        {
            return 5243;
        }

        static string ShortestPath(string path)
        {
            int w = 0, e = 0, nw = 0, ne = 0, sw = 0, se = 0;
            StringBuilder sb = new StringBuilder();
            bool doublechar = false, north = false;
            foreach (char c in path)
            {
                if (doublechar)
                {
                    if (north)
                    {
                        if (c == 'e')
                            ne++;
                        if (c == 'w')
                            nw++;
                        north = false;
                    }
                    else
                    {
                        if (c == 'e')
                            se++;
                        if (c == 'w')
                            sw++;
                    }
                    doublechar = false;
                }
                else
                {
                    if (c == 'e')
                        e++;
                    else if (c == 'w')
                        w++;
                    else if (c == 'n')
                    {
                        doublechar = true;
                        north = true;
                    }
                    else if (c == 's')
                    {
                        doublechar = true;
                        north = false;
                    }
                }
            }
            bool changed = true;
            while (changed)
            {
                changed = false;
                if (e > 0 && w > e)
                {
                    w -= e;
                    e = 0;
                    changed = true;
                }
                else if (w > 0 && w <= e)
                {
                    e -= w;
                    w = 0;
                    changed = true;
                }
                if (se > 0 && nw > se)
                {
                    nw -= se;
                    se = 0;
                    changed = true;
                }
                else if (nw > 0 && nw <= se)
                {
                    se -= nw;
                    nw = 0;
                    changed = true;
                }
                if (ne > 0 && sw > ne)
                {
                    sw -= ne;
                    ne = 0;
                    changed = true;
                }
                else if (sw > 0 && sw <= ne)
                {
                    ne -= sw;
                    sw = 0;
                    changed = true;
                }

                if (w > 0 && ne > 0)
                {
                    if (w > ne)
                    {
                        w -= ne;
                        nw += ne;
                        ne = 0;
                    }
                    else if (w <= ne)
                    {
                        ne -= w;
                        nw += w;
                        w = 0;
                    }
                    changed = true;
                }
                if (nw > 0 && e > 0)
                {
                    if (nw > e)
                    {
                        nw -= e;
                        ne += e;
                        e = 0;
                    }
                    else if (nw <= e)
                    {
                        e -= nw;
                        ne += nw;
                        nw = 0;
                    }
                    changed = true;
                }
                if (ne > 0 && se > 0)
                {
                    if (ne > se)
                    {
                        ne -= se;
                        e += se;
                        se = 0;
                    }
                    else if (ne <= se)
                    {
                        se -= ne;
                        e += ne;
                        ne = 0;
                    }
                    changed = true;
                }
                if (e > 0 && sw > 0)
                {
                    if (e > sw)
                    {
                        e -= sw;
                        se += sw;
                        sw = 0;
                    }
                    else if (e <= sw)
                    {
                        sw -= e;
                        se += e;
                        e = 0;
                    }
                    changed = true;
                }
                if (se > 0 && w > 0)
                {
                    if (se > w)
                    {
                        se -= w;
                        sw += w;
                        w = 0;
                    }
                    else if (se <= w)
                    {
                        w -= se;
                        sw += se;
                        se = 0;
                    }
                    changed = true;
                }
                if (sw > 0 && nw > 0)
                {
                    if (sw > nw)
                    {
                        sw -= nw;
                        w += nw;
                        nw = 0;
                    }
                    else if (sw <= nw)
                    {
                        nw -= sw;
                        w += sw;
                        sw = 0;
                    }
                    changed = true;
                }
            }

            for (int i = 0; i < w; i++)
                sb.Append('w');
            for (int i = 0; i < e; i++)
                sb.Append('e');
            for (int i = 0; i < nw; i++)
                sb.Append("nw");
            for (int i = 0; i < ne; i++)
                sb.Append("ne");
            for (int i = 0; i < sw; i++)
                sb.Append("sw");
            for (int i = 0; i < se; i++)
                sb.Append("se");

            return sb.ToString();
        }
    }
}
