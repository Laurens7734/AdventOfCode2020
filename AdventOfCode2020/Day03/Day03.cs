using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day03 : Day
    {
        public List<string> data;
        public Day03(List<string> d)
        {
            data = d;
        }
        public string Answer1()
        {
            int trees = 0;
            int pos = 0;
            foreach(string s in data)
            {
                if (s[pos] == '#')
                    trees++;
                pos += 3;
                pos = pos % s.Length;
            }
            return trees.ToString();
        }

        public string Answer2()
        {
            long answer = 1;
            answer = answer * Treecount(1, 1);
            Console.WriteLine(answer.ToString());
            answer = answer * Treecount(1, 3);
            Console.WriteLine(answer.ToString());
            answer = answer * Treecount(1, 5);
            Console.WriteLine(answer.ToString());
            answer = answer * Treecount(1, 7);
            Console.WriteLine(answer.ToString());
            answer = answer * Treecount(2, 1);
            return answer.ToString();
        }

        public int Treecount(int down, int right)
        {
            int trees = 0;
            int pos = 0;
            for(int i = 0; i < data.Count; i += down)
            {
                string s = data[i];
                if (s[pos] == '#')
                    trees++;
                pos += right;
                pos %= s.Length;
            }
            return trees;
        }
    }
}
