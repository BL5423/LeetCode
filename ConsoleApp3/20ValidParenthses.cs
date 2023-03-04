using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class ValidParenthses
    {
        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                switch (c)
                {
                    case '{':
                    case '[':
                    case '(':
                        stack.Push(c);
                        break;

                    case '}':
                        if (stack.Count == 0)
                            return false;
                        else
                        {
                            if (stack.Pop() != '{')
                                return false;
                        }
                        break;

                    case ']':
                        if (stack.Count == 0)
                            return false;
                        else
                        {
                            if (stack.Pop() != '[')
                                return false;
                        }
                        break;


                    case ')':
                        if (stack.Count == 0)
                            return false;
                        else
                        {
                            if (stack.Pop() != '(')
                                return false;
                        }
                        break;
                }
            }

            return stack.Count() == 0;
        }
    }
}
