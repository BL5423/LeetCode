using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class HappyNum
    {
        public bool IsHappy(int n)
        {
            int faster = Sum(n), slower = n;
            while (faster != slower)
            {
                faster = Sum(Sum(faster));
                slower = Sum(slower);
            }

            return faster == 1;
        }

        public bool IsHappyV1(int n)
        {
            HashSet<int> results = new HashSet<int>();
            while (n != 1)
            {
                n = Sum(n);
                if (results.Contains(n))
                {
                    return false;
                }

                results.Add(n);
            }

            return true;
        }

        private int Sum(int n)
        {
            int sum = 0;
            while (n != 0)
            {
                int re = n % 10;
                sum += re * re;
                n /= 10;
            }

            return sum;
        }
    }
}
