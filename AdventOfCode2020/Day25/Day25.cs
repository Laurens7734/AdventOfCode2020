using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day25:Day
    {
        long keyCard;
        long keyDoor;
        public Day25(List<string> d)
        {
            keyCard = int.Parse(d[0]);
            keyDoor = int.Parse(d[1]);
        }

        public string Answer1()
        {
            int cardloopsize = CalculateLoopSize(keyCard);
            int doorloopsize = CalculateLoopSize(keyDoor);

            long encriptionKey1 = RunLoop(keyCard, doorloopsize);
            long encriptionKey2 = RunLoop(keyDoor, cardloopsize);
            if(encriptionKey1 == encriptionKey2)
            {
                return encriptionKey1.ToString();
            }
            else
            {
                return "error";
            }
        }

        public string Answer2()
        {
            return "no second puzzle today :(";
        }

        int CalculateLoopSize(long key)
        {
            int currentvalue = 1;
            for(int i = 1; i < int.MaxValue; i++)
            {
                currentvalue = (7 * currentvalue) % 20201227;
                if (currentvalue == key)
                    return i;
            }
            return -1;
        }

        long RunLoop(long start, int amount)
        {
            long current = 1;
            for(int i = 0; i < amount; i++)
            {
                current = (start * current) % 20201227;
            }
            return current;
        }
    }
}
