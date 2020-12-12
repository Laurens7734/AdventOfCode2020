using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day12:Day
    {
        List<string> data;
        public Day12(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            int direction = 90;
            int x = 0, y = 0;

            foreach(string s in data)
            {
                char instruction = s[0];
                int amount = int.Parse(s.Substring(1));
                switch (instruction)
                {
                    case 'N': y += amount; break;
                    case 'S': y -= amount; break;
                    case 'E': x += amount; break;
                    case 'W': x -= amount; break;
                    case 'L': direction = (direction + 360 - amount) % 360; break;
                    case 'R': direction = (direction + amount) % 360; break;
                    case 'F': Tuple<int,int> t = CalcDirection(direction, amount);  x += t.Item1; y += t.Item2; break;
                }
            }

            int answer = Math.Abs(x) + Math.Abs(y);

            return answer.ToString();
        }

        public string Answer2()
        {
            int xShip = 0, yShip = 0;
            int xWay = 10, yWay = 1;

            foreach (string s in data)
            {
                char instruction = s[0];
                int amount = int.Parse(s.Substring(1));
                switch (instruction)
                {
                    case 'N': yWay += amount; break;
                    case 'S': yWay -= amount; break;
                    case 'E': xWay += amount; break;
                    case 'W': xWay -= amount; break;
                    case 'L': Tuple<int, int> t1 = NewWaypointLocation(xWay, yWay, (360 - amount)); xWay = t1.Item1; yWay = t1.Item2; break;
                    case 'R': Tuple<int, int> t2 = NewWaypointLocation(xWay, yWay, amount); xWay = t2.Item1; yWay = t2.Item2; break;
                    case 'F': xShip += (amount * xWay); yShip += (amount * yWay); break;
                }
            }

            int answer = Math.Abs(xShip) + Math.Abs(yShip);

            return answer.ToString();
        }

        Tuple<int,int> CalcDirection(int direction, int amount)
        {
            if(direction%90 == 0)
            {
                switch (direction)
                {
                    case 0: return Tuple.Create(0, amount);
                    case 90: return Tuple.Create(amount, 0);
                    case 180: return Tuple.Create(0, -amount);
                    case 270: return Tuple.Create(-amount, 0);
                }
            }
            else if(direction%90 == 45)
            {
                int horizontalAmount = (int)Math.Sqrt(Math.Pow(amount, 2) / 2);
                switch (direction)
                {
                    case 45: return Tuple.Create(horizontalAmount, horizontalAmount);
                    case 135: return Tuple.Create(horizontalAmount, -horizontalAmount);
                    case 225: return Tuple.Create(-horizontalAmount, -horizontalAmount);
                    case 315: return Tuple.Create(-horizontalAmount, horizontalAmount);
                }
            }

            return Tuple.Create(0, 0);
        }

        Tuple<int, int> NewWaypointLocation(int x, int y, int rotation)
        {
            if(rotation == 0)
            {
                return Tuple.Create(x, y);
            }
            else if(rotation == 180)
            {
                return Tuple.Create(-x, -y);
            }
            else if(rotation == 90)
            {
                return Tuple.Create(y, -x);
            }
            else if (rotation == 270)
            {
                return Tuple.Create(-y, x);
            }
            else
            {
                return Tuple.Create(0, 0);
            }
        }
    }
}
