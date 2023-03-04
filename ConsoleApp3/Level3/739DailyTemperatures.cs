using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _739DailyTemperatures
    {
        public int[] DailyTemperaturesV2(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];
            int hottest = 0;
            for(int i = temperatures.Length - 1; i >= 0; --i)
            {
                int temp = temperatures[i];
                if (temp >= hottest)
                {
                    hottest = temp;
                    continue;
                }

                int days = 1;
                while (temp >= temperatures[i + days])
                {
                    days += res[i + days];
                }

                res[i] = days;
            }

            return res;
        }

        public int[] DailyTemperaturesV1(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];
            Stack<int> stack = new Stack<int>();
            for(int i = res.Length - 1; i >= 0; --i)
            {
                int temp = temperatures[i];
                while (stack.Count != 0)
                {
                    if (temperatures[stack.Peek()] > temp)
                        break;

                    stack.Pop();
                }

                if (stack.Count != 0)
                {
                    res[i] = stack.Peek() - i;
                }
                else
                {
                    res[i] = 0;
                }

                stack.Push(i);
            }

            return res;
        }
    }
}
