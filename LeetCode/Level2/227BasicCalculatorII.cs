using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _227BasicCalculatorII
    {
        public int Calculate(string s)
        {
            int lastNum = 0, curNum = 0;
            int curSign = 1;
            s += '+';
            int index = 0;
            while (index < s.Length)
            {
                char ch = s[index];
                if (ch >= '0' && ch <= '9')
                {
                    curNum *= 10;
                    curNum += ch - '0';
                }
                else
                {
                    if (ch != ' ')
                    {
                        if (ch == '+' || ch == '-')
                        {
                            lastNum += curNum * curSign;
                            curNum = 0; // reset
                            curSign = (ch == '+' ? 1 : -1);
                        }
                        else // ch == '*' or '/'
                        {
                            ++index;
                            int nextNum = 0;
                            while (s[index] == ' ')
                                ++index;

                            while (s[index] >= '0' && s[index] <= '9')
                            {
                                nextNum *= 10;
                                nextNum += s[index] - '0';
                                ++index;
                            }

                            curNum = (ch == '*' ? curNum * nextNum : curNum / nextNum);
                            --index;
                        }
                    }
                }

                ++index;
            }

            return lastNum;
        }

        public int CalculateV3(string s)
        {
            char lastOperator = '+';
            int val = 0, finalVal = 0;
            int? prevVal = null;
            for (int index = 0; index < s.Length; ++index)
            {
                char ch = s[index];
                if (ch >= '0' && ch <= '9')
                {
                    val = val * 10 + (ch - '0');
                }

                if ((!(ch >= '0' && ch <= '9') && ch != ' ') || index == s.Length - 1)
                {
                    if (prevVal != null)
                    {
                        if (lastOperator == '+' || lastOperator == '-')
                        {
                            finalVal += prevVal.Value;
                        }
                        else
                        {
                            if (lastOperator == '*')
                            {
                                val = prevVal.Value * val;
                            }
                            else if (lastOperator == '/')
                            {
                                val = prevVal.Value / val;
                            }
                        }
                    }

                    prevVal = (lastOperator == '-' ? -val : val);
                    val = 0;
                    lastOperator = ch;
                }
            }

            return finalVal + prevVal.Value;
        }

        public int CalculateV2(string s)
        {
            Stack<int> stack = new Stack<int>();
            char lastOperator = '+';
            int val = 0;
            for(int index = 0; index < s.Length; ++index)
            {
                char ch = s[index];
                if (ch >= '0' && ch <= '9')
                {
                    val = val * 10 + (ch - '0');
                }                
                
                if ((!(ch >= '0' && ch <= '9') && ch != ' ') || index == s.Length - 1)
                {
                    if (lastOperator == '*')
                    {
                        var prevVal = stack.Pop();
                        val = prevVal * val;
                    }
                    else if (lastOperator == '/')
                    {
                        var prevVal = stack.Pop();
                        val = prevVal / val;
                    }
                    else if (lastOperator == '-')
                    {
                        val = -val;
                    }

                    stack.Push(val);
                    lastOperator = ch;
                    val = 0;
                }
            }

            val = 0;
            while (stack.Count > 0)
            {
                val += stack.Pop();
            }

            return val;
        }

        public int CalculateV1(string s)
        {
            Stack<long> vals = new Stack<long>(s.Length);
            Stack<char> ops = new Stack<char>(s.Length);
            int index = 0;
            long val = 0;
            while (index < s.Length)
            {
                char ch = s[index++];
                switch(ch)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        if (ops.Count > 0)
                        {
                            var lastOperator = ops.Peek();
                            if (lastOperator == '*' || lastOperator == '/')
                            {
                                // lastOp and curOp have same priority, then evaluate prev operator and its vals
                                ops.Pop();
                                var prevVal = vals.Pop();
                                if (lastOperator == '*')
                                {
                                    val *= prevVal;
                                }
                                else
                                {
                                    val = prevVal / val;
                                }
                            }
                        }

                        vals.Push(val);
                        val = 0;
                        ops.Push(ch);
                        break;

                    case ' ':
                        break;

                    default:
                        val = val * 10 + (ch - '0');
                        break;
                }
            }

            vals.Push(val);
            val = 0;
            if (ops.Count > 0)
            {
                var op = ops.Peek();
                if (op == '*' || op == '/')
                {
                    ops.Pop();
                    var val1 = vals.Pop();
                    var val2 = vals.Pop();
                    if (op == '*')
                        val = val2 * val1;
                    else
                        val = val2 / val1;

                    vals.Push(val);
                    val = 0;
                }

                if (ops.Count > 0)
                {
                    var listOps = new LinkedList<char>();
                    var listVals = new LinkedList<long>();
                    while (ops.Count > 0)
                    {
                        listOps.AddFirst(ops.Pop());
                        if (vals.Count > 0)
                            listVals.AddFirst(vals.Pop());
                        if (vals.Count > 0)
                            listVals.AddFirst(vals.Pop());
                    }

                    val = listVals.First();
                    listVals.RemoveFirst();
                    while (listVals.Count > 0)
                    {
                        var nextVal = listVals.First();
                        listVals.RemoveFirst();
                        op = listOps.First();
                        listOps.RemoveFirst();

                        if (op == '+')
                            val += nextVal;
                        else
                            val -= nextVal;
                    }

                    vals.Push(val);
                }
            }

            return (int)vals.Peek();
        }
    }
}
