using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _394DecodeString
    {
        public string DecodeString(string s)
        {
            Stack<object> stack = new Stack<object>();
            int index = 0;
            StringBuilder prefix = new StringBuilder();
            while (index < s.Length)
            {
                int k = 0;
                char ch = s[index];
                while (ch >= '0' && ch <= '9')
                {
                    k *= 10;
                    k += (ch - '0');
                    ++index;
                    ch = s[index];
                }

                if (k != 0)
                {
                    // push current prefix into stack
                    if (prefix.Length > 0)
                    {
                        stack.Push(prefix.ToString());
                        prefix.Clear();
                    }
                    else
                    {
                        stack.Push(string.Empty);
                    }

                    // push repeat into stack
                    stack.Push(k);
                }
                if (ch == ']')
                {
                    // time to pop up
                    int repeat = (int)stack.Pop();
                    string pfx = stack.Pop() as string;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(pfx);
                    sb.Insert(sb.Length, prefix.ToString(), repeat);
                    prefix = sb;
                }
                else if (ch != '[')
                {
                    // append ch to prefix
                    prefix.Append(ch);
                }

                ++index;
            }

            return prefix.ToString();
        }

        public string DecodeStringV1(string s)
        {
            int index = 0;
            return DecodeStringV1(s, ref index);
        }

        private string DecodeStringV1(string s, ref int index)
        {
            StringBuilder sb = new StringBuilder();
            while (index < s.Length)
            {
                int k = 0;
                char ch = s[index];
                while (ch >= '0' && ch <= '9')
                {
                    k *= 10;
                    k += (ch - '0');
                    ++index;
                    ch = s[index];
                }
                if (k != 0)
                {
                    sb.Insert(sb.Length, DecodeStringV1(s, ref index), k);
                }
                else if (ch != '[' && ch != ']')
                {
                    sb.Append(ch);
                }
                else if (ch == ']')
                {
                    return sb.ToString();
                }

                ++index;
            }

            return sb.ToString();
        }
    }
}
