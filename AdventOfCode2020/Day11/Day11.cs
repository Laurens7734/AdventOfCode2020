using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day11:Day
    {
        List<string> data;
        public Day11(List<string> d)
        {
            data =d;
        }

        public string Answer1()
        {
            bool hasChanged = true;
            List<string> current = data;
            while (hasChanged)
            {
                current = NextStep(current, out hasChanged);
            }
            int count = 0;
            foreach(string s in current)
            {
                foreach(char c in s)
                {
                    if (c == '#')
                        count++;
                }
            }
            return count.ToString();
        }

        public string Answer2()
        {
            List<Seat> seats = GetSeatList(data);
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach(Seat s in seats)
                {
                    s.SaveState();
                }
                foreach (Seat s in seats)
                {
                    if (s.Update())
                    {
                        changed = true;
                    }
                }
            }
            int people = 0;
            foreach(Seat s in seats)
            {
                if (s.Taken)
                    people++;
            }
            return people.ToString();
        }

        List<string> NextStep(List<string> d, out bool changed)
        {
            List<string> ret = new List<string>();
            changed = false;

            for (int i = 0; i < d.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < d[i].Length; j++)
                {
                    int people = 0;
                    if (d[i][j] == '.')
                    {
                        sb.Append('.');
                    }
                    else
                    {
                        if (i > 0)
                        {
                            if (d[i - 1][j] == '#')
                                people++;
                            if(j > 0)
                            {
                                if (d[i - 1][j-1] == '#')
                                    people++;
                            }
                            if(j < d[i].Length - 1)
                            {
                                if (d[i - 1][j+1] == '#')
                                    people++;
                            }
                        }
                        if (i < d.Count-1)
                        {
                            if (d[i + 1][j] == '#')
                                people++;
                            if (j > 0)
                            {
                                if (d[i + 1][j - 1] == '#')
                                    people++;
                            }
                            if (j < d[i].Length - 1)
                            {
                                if (d[i + 1][j + 1] == '#')
                                    people++;
                            }
                        }
                        if (j > 0)
                        {
                            if (d[i][j-1] == '#')
                                people++;
                        }
                        if (j < d[i].Length - 1)
                        {
                            if (d[i][j + 1] == '#')
                                people++;
                        }

                        if (d[i][j] == 'L' )
                        {
                            if (people == 0)
                            {
                                sb.Append('#');
                                changed = true;
                            }
                            else
                            {
                                sb.Append('L');
                            }
                        }
                        if(d[i][j] == '#')
                        {
                            if (people >= 4)
                            {
                                sb.Append('L');
                                changed = true;
                            }
                            else
                            {
                                sb.Append('#');
                            }
                        }
                           
                    }
                }
                ret.Add(sb.ToString());
            }
            return ret;
        }

        List<Seat> GetSeatList(List<string> d)
        {
            List<Seat> seats = new List<Seat>();

            for(int i = 0; i < d.Count; i++)
            {
                for(int j = 0; j < d[0].Length; j++)
                {
                    if(d[i][j] == 'L')
                    {
                        Seat seat = new Seat(i, j);
                        seat.AddNeighbours(GetVisibleSeats(d, seats, i, j));
                        seats.Add(seat);
                    }
                }
            }
            return seats;
        }

        List<Seat> GetVisibleSeats(List<string> d, List<Seat> seats, int i, int j)
        {
            List<Seat> foundSeats = new List<Seat>();
            bool left = false, up = false, upLeft = false, upRight = false;
            int done = 0;
            int offset = 1;
            while (done < 4)
            {
                if(!left)
                {
                    if(i-offset >= 0)
                    {
                        if(d[i-offset][j] == 'L')
                        {
                            left = true;
                            foundSeats.Add(seats.Find(x => x.FindSeat(i - offset, j)));
                            done++;
                        }
                    }
                    else
                    {
                        left = true; ;
                        done++;
                    }
                }
                if (!up)
                {
                    if (j - offset >= 0)
                    {
                        if (d[i][j-offset] == 'L')
                        {
                            up = true;
                            foundSeats.Add(seats.Find(x => x.FindSeat(i , j-offset)));
                            done++;
                        }
                    }
                    else
                    {
                        up = true; ;
                        done++;
                    }
                }
                if (!upLeft)
                {
                    if (i - offset >= 0 && j - offset >= 0)
                    {
                        if (d[i - offset][j - offset] == 'L')
                        {
                            upLeft = true;
                            foundSeats.Add(seats.Find(x => x.FindSeat(i - offset, j - offset)));
                            done++;
                        }
                    }
                    else
                    {
                        upLeft = true; ;
                        done++;
                    }
                }
                if (!upRight)
                {
                    if (i - offset >= 0 && j + offset < d[0].Length)
                    {
                        if (d[i - offset][j + offset] == 'L')
                        {
                            upRight = true;
                            foundSeats.Add(seats.Find(x => x.FindSeat(i - offset, j + offset)));
                            done++;
                        }
                    }
                    else
                    {
                        upRight = true;
                        done++;
                    }
                }
                offset++;
            }
            return foundSeats;
        }
    }
}
