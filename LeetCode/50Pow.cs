using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _50Pow
    {
        public double MyPow(double x, int n)
        {
            double pow = PowIterative(x, n);

            if (n < 0)
                return 1.0 / pow;

            return pow;
        }

        private double PowIterative(double x, int n)
        {
            double res = 1.0, product = x;
            for(int i = n; i != 0; i /=2)
            {
                if (i % 2 != 0)
                    res *= product;

                product *= product;
            }

            return res;
        }

        private double PowRecursive(double x, int n)
        {
            if (n == 0)
                return 1.0;
            else if (n == 1 || n == -1)
                return x;
            else if (n == 2 || n == -2)
                return x * x;
            else if (x == 0 || x == 1)
                return x;

            double half = PowRecursive(x, n / 2);
            if (n % 2 != 0)
                return x * half * half;
            else
                return half * half;
        }
    }
}
