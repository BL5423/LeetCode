using LC.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _772BasicCalculatorIII
    {
        public int Calculate(string s)
        {
            s += "+";
            int index = 0;
            return this.Evaluate(s, ref index);
        }

        private int Evaluate(string s, ref int index)
        {
            int cur = 0;
            char prevOperator = '+';
            Stack<int> stack = new Stack<int>();
            while (index < s.Length)
            {
                if (s[index] == '(')
                {
                    ++index;
                    cur = Evaluate(s, ref index);
                }
                else
                {
                    if (s[index] >= '0' && s[index] <= '9')
                    {
                        cur *= 10;
                        cur += s[index] - '0';
                    }
                    else
                    {
                        if (prevOperator == '+')
                        {
                            stack.Push(cur);
                        }
                        else if(prevOperator == '-')
                        {
                            stack.Push(-cur);
                        }
                        else if (prevOperator == '*')
                        {
                            stack.Push(stack.Pop() * cur);
                        }
                        else
                        {
                            stack.Push(stack.Pop() / cur);
                        }

                        if (s[index] == ')')
                        {
                            ++index;
                            break;
                        }

                        prevOperator = s[index];
                        cur = 0;
                    }

                    ++index;
                }
            }

            int res = 0;
            while (stack.Count != 0)
            {
                res += stack.Pop();
            }

            return res;
        }

        public int CalculateV2(string s)
        {            
            s += '+';
            Stack<object> cachedResults = new Stack<object>(s.Length / 2);
            int curNum = 0;
            char prevOperator = '+';
            foreach(var ch in s)
            {
                if (ch >= '0' && ch <= '9')
                {
                    curNum *= 10;
                    curNum += ch - '0';
                }
                else
                {
                    if (ch == '(')
                    {
                        // save for the current term and start a new one(in the parenthese)
                        cachedResults.Push(prevOperator);
                        prevOperator = '+'; // reset;
                    }
                    else if (prevOperator == '+' || prevOperator == '-' || prevOperator == '*' || prevOperator == '/')
                    {
                        if (prevOperator == '+')
                        {
                            cachedResults.Push(curNum);
                        }
                        else if (prevOperator == '-')
                        {
                            cachedResults.Push(-curNum);
                        }
                        else if (prevOperator == '*')
                        {
                            cachedResults.Push((int)cachedResults.Pop() * curNum);
                        }
                        else //if (prevOperator == '/')
                        {
                            cachedResults.Push((int)cachedResults.Pop() / curNum);
                        }

                        prevOperator = ch;
                        curNum = 0;

                        if (ch == ')')
                        {
                            int result = 0;
                            object op = cachedResults.Pop();
                            while (op is int)
                            {
                                result += (int)op;
                                op = cachedResults.Pop();
                            }

                            curNum = result;
                            prevOperator = (char)op;
                        }
                    }
                }
            }

            int lastResult = 0;
            while (cachedResults.Count > 0)
            {
                lastResult += (int)cachedResults.Pop();
            }

            return lastResult;
        }

        public int CalculateV1(string s)
        {
            Stack<int> leftParensis = new Stack<int>(s.Length / 2);
            //                       start, (len, result)
            var p2p = new Dictionary<int, (int, int)>(s.Length / 2);
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(')
                {
                    leftParensis.Push(i);
                }
                else if (s[i] == ')')
                {
                    int start = leftParensis.Pop();
                    int res = CalculateImpl(s.Substring(start + 1, i - start - 1), start + 1, p2p);
                    p2p.Add(start, (i - start, res));
                }
            }

            return CalculateImpl(s, 0, p2p);
        }

        private static int CalculateImpl(string s, int offset, Dictionary<int, (int, int)> p2p)
        {
            LinkedList<string> tokens = new LinkedList<string>();
            int index = 0, curNum = 0;
            bool remaining = false;
            while (index < s.Length)
            {
                if (s[index] >= '0' && s[index] <= '9')
                {
                    curNum *= 10;
                    curNum += s[index] - '0';
                    remaining = true;

                    ++index;
                }
                else
                {
                    if (remaining)
                    {
                        tokens.AddLast(curNum.ToString());
                        curNum = 0;
                    }
                    remaining = false;

                    if (s[index] == '(')
                    {
                        // reuse calculated result
                        var r = p2p[index + offset];
                        tokens.AddLast(r.Item2.ToString());
                        index += r.Item1 + 1;
                    }
                    else if (s[index] == '*')
                    {
                        int next = 0;
                        ++index;
                        if (s[index] >= '0' && s[index] <= '9')
                        {
                            while (index < s.Length && s[index] >= '0' && s[index] <= '9')
                            {
                                next *= 10;
                                next += s[index] - '0';
                                ++index;
                            }
                        }
                        else// if (s[index] == '(')
                        {
                            // reuse calculated result
                            var r = p2p[index + offset];
                            next = r.Item2;
                            index += r.Item1 + 1;
                        }

                        var m = int.Parse(tokens.Last.Value) * next;
                        tokens.RemoveLast();
                        tokens.AddLast(m.ToString());
                    }
                    else if (s[index] == '/')
                    {
                        int next = 0;
                        ++index;
                        if (s[index] >= '0' && s[index] <= '9')
                        {
                            while (index < s.Length && s[index] >= '0' && s[index] <= '9')
                            {
                                next *= 10;
                                next += s[index] - '0';
                                ++index;
                            }
                        }
                        else// if (s[index] == '(')
                        {
                            // reuse calculated result
                            var r = p2p[index + offset];
                            next = r.Item2;
                            index += r.Item1 + 1;
                        }

                        var m = int.Parse(tokens.Last.Value) / next;
                        tokens.RemoveLast();
                        tokens.AddLast(m.ToString());
                    }
                    else // "+" "-"
                    {
                        tokens.AddLast(s[index].ToString());

                        ++index;
                    }
                }
            }

            if (remaining)
                tokens.AddLast(curNum.ToString());

            int res = 0;
            var token = tokens.First;
            while (token != null)
            {
                if (token.Value == "+")
                {
                    token = token.Next;
                    res += int.Parse(token.Value);
                }
                else if (token.Value == "-")
                {
                    token = token.Next;
                    res -= int.Parse(token.Value);
                }
                else
                {
                    res = int.Parse(token.Value);
                }

                token = token.Next;
            }

            return res;
        }
    }
}
