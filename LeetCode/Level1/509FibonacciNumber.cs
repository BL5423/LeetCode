using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _509FibonacciNumber
    {
        public int Fib(int n)
        {
            if (n <= 1)
                return n;

            int[,] intial = { { 1, 1 }, { 1, 0 } };
            MatrixPower(intial, n - 1);

            return intial[0, 0];
        }

        private void MatrixPower(int[,] f, int power)
        {
            if (power == 1)
                return;

            MatrixPower(f, power / 2);
            f = MatrixMultiply(f, f);
            if (power % 2 == 1)
            {
                int[,] intial = { { 1, 1 }, { 1, 0 } };
                MatrixMultiply(f, intial);
            }
        }

        private int[,] MatrixMultiply(int[,] f1, int[,] f2)
        {
            int a = f1[0, 0] * f2[0, 0] + f1[0, 1] * f2[1, 0];
            int b = f1[0, 0] * f2[0, 1] + f1[0, 1] * f2[1, 1];
            int c = f1[1, 0] * f2[0, 0] + f1[1, 1] * f2[1, 0];
            int d = f1[1, 0] * f2[0, 1] + f1[1, 1] * f2[1, 1];

            f1[0, 0] = a;
            f1[0, 1] = b;
            f1[1, 0] = c;
            f1[1, 1] = d;
            return f1;
        }

        public int Fibv1(int n)
        {
            int sum = 0;
            int n2 = 0, n1 = 1;
            for(int i = 1; i < n; ++i)
            {
                sum = n2 + n1;
                n2 = n1;
                n1 = sum;
            }

            return n != 1 ? sum : n1;
        }
    }
}
