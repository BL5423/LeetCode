using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _22GenerateParentheses
    {
        public IList<string> GenerateParenthesis(int n)
        {
            var parentTheses = new HashSet<string>();
            if (n < 1)
                return parentTheses.ToList();
            parentTheses.Add("()");
            --n;

            while (--n >= 0)
            {
                parentTheses = GenerateOneMoreParentheses(parentTheses);
            }

            return parentTheses.ToList();
        }

        private HashSet<string> GenerateOneMoreParentheses(HashSet<string> parentheses)
        {
            var newParentheses = new HashSet<string>();
            foreach (var parenthese in parentheses)
            {
                for(int index = 0; index < parenthese.Length; ++index)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(parenthese.Substring(0, index));
                    sb.Append("()");
                    sb.Append(parenthese.Substring(index));

                    newParentheses.Add(sb.ToString());
                }
            }

            return newParentheses;
        }
    }


    public class _22GenerateParenthesesV2
    {
        public IList<string> GenerateParenthesis(int n)
        {
            var results = new List<string>();
            Generate("", n, n, n*2, results);

            return results;
        }

        private void Generate(string input, int numberOfLeft, int numberOfRight, int total, IList<string> results)
        {
            if (input.Length == total)
            {
                results.Add(input);
                return;
            }

            // We can start an opening bracket if we still have one(of n) left to place.
            
            if (numberOfLeft > 0)
            {
                Generate(input + "(", numberOfLeft - 1, numberOfRight, total, results);
            }

            // And we can start a closing bracket if it would not exceed the number of opening brackets.
            if (numberOfRight > 0 && numberOfLeft < numberOfRight)
            {
                Generate(input + ")", numberOfLeft, numberOfRight - 1, total, results);
            }
        }
    }
}
