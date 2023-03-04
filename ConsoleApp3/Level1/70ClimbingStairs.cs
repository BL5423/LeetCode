using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _70ClimbingStairs
    {
        public int ClimbStairs(int n)
        {
            if (n <= 0)
                return n;

            int[,] initial = { { 1, 1 }, { 1, 0 } };
            int[,] res = MatrixPower(initial, n);

            return res[0, 0];
        }

        private int[,] MatrixPower(int[,] initial, int n)
        {
            // mutiplier mutiplies any matrix will result in the matrix itself
            int[,] mutiplier = { { 1, 0 }, { 0 , 1 } };
            while (n > 0)
            {
                if (n % 2 == 1)
                {
                    MatrixMultiply(mutiplier, initial);
                }

                n >>= 1;
                MatrixMultiply(initial, initial);
            }

            return mutiplier;
        }

        private void MatrixMultiply(int[,] a, int [,] b)
        {
            int c00 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
            int c01 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
            int c10 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
            int c11 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];

            a[0, 0] = c00;
            a[0, 1] = c01;
            a[1, 0] = c10;
            a[1, 1] = c11;
        }

        public int ClimbStairsV1(int n)
        {
            if (n <= 2)
                return n;

            int steps = 0;
            int n2 = 1;
            int n1 = 2;
            for(int i = 3; i <= n; ++i)
            {
                // total possibilities  =      take 2 steps  plus   take 1 step
                steps =                             n2         +         n1;
                n2 = n1;
                n1 = steps;
            }

            return steps;
        }
    }
}
