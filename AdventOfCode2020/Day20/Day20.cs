using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day20 : Day
    {
        Picture p;
        string[] picture;
        public Day20(List<string> d)
        {
            p = new Picture();
            Tile t = null;
            foreach(string s in d)
            {
                if (s.StartsWith("Tile"))
                {
                    int id = int.Parse(s.Substring(s.IndexOf(' '), s.IndexOf(':')-(s.IndexOf(' '))));
                    t = new Tile(id);
                }
                else if(s == "")
                {
                    t.GenerateBorders();
                    p.AddTile(t);
                }
                else
                {
                    t.AddLine(s);
                }
            }
            t.GenerateBorders();
            p.AddTile(t);

            p.Assemble();
        }

        public string Answer1()
        {
            long number = 1;
            foreach(Tile t in p.GetCornerTiles())
            {
                number *= t.Id;
            }
            return number.ToString();
        }
        public string Answer2()
        {
            long count = 0;
            picture = p.GetFullImage().Split("\n");
            MarkMonsters();
            foreach(string s in picture)
            {
                foreach(char c in s)
                {
                    if (c == '#')
                        count++;
                }
            }
            return count.ToString(); ;
        }

        void MarkMonsters()
        {
            for (int y = 0; y < picture.Length; y++)
            {
                for (int x = 0; x < picture[0].Length; x++)
                {
                    if (IsHead(x, y))
                    {
                        IsMonster(x, y);
                    }
                }
            }
        }

        bool IsHead(int x, int y)
        {
            List<char> symbols = new List<char>() {'#', 'O' };
            if (x == 0 || y == 0 || x == picture[0].Length-1 || y == picture.Length-1)
                return false;

            if (symbols.Contains(picture[y][x]))
            {
                char[] neigbours = new char[] { picture[y][x - 1], picture[y][x + 1], picture[y - 1][x], picture[y + 1][x] };
                int i = 0;
                foreach(char c in neigbours)
                {
                    if (symbols.Contains(c))
                        i++;
                }
                if(i == 3)
                {
                    if(!symbols.Contains(picture[y + 1][x]) && ((x > 1 && symbols.Contains(picture[y+1][x-2])) || (x < picture[0].Length - 2 && symbols.Contains(picture[y + 1][x + 2]))))
                    {
                        return true;
                    }
                    if (!symbols.Contains(picture[y - 1][x]) && ((x > 1 && symbols.Contains(picture[y - 1][x - 2])) || (x < picture[0].Length - 2 && symbols.Contains(picture[y - 1][x + 2]))))
                    {
                        return true;
                    }
                    if (!symbols.Contains(picture[y][x-1]) && ((y > 1 && symbols.Contains(picture[y - 2][x -1])) || (y < picture.Length - 2 && symbols.Contains(picture[y + 2][x -1]))))
                    {
                        return true;
                    }
                    if (!symbols.Contains(picture[y][x + 1]) && ((y > 1 && symbols.Contains(picture[y - 2][x + 1])) || (y < picture.Length - 2 && symbols.Contains(picture[y + 2][x + 1]))))
                    {
                        return true;
                    }
                }
                else if(i == 4)
                {
                    List<char> allPos = new List<char>();
                    if(x > 1)
                    {
                        allPos.Add(picture[y + 1][x - 2]);
                        allPos.Add(picture[y - 1][x - 2]);
                    }
                    if(x < picture[0].Length-2)
                    {
                        allPos.Add(picture[y + 1][x + 2]);
                        allPos.Add(picture[y - 1][x + 2]);
                    }
                    if (y > 1)
                    {
                        allPos.Add(picture[y - 2][x + 1]);
                        allPos.Add(picture[y - 2][x - 1]);
                    }
                    if (y < picture.Length-2)
                    {
                        allPos.Add(picture[y + 2][x + 1]);
                        allPos.Add(picture[y + 2][x - 1]);
                    }

                    foreach (char c in allPos)
                    {
                        if (symbols.Contains(c))
                            return true;
                    }

                }
            }
            return false;
        }

        void IsMonster(int x, int y)
        {
            if(x > 17)
            {
                List<Tuple<int, int>> cords1 = new List<Tuple<int, int>>() {Tuple.Create(0,-1), Tuple.Create(1, 0), Tuple.Create(-1, 0), Tuple.Create(0, 0), Tuple.Create(-2, 1),
                 Tuple.Create(-5, 1), Tuple.Create(-6, 0), Tuple.Create(-7, 0), Tuple.Create(-8, 1),
                 Tuple.Create(-11, 1), Tuple.Create(-12, 0), Tuple.Create(-13, 0), Tuple.Create(-14, 1),
                 Tuple.Create(-17, 1), Tuple.Create(-18, 0)};
                if (ContainsMonster(x, y, cords1))
                    Replace(x, y, cords1);
                List<Tuple<int, int>> cords2 = new List<Tuple<int, int>>() {Tuple.Create(0,1), Tuple.Create(1, 0), Tuple.Create(-1, 0), Tuple.Create(0, 0), Tuple.Create(-2, -1),
                 Tuple.Create(-5, -1), Tuple.Create(-6, 0), Tuple.Create(-7, 0), Tuple.Create(-8, -1),
                 Tuple.Create(-11, -1), Tuple.Create(-12, 0), Tuple.Create(-13, 0), Tuple.Create(-14, -1),
                 Tuple.Create(-17, -1), Tuple.Create(-18, 0)};
                if (ContainsMonster(x, y, cords2))
                    Replace(x, y, cords2);
            }
            if (x < picture[0].Length-18)
            {
                List<Tuple<int, int>> cords1 = new List<Tuple<int, int>>() {Tuple.Create(0,-1), Tuple.Create(-1, 0), Tuple.Create(1, 0), Tuple.Create(0, 0), Tuple.Create(2, 1),
                 Tuple.Create(5, 1), Tuple.Create(6, 0), Tuple.Create(7, 0), Tuple.Create(8, 1),
                 Tuple.Create(11, 1), Tuple.Create(12, 0), Tuple.Create(13, 0), Tuple.Create(14, 1),
                 Tuple.Create(17, 1), Tuple.Create(18, 0)};
                if (ContainsMonster(x, y, cords1))
                    Replace(x, y, cords1);
                List<Tuple<int, int>> cords2 = new List<Tuple<int, int>>() {Tuple.Create(0,1), Tuple.Create(-1, 0), Tuple.Create(1, 0), Tuple.Create(0, 0), Tuple.Create(2, -1),
                 Tuple.Create(5, -1), Tuple.Create(6, 0), Tuple.Create(7, 0), Tuple.Create(8, -1),
                 Tuple.Create(11, -1), Tuple.Create(12, 0), Tuple.Create(13, 0), Tuple.Create(14, -1),
                 Tuple.Create(17, -1), Tuple.Create(18, 0)};
                if (ContainsMonster(x, y, cords2))
                    Replace(x, y, cords2);
            }
            if(y > 17)
            {
                List<Tuple<int, int>> cords1 = new List<Tuple<int, int>>() {Tuple.Create(-1,0), Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(0, 0), Tuple.Create(1, -2),
                 Tuple.Create(1, -5), Tuple.Create(0, -6), Tuple.Create(0, -7), Tuple.Create(1, -8),
                 Tuple.Create(1, -11), Tuple.Create(0, -12), Tuple.Create(0, -13), Tuple.Create(1, -14),
                 Tuple.Create(1, -17), Tuple.Create(0, -18)};
                if (ContainsMonster(x, y, cords1))
                    Replace(x, y, cords1);
                List<Tuple<int, int>> cords2 = new List<Tuple<int, int>>() {Tuple.Create(1,0), Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(0, 0), Tuple.Create(-1, -2),
                 Tuple.Create(-1, -5), Tuple.Create(0, -6), Tuple.Create(0, -7), Tuple.Create(-1, -8),
                 Tuple.Create(-1, -11), Tuple.Create(0, -12), Tuple.Create(0, -13), Tuple.Create(-1, -14),
                 Tuple.Create(-1, -17), Tuple.Create(0, -18)};
                if (ContainsMonster(x, y, cords2))
                    Replace(x, y, cords2);
            }
            if (y < picture.Length - 18)
            {
                List<Tuple<int, int>> cords1 = new List<Tuple<int, int>>() {Tuple.Create(-1,0), Tuple.Create(0, -1), Tuple.Create(0, 1), Tuple.Create(0, 0), Tuple.Create(1, 2),
                 Tuple.Create(1, 5), Tuple.Create(0, 6), Tuple.Create(0, 7), Tuple.Create(1, 8),
                 Tuple.Create(1, 11), Tuple.Create(0, 12), Tuple.Create(0, 13), Tuple.Create(1, 14),
                 Tuple.Create(1, 17), Tuple.Create(0, 18)};
                if (ContainsMonster(x, y, cords1))
                    Replace(x, y, cords1);
                List<Tuple<int, int>> cords2 = new List<Tuple<int, int>>() {Tuple.Create(1,0), Tuple.Create(0, -1), Tuple.Create(0, 1), Tuple.Create(0, 0), Tuple.Create(-1, 2),
                 Tuple.Create(-1, 5), Tuple.Create(0, 6), Tuple.Create(0, 7), Tuple.Create(-1, 8),
                 Tuple.Create(-1, 11), Tuple.Create(0, 12), Tuple.Create(0, 13), Tuple.Create(-1, 14),
                 Tuple.Create(-1, 17), Tuple.Create(0, 18)};
                if (ContainsMonster(x, y, cords2))
                    Replace(x, y, cords2);
            }
        }

        void Replace(int x, int y, List<Tuple<int,int>> cords)
        {
            foreach(Tuple<int,int> t in cords)
            {
                char[] chars = picture[y + t.Item2].ToCharArray();
                chars[x + t.Item1] = 'O';
                picture[y + t.Item2] = new string(chars);
            }
        }

        bool ContainsMonster(int x, int y, List<Tuple<int, int>> cords)
        {
            foreach(Tuple<int, int> t in cords)
            {
                if (picture[y + t.Item2][x+t.Item1] != '#' && picture[y + t.Item2][x + t.Item1] != 'O')
                    return false;
            }
            return true;
        }
    }
}
