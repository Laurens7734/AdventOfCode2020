using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day08 : Day
    {
        List<string> data;
        int accumulator = 0;

        public Day08(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            int position = 0;

            List<int> used = new List<int>();

            while (position < data.Count)
            {
                if (used.Contains(position)) 
                    return accumulator.ToString();
                else
                    used.Add(position);

                position = RunCommand(position);
            }
            return "no loop found";
        }

        public string Answer2()
        {
            List<int> possibleMistakes = GetPossibleMistakes();
            FindAndFix(possibleMistakes);

            accumulator = 0;
            int position = 0;

            while (position < data.Count)
            {
                position = RunCommand(position);
            }
            return accumulator.ToString();
        }

        int RunCommand(int p)
        {
            int position = p;
            string s = data[position];
            string[] split = s.Split(' ');

            switch (split[0])
            {
                case "acc": accumulator += int.Parse(split[1]); position++; break;
                case "nop": position++; break;
                case "jmp": position += int.Parse(split[1]); break;
            }

            return position;
        }

        int LoopSize(int start)
        {
            int position = start;

            List<int> used = new List<int>();

            while (position < data.Count)
            {
                if (used.Contains(position))
                {
                    return (used.Count - used.IndexOf(position));
                }
                else
                    used.Add(position);

                position = RunCommand(position);
            }
            return 0;
        }

        List<int> GetPossibleMistakes()
        {
            //just realized this may actually not always work and should probably return used
            int position = 0;

            List<int> used = new List<int>();
            List<int> possibleMistakes = new List<int>();

            while (position < data.Count)
            {
                if (used.Contains(position))
                {
                    possibleMistakes = used.GetRange(used.IndexOf(position), used.Count - used.IndexOf(position));
                    break;
                }
                else
                    used.Add(position);

                position = RunCommand(position);
            }

            return possibleMistakes;
        }

        void FindAndFix(List<int> possibleMistakes) 
        {
            foreach (int i in possibleMistakes)
            {
                string s = data[i];
                string[] split = s.Split(' ');

                switch (split[0])
                {
                    case "acc": continue;
                    case "nop": data[i] = $"jmp {split[1]}"; break;
                    case "jmp": data[i] = $"nop {split[1]}"; break;
                }

                if (LoopSize(i) == 0)
                    break;
                else
                    data[i] = s;
            }
        }
    }
}
