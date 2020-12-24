using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day23:Day
    {
        List<Cup> allcups;
        public Day23(List<string> d)
        {
            allcups = new List<Cup>();
            foreach(char c in d[0])
            {
                int number = (int)char.GetNumericValue(c);
                Cup a = new Cup(number);
                allcups.Add(a);
            }
            for(int i = 0; i < allcups.Count; i++)
            {
                if (i == allcups.Count - 1)
                {
                    allcups[i].Next = allcups[0];
                }
                else
                {
                    allcups[i].Next = allcups[i + 1];
                }
            }
        }
        public string Answer1()
        {
            Cup current = allcups[0];
            for(int i = 0; i < 100; i++)
            {
                Cup aftercurrent = current.Next;
                Cup mid = aftercurrent.Next;
                Cup tail = mid.Next;
                current.Next = tail.Next;
                int addto = current.number-1;
                while (true)
                {
                    if (addto == 0)
                        addto += 9;

                    if (aftercurrent.number == addto || mid.number == addto || tail.number == addto)
                    {
                        addto -= 1;
                    }
                    else
                    {
                        Cup toaddto = allcups.Find(x => x.number == addto);
                        tail.Next = toaddto.Next;
                        toaddto.Next = aftercurrent;
                        break;
                    }
                }
                current = current.Next;
            }
            StringBuilder sb = new StringBuilder();
            Cup toProcess = allcups.Find(x => x.number == 1).Next;
            while( toProcess != allcups.Find(x => x.number == 1))
            {
                sb.Append(toProcess.number);
                toProcess = toProcess.Next;
            }
            return sb.ToString();
        }

        public string Answer2()
        {
            ResetCups();
            Cup previous = allcups.Find(x => x.number == 8);
            Cup lastNum = allcups.Find(x => x.number == 9);
            for (int i = 10; i <= 1000000; i++)
            {
                Cup c = new Cup(i);
                allcups.Add(c);
                c.LowerNum = lastNum;
                previous.Next = c;
                previous = c;
                lastNum = c;
            }
            previous.Next = allcups[0];
            allcups.Find(x => x.number == 1).LowerNum = lastNum;
            Cup current = allcups[0];
            for (long l = 0; l < 10000000; l++)
            {
                Cup aftercurrent = current.Next;
                Cup mid = aftercurrent.Next;
                Cup tail = mid.Next;
                current.Next = tail.Next;
                Cup connectTo = current.LowerNum;
                while (true)
                {
                    if (aftercurrent == connectTo || mid == connectTo || tail == connectTo)
                    {
                        connectTo = connectTo.LowerNum;
                    }
                    else
                    {
                        tail.Next = connectTo.Next;
                        connectTo.Next = aftercurrent;
                        break;
                    }
                }
                current = current.Next;
            }
            Cup after1 = allcups.Find(x => x.number == 1).Next;
            long num1 = after1.number;
            long num2 = after1.Next.number;
            long answer = num1 * num2;
            return answer.ToString();
        }

        public void ResetCups()
        {
            string d = "624397158";
            allcups = new List<Cup>();
            foreach (char c in d)
            {
                int number = (int)char.GetNumericValue(c);
                Cup a = new Cup(number);
                allcups.Add(a);
            }
            for (int i = 0; i < allcups.Count; i++)
            {
                if (i == allcups.Count - 1)
                {
                    allcups[i].Next = allcups[0];
                }
                else
                {
                    allcups[i].Next = allcups[i + 1];
                }
                allcups[i].LowerNum = allcups.Find(x => x.number == allcups[i].number - 1);
            }
        }
    }
}
