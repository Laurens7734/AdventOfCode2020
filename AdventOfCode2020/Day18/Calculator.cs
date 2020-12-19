using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    class Calculator
    {
        public string toCalculate;
        List<char> operators = new List<char>();

        public Calculator(string s)
        {
            StringBuilder myCalculation = new StringBuilder();
            StringBuilder passOn = new StringBuilder();

            int bracketCount = 0;
            bool inBrackets = false;

            foreach(char c in s)
            {
                if (inBrackets)
                {
                    if (c == ')')
                        bracketCount--;
                    if (bracketCount == 0)
                    {
                        inBrackets = false;
                        myCalculation.Append(new Calculator(passOn.ToString()).Resolve());
                        passOn = new StringBuilder();
                        continue;
                    }
                    if (c == '(')
                        bracketCount++;
                    passOn.Append(c);
                        
                }
                else if(c == '(')
                {
                    bracketCount++;
                    inBrackets = true;
                }
                else
                {
                    myCalculation.Append(c);
                    if (c == '+' || c == '*')
                        operators.Add(c);
                }
            }
            toCalculate = myCalculation.ToString();
        }

        public Calculator(string s, bool b)
        {
            StringBuilder myCalculation = new StringBuilder();
            StringBuilder passOn = new StringBuilder();

            int bracketCount = 0;
            bool inBrackets = false;

            foreach (char c in s)
            {
                if (inBrackets)
                {
                    if (c == ')')
                        bracketCount--;
                    if (bracketCount == 0)
                    {
                        inBrackets = false;
                        myCalculation.Append(new Calculator(passOn.ToString(),b).Resolve2());
                        passOn = new StringBuilder();
                        continue;
                    }
                    if (c == '(')
                        bracketCount++;
                    passOn.Append(c);

                }
                else if (c == '(')
                {
                    bracketCount++;
                    inBrackets = true;
                }
                else
                {
                    myCalculation.Append(c);
                    if (c == '+' || c == '*')
                        operators.Add(c);
                }
            }
            toCalculate = myCalculation.ToString();
        }

        public long Resolve()
        {
            List<string> nums = new List<string>(toCalculate.Split(new char[]{ '+', '*'}));
            List<int> numbers = nums.Select(int.Parse).ToList();
            long result = numbers[0];
            for(int i = 1; i < numbers.Count; i++)
            {
                if (operators[i - 1] == '+')
                {
                    result += numbers[i];
                }
                else
                    result *= numbers[i];
            }
            return result;
        }

        public long Resolve2()
        {
            long result = 0;
            List<string> additions = new List<string>(toCalculate.Split('*'));
            if(additions.Count > 1)
            {
                result++;
                foreach(string s in additions)
                {
                    result *= new Calculator(s, false).Resolve2();
                }
            }
            else
            {
                List<long> numbers = toCalculate.Split('+').Select(long.Parse).ToList();
                foreach(long l in numbers)
                {
                    result += l;
                }
            }
            
            

            return result;
        }
    }
}
