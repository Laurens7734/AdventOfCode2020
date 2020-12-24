using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Cup
    {
        public readonly int number;
        public Cup Next { get; set; }

        public Cup LowerNum { get; set; }
        public Cup(int num)
        {
            number = num;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cup c)
            {
                if (c.number == number)
                    return true;
            }
            else if (obj.Equals(number))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return number;
        }
    }
}
