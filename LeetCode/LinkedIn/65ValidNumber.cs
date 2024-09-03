using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _65ValidNumber
    {
        private static IDictionary<string, int>[] DFA = new IDictionary<string, int>[] 
        {
            new Dictionary<string, int> { { "digit", 1 }, { "sign", 2 }, { "dot", 3 } },
            new Dictionary<string, int> { { "digit", 1 }, { "dot",  4 }, { "exp", 5 } },
            new Dictionary<string, int> { { "digit", 1 }, { "dot",  3 } },
            new Dictionary<string, int> { { "digit", 4 } },
            new Dictionary<string, int> { { "digit", 4 }, { "exp",  5 } },
            new Dictionary<string, int> { { "digit", 7 }, { "sign", 6 } },
            new Dictionary<string, int> { { "digit", 7 } },
            new Dictionary<string, int> { { "digit", 7 } },
        };

        public bool IsNumber(string s)
        {
            int currentState = 0;
            string type = "";
            foreach(var ch in s)
            {
                if (ch >= '0' && ch <= '9')
                {
                    type = "digit";
                }
                else if (ch == 'e' || ch == 'E')
                {
                    type = "exp";
                }
                else if (ch == '+' || ch == '-')
                {
                    type = "sign";
                }
                else if (ch == '.')
                {
                    type = "dot";
                }
                else
                {
                    return false;
                }

                if (DFA[currentState].TryGetValue(type, out int nextState))
                {
                    currentState = nextState;
                }
                else
                {
                    return false;
                }
            }

            return currentState == 1 || currentState == 4 || currentState == 7;
        }

        public bool IsNumberV1(string s)
        {
            // pattern of valid numbers:
            //        [(+-)(0~9)] | [((.)[0~9])] (eE)(+-)(0~9)
            //states:  1    2          3   4      5   6   7
            // where () means optional, [] means required
            int state = 0;
            var prevStates = new HashSet<int>(7);
            foreach (char ch in s)
            {
                if (state == 0)
                {
                    // initialize state
                    if (ch == '+' || ch == '-')
                        state = 1;
                    else if (ch >= '0' && ch <= '9')
                        state = 2;
                    else if (ch == '.')
                        state = 3;
                    else
                        return false;
                }
                else
                {
                    if (ch >= '0' && ch <= '9')
                    {
                        if (state == 1)
                            state = 2;
                        else if (state == 3)
                            state = 4;
                        else if (state == 5 || state == 6)
                            state = 7;

                        if (state != 2 && state != 4 && state != 7)
                            return false;
                    }
                    else if (ch == '.')
                    {
                        if (state == 3)
                            return false;

                        if (state == 1 || state == 2)
                            state = 3;
                        else
                            return false;
                    }
                    else if (ch == 'e' || ch == 'E')
                    {
                        if (state >= 5)
                            return false;

                        if (!prevStates.Contains(2) && !prevStates.Contains(4))
                        {
                            return false;
                        }

                        if (state == 2 || state == 3 || state == 4)
                            state = 5;
                        else
                            return false;
                    }
                    else if (ch == '+' || ch == '-')
                    {
                        if (state != 5)
                            return false;

                        state = 6;
                    }
                    else
                    {
                        return false;
                    }
                }

                prevStates.Add(state);
            }

            // valid end states 
            return state == 2 || (prevStates.Contains(2) && state == 3) || state == 4 || state == 7;
        }
    }
}
