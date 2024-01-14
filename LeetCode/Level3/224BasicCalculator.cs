using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _224BasicCalculator
    {
        public int Calculate(string s)
        {
            s += "@";
            Stack<int> nums = new Stack<int>();
            Stack<char> ops = new Stack<char>();
            int curNum = 0, lastRes = 0;
            char curOp = '+';
            for (int index = 0; index < s.Length; ++index)
            {
                char c = s[index];
                if (c >= '0' && c <= '9')
                {
                    curNum *= 10;
                    curNum += c - '0';
                }
                else if (c != ' ')
                {
                    lastRes += curNum * (curOp == '+' ? 1 : -1);
                    curNum = 0;

                    if (c == '+' || c == '-')
                    {
                        curOp = c;
                    }
                    else if (c == '(')
                    {
                        ops.Push(curOp);
                        curOp = '+';
                        nums.Push(lastRes);
                        lastRes = 0;
                    }
                    else if (c == ')')
                    {
                        lastRes *= (ops.Pop() == '+' ? 1 : -1);
                        lastRes = nums.Pop() + lastRes; // merge
                    }
                }
            }

            return lastRes;
        }

        public int CalculateV1(string s)
        {
            int curNum = 0;
            var stack = new Stack<object>(s.Length);
            foreach(var ch in s)
            {
                switch(ch)
                {
                    case '(':
                        stack.Push(ch);
                        break;

                    case ')':
                        stack.Push(curNum);
                        curNum = 0;
                        LinkedList<object> list = new LinkedList<object>();
                        while (stack.Count > 0)
                        {
                            var item = stack.Pop();
                            int sign = 1;
                            if (item.Equals('('))
                            {
                                int res = 0;
                                foreach(var op in list)
                                {
                                    if (op is int)
                                    {
                                        res += ((int)op) * sign;
                                    }
                                    else
                                    {
                                        sign = ((char)op == '+' ? 1 : -1);
                                    }
                                }

                                stack.Push(res);
                                break;
                            }
                            else
                            {
                                list.AddFirst(item);
                            }
                        }
                        break;

                    case '+':
                    case '-':
                        stack.Push(curNum);
                        curNum = 0;
                        stack.Push(ch);
                        break;

                    case ' ':
                        break;

                    default: // digit
                        curNum *= 10;
                        curNum += (ch - '0');
                        break;
                }
            }

            if (curNum != 0)
            {
                stack.Push(curNum);
            }

            if (stack.Count > 1)
            {
                LinkedList<object> list = new LinkedList<object>();
                while (stack.Count != 0) 
                {
                    list.AddFirst(stack.Pop());
                }

                int res = 0, sign = 1;
                foreach (var op in list)
                {
                    if (op is int)
                    {
                        res += ((int)op) * sign;
                    }
                    else
                    {
                        sign = ((char)op == '+' ? 1 : -1);
                    }
                }

                stack.Push(res);
            }
            else if (stack.Count == 0)
            {
                stack.Push(0);
            }

            return (int)stack.Peek();
        }
    }
}
