using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _2116CheckifaParenthesesStringCanBeValid
    {
        public bool CanBeValid(string s, string locked)
        {
            if (s.Length % 2 != 0)
                return false;

            int balance = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(' || locked[i] == '0')
                    ++balance;
                else
                    --balance;
                
                if (balance < 0)
                    return false;
            }

            balance = 0;
            for (int i = s.Length - 1; i >= 0; --i)
            {
                if (s[i] == ')' || locked[i] == '0')
                    ++balance;
                else
                    --balance; // matched

                if (balance < 0)
                    return false;
            }

            return balance >= 0;
        }

        public bool CanBeValidStack(string s, string locked)
        {
            if (s.Length % 2 != 0)
                return false;

            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; ++i)
            {
                if (locked[i] == '0')
                    stack.Push('*');
                else // locked[i] == '1'
                {
                    if (s[i] == ')')
                    {
                        if (stack.Count > 0 && (stack.Peek() == '(' || stack.Peek() == '*'))
                            stack.Pop();
                        else
                            return false;
                    }
                    else // s[i] == '('
                        stack.Push(s[i]);
                }
            }

            if (stack.Count % 2 != 0)
                return false;

            var list = stack.ToArray();
            list = list.Reverse().ToArray();
            int leftParentheseToMatch = 0;
            for(int j = 0; j < list.Length && leftParentheseToMatch >= 0; ++j)
            {
                if (list[j] == '(' || leftParentheseToMatch == 0)
                    ++leftParentheseToMatch;
                else
                    --leftParentheseToMatch;                
            }

            if (leftParentheseToMatch == 0)
                return true;

            int rightParenthesesToMatch = 0;
            for(int j = list.Length - 1; j >= 0 && rightParenthesesToMatch >= 0; --j)
            {
                if (list[j] == '(' || rightParenthesesToMatch > 0) // greedily match
                    --rightParenthesesToMatch;
                else
                    ++rightParenthesesToMatch;  // change '*' to ')'
            }

            return rightParenthesesToMatch == 0;
        }
    }
}
