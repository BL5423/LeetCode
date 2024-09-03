using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _1047RemoveAllAdjacentDuplicatesInString
    {
        public string RemoveDuplicates(string s)
        {
            Stack<char> stack = new Stack<char>(s.Length);
            for (int i = 0; i < s.Length; ++i)
            {
                if (stack.Count == 0 || stack.Peek() != s[i])
                    stack.Push(s[i]);
                else
                {
                    while (stack.Count != 0 && stack.Peek() == s[i])
                        stack.Pop();
                }
            }

            return string.Join(string.Empty, stack.Reverse());
        }
    }
}
