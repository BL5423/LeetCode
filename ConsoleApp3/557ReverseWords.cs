using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _557ReverseWords
    {
        public string ReverseWords(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            Stack<char> stack = new Stack<char>();
            foreach(char ch in s)
            {
                if (ch == ' ')
                {
                    while(stack.Count > 0)
                    {
                        sb.Append(stack.Pop());
                    }
                    sb.Append(ch);
                }
                else
                {
                    stack.Push(ch);
                }
            }

            while (stack.Count > 0)
            {
                sb.Append(stack.Pop());
            }

            return sb.ToString();
        }
    }
}
