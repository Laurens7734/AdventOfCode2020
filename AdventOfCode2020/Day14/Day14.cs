using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day14: Day
    {
        List<string> data;
        public Day14(List<string> d)
        {
            data = d;
        }

        public string Answer1()
        {
            string mask = "";
            Dictionary<long, long> memory = new Dictionary<long, long>();
            foreach(string s in data)
            {
                if (s.StartsWith("mask"))
                {
                    mask = s.Substring(7, 36);
                }
                else
                {
                    long memaddress = long.Parse(s.Substring(s.IndexOf('[') + 1, s.IndexOf(']')-(1 + s.IndexOf('['))));
                    string bin = NumToBin(long.Parse(s.Substring(s.IndexOf('=') + 2)));
                    bin = ApplyMask(bin, mask);
                    long value = BinToNum(bin);

                    if (memory.ContainsKey(memaddress))
                        memory[memaddress] = value;
                    else
                        memory.Add(memaddress, value);
                }
            }

            long total = 0;

            foreach(KeyValuePair<long,long> k in memory)
            {
                total += k.Value;
            }
            return total.ToString();
        }

        public string Answer2()
        {
            string mask = "";
            Dictionary<long, long> memory = new Dictionary<long, long>();
            foreach (string s in data)
            {
                if (s.StartsWith("mask"))
                {
                    mask = s.Substring(7, 36);
                }
                else
                {
                    long memaddress = long.Parse(s.Substring(s.IndexOf('[') + 1, s.IndexOf(']') - (1 + s.IndexOf('['))));
                    long value = long.Parse(s.Substring(s.IndexOf('=') + 2));

                    string bin = NumToBin(memaddress);
                    List<string> alladdresses = ApplyMask2(bin, mask);

                    foreach(string a in alladdresses)
                    {
                        long tempaddress = BinToNum(a);

                        if (memory.ContainsKey(tempaddress))
                            memory[tempaddress] = value;
                        else
                            memory.Add(tempaddress, value);
                    }
                    
                }
            }

            long total = 0;

            foreach (KeyValuePair<long, long> k in memory)
            {
                total += k.Value;
            }
            return total.ToString();
        }

        string NumToBin(long value)
        {
            long remainder = value;
            StringBuilder sb = new StringBuilder();
            long power = (long)Math.Pow(2, 35);

            while(power >= 1)
            {
                if(remainder >= power)
                {
                    sb.Append('1');
                    remainder -= power;
                }
                else
                {
                    sb.Append('0');
                }
                power /= 2;
            }

            return sb.ToString();
        }

        long BinToNum(string s)
        {
            long value = 0;

            for (int i = 0; i < 36; i++)
            {
                if(s[i] == '1')
                {
                    value += (long)(Math.Pow(2, 35) / Math.Pow(2, i));
                }
            }

            return value;
        }

        string ApplyMask(string value, string mask)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 36; i++)
            {
                if (mask[i] != 'X')
                    sb.Append(mask[i]);
                else
                    sb.Append(value[i]);
            }
            return sb.ToString();
        }

        List<string> ApplyMask2(string value, string mask)
        {
            List<string> answer = new List<string>();
            List<StringBuilder> allstrings = new List<StringBuilder>() { new StringBuilder() };
            for (int i = 0; i < 36; i++)
            {
                if (mask[i] == '1')
                    allstrings.ForEach(a => a.Append('1'));
                else if (mask[i] == 'X')
                {
                    List<StringBuilder> copy = new List<StringBuilder>();
                    allstrings.ForEach((item) => copy.Add(new StringBuilder(item.ToString())));
                    allstrings.ForEach(a => a.Append('0'));
                    copy.ForEach(a => a.Append('1'));
                    allstrings.AddRange(copy);
                }
                else
                    allstrings.ForEach(a => a.Append(value[i]));
            }

            foreach(StringBuilder sbd in allstrings)
            {
                answer.Add(sbd.ToString());
            }

            return answer;
        }
    }
}
