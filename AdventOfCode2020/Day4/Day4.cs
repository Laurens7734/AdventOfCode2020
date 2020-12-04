using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day4 : Day
    {
        public List<string> data;
        public List<string> q1Req = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        public Day4(List<string> d)
        {
            data = d;
        }
        public string Answer1()
        {
            int count = 0;
            List<string> passport = new List<string>();

            foreach(string s in data)
            {
                if (s.Equals(""))
                {
                    if (CheckPassport(passport, q1Req))
                        count++;
                    
                    passport = new List<string>();
                }
                else
                {
                    passport.AddRange(Split(s).Keys);
                }
                    
            }
            if (CheckPassport(passport, q1Req))
                count++;

            return count.ToString();
        }

        public string Answer2()
        {
            int count = 0;
            Dictionary<string, string> passport = new Dictionary<string, string>();

            foreach (string s in data)
            {
                if (s.Equals(""))
                {
                    if (CheckPassport(new List<string>(passport.Keys), q1Req))
                        if (DataValidation(passport))
                            count++;

                    passport = new Dictionary<string, string>();
                }
                else
                {
                    passport = Merge(Split(s), passport);
                }

            }
            if (CheckPassport(new List<string>(passport.Keys), q1Req))
                if (DataValidation(passport))
                    count++;

            return count.ToString();
        }

        Dictionary<string, string> Split(string s)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            string[] elements = s.Split(' ');
            foreach(string a in elements)
            {
                response.Add(a.Substring(0, a.IndexOf(':')), a.Substring(a.IndexOf(':') + 1));
            }
            return response;
        }

        Dictionary<string, string> Merge(Dictionary<string, string> a, Dictionary<string, string> b)
        {
            foreach(KeyValuePair<string,string> k in b)
            {
                a.Add(k.Key, k.Value);
            }
            return a;
        }

        bool CheckPassport(List<string> passport, List<string> req)
        {
            if (passport.Count < req.Count)
            {
                return false;
            }
                

            foreach(string s in req)
            {
                if (!passport.Contains(s))
                {
                    return false;
                }
                   
            }
            return true;
        }

        bool DataValidation(Dictionary<string, string> passport)
        {
            if(int.Parse(passport["byr"]) < 1920 || int.Parse(passport["byr"]) > 2002)
                return false;
            if (int.Parse(passport["iyr"]) < 2010 || int.Parse(passport["iyr"]) > 2020)
                return false;
            if (int.Parse(passport["eyr"]) < 2020 || int.Parse(passport["eyr"]) > 2030)
                return false;
           
            switch (passport["ecl"])
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth": break;
                default: return false;
            }

            Regex r = new Regex(@"^#[0-9a-f]{6}$");
            if (!r.IsMatch(passport["hcl"]))
                return false;
            Regex r2 = new Regex(@"^[0-9]{9}$");
            if (!r2.IsMatch(passport["pid"]))
                return false;

            string height = passport["hgt"];
            if (height.EndsWith("cm"))
            {
                int i;
                if(int.TryParse(height.Substring(0, height.Length-2), out i))
                {
                    if (i > 193 || i < 150)
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else if (height.EndsWith("in"))
            {
                int i;
                if (int.TryParse(height.Substring(0, height.Length - 2), out i))
                {
                    if (i > 76 || i < 59)
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else 
                return false;


            return true;
        }
    }
}
