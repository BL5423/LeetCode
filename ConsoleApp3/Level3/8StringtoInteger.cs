using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public enum State
    {
        Sign,

        Digits,

        Stop
    }

    public class _8StringtoInteger
    {
        public int MyAtoiV2(string s)
        {
            int min = int.MinValue;
            int max = int.MaxValue;
            int res = 0, sign = 1;
            State state = State.Sign;
            foreach (char ch in s)
            {
                switch (state)
                {
                    case State.Sign:
                        if (ch == '-' || ch == '+')
                        {
                            sign = (ch == '-') ? -1 : 1;
                            state = State.Digits;
                        }
                        else if (ch >= '0' && ch <= '9')
                        {
                            res += (ch - '0');
                            state = State.Digits;
                        }
                        else if (ch != ' ')
                        {
                            state = State.Stop;
                        }
                        break;

                    case State.Digits:
                        if (ch >= '0' && ch <= '9')
                        {
                            int num = ch - '0';
                            int temp = res;
                            temp *= 10;
                            temp += num;

                            if (res != 0 && temp / res < 10)
                            {
                                // overflow
                                state = State.Stop;
                                res = (sign == 1) ? max : min;
                            }
                            else
                            {
                                res = temp;
                            }
                        }
                        else
                        {
                            res = 0;
                            state = State.Stop;
                        }
                        break;

                    case State.Stop:
                        return res;
                }
            }

            return sign * res;
        }

        public int MyAtoiV1(string s)
        {
            long min = int.MinValue;
            min *= -1;
            long max = int.MaxValue;
            long res = 0;
            State state = State.Sign;
            char sign = '+';
            bool stop = false;
            foreach(char ch in s)
            {
                if (stop)
                    break;

                switch(state)
                {
                    case State.Sign:
                        if (ch == '-' || ch == '+')
                        {
                            sign = ch;
                            state = State.Digits;
                        }
                        else if (ch >= '0' && ch <= '9')
                        {
                            res += (ch - '0');
                            state = State.Digits;
                        }
                        else if (ch != ' ')
                        {
                            stop = true;
                        }
                        break;

                    case State.Digits:
                        if (ch >= '0' && ch <= '9')
                        {
                            res *= 10;
                            res += (ch - '0');

                            if (sign == '+' && res >= max)
                            {
                                res = max;
                                stop = true;
                            }
                            else if (sign == '-' && res >= min)
                            {
                                res = min;
                                stop = true;
                            }
                        }
                        else
                        {
                            stop = true;
                        }
                        break;
                }
            }

            return sign == '+' ? (int)res : (int)-res;
        }
    }
}
