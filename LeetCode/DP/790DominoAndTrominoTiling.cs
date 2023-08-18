using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _790DominoAndTrominoTiling
    {
        private const int module = 1000000007;

        public int NumTilings_Matrix(int n)
        {
            if (n <= 2)
                return n;
            if (n == 3)
                return 5;

            // f(k) = 2*f(k-1) + f(k-3)

            long[][] coefficiencies = new long[3][];
            // f
            coefficiencies[0] = new long[] { 2, 0, 1 };
            // f1
            coefficiencies[1] = new long[] { 1, 0, 0 };
            // p
            coefficiencies[2] = new long[] { 0, 1, 0 };
            // cofficienties * [f(k-1), f(k-2), f(k-3)]t = [f(k), f(k-1), p(k-2)]t

            //                      f(3)  f(2)  f(1)
            int[] res = new int[] { 5, 2, 1 };
            var finalMultiplier = Multiply(coefficiencies, n - 3, new Dictionary<int, long[][]>(n));
            long nums = finalMultiplier[0][0] * res[0] + finalMultiplier[0][1] * res[1] + finalMultiplier[0][2] * res[2];
            return (int)(nums % module);
        }

        public int NumTilings_MatrixV1(int n)
        {
            if (n <= 2)
                return n;

            long[][] coefficiencies = new long[3][];
            // f
            coefficiencies[0] = new long[] { 1, 1, 2 };
            // f1
            coefficiencies[1] = new long[] { 1, 0, 0 };
            // p
            coefficiencies[2] = new long[] { 0, 1, 1 };
            // cofficienties * [f(k-1), f(k-2), p(k-1)]t = [f(k), f(k-1), p(k)]t

            //                      f(2)  f(1)  p(2)
            int[] res = new int[] { 2,    1,    1   };
            var finalMultiplier = Multiply(coefficiencies, n - 2, new Dictionary<int, long[][]>(n));
            long nums = finalMultiplier[0][0] * res[0] + finalMultiplier[0][1] * res[1] + finalMultiplier[0][2] * res[2];
            return (int)(nums % module);
        }

        private long[][] Multiply(long[][] matrix1, long[][] matrix2)
        {
            long[][] res = new long[matrix1.Length][];
            for(int i = 0; i < matrix1.Length; ++i)
            {
                res[i] = new long[matrix2[0].Length];
                for (int k = 0; k < matrix2[0].Length; ++k)
                {
                    long sum = 0;
                    for (int j = 0; j < matrix1[i].Length; ++j)
                    {
                        sum += (matrix1[i][j] * matrix2[j][k]) % module;
                    }

                    res[i][k] = sum % module;
                }                
            }

            return res;
        }

        private long[][] Multiply(long[][] matrix1, int power, Dictionary<int, long[][]> cache)
        {
            if (power == 1)
                return matrix1;

            if (cache.TryGetValue(power, out long[][] res))
                return res;

            var half = Multiply(matrix1, power >> 1, cache);

            res = (power % 2 == 0) ? Multiply(half, half) : Multiply(Multiply(half, half), matrix1);
            cache.Add(power, res);
            return res;
        }

        public int NumTilings_BottomUp_ConstantSpace(int n)
        {
            if (n <= 1)
                return n;

            long f1 = 2, f2 = 1, p1 = 1;
            for (int i = 3; i <= n; i++)
            {
                long f = (f1 + f2 + p1 * 2) % module;
                long p = (f2 + p1) % module;

                f2 = f1;
                f1 = f;
                p1 = p;
            }

            return (int)(f1 % module);
        }

        public int NumTilings_BottomUp(int n)
        {
            // dp[i, j] indicates the # of layouts for width i with fully covered 0 or partially covered 1
            long[,] dp = new long[n, 2];
            dp[0, 0] = 1;
            if (n > 1)
            {
                dp[1, 0] = 2;
                dp[1, 1] = 1;
                for (int i = 2; i < n; i++)
                {
                    dp[i, 0] = (dp[i - 1, 0] + dp[i - 2, 0] + dp[i - 1, 1] * 2) % module;
                    dp[i, 1] = (dp[i - 2, 0] + dp[i - 1, 1]) % module;
                }
            }

            return (int)(dp[n - 1, 0] % module);
        }

        public int NumTilings_TopDown(int n)
        {
            int[,] cache = new int[n, 7];
            int layouts = this.NumTilings_TopDown(n, 0, 1, cache);
            return layouts != -1 ? layouts : 0;
        }

        private int NumTilings_TopDown(int n, int index, int prevTile, int[,] cache)
        {
            int leftWidth = n - index;
            if (leftWidth < 0)
                return -1;
            else if (leftWidth == 0)
            {
                if (prevTile == 1 || prevTile == 2 || prevTile == 4 || prevTile == 6)
                    return 1;

                return -1;
            }
            else
            {
                if (cache[index, prevTile] == 0)
                {
                    int layouts = 0;
                    switch(prevTile)
                    {
                        case 1:
                        case 4:
                        case 6:
                            int layout1 = NumTilings_TopDown(n, index + 1, 1, cache);
                            layouts += (layout1 != -1 ? layout1 % module : 0);
                            layouts %= module;
                            int layout2 = NumTilings_TopDown(n, index + 2, 2, cache);
                            layouts += (layout2 != -1 ? layout2 % module: 0);
                            layouts %= module;
                            int layout3 = NumTilings_TopDown(n, index + 2, 3, cache);
                            layouts += (layout3 != -1 ? layout3 % module : 0);
                            layouts %= module;
                            int layout4 = NumTilings_TopDown(n, index + 2, 5, cache); 
                            layouts += (layout4 != -1 ? layout4 % module : 0);
                            break;

                        case 2:
                            int layout5 = NumTilings_TopDown(n, index, 1, cache); // 2 + 2 -> 1
                            layouts += (layout5 != -1 ? layout5 % module : 0);
                            break;

                        case 3:
                            int layout6 = NumTilings_TopDown(n, index + 1, 1, cache); // 3 + 4 -> 1
                            layouts += (layout6 != -1 ? layout6 % module : 0);
                            layouts %= module;
                            int layout7 = NumTilings_TopDown(n, index + 1, 5, cache); // 3 + 2 -> 5
                            layouts += (layout7 != -1 ? layout7 % module : 0);
                            break;

                        case 5:
                            int layout8 = NumTilings_TopDown(n, index + 1, 1, cache); // 5 + 6 -> 1
                            layouts += (layout8 != -1 ? layout8 % module : 0);
                            layouts %= module;
                            int layout9 = NumTilings_TopDown(n, index + 1, 3, cache); // 5 + 2 -> 3
                            layouts += (layout9 != -1 ? layout9 % module : 0);
                            break;
                    }

                    cache[index, prevTile] = (layouts != 0 ? layouts % module : -1);
                }

                return cache[index, prevTile];
            }
        }
    }
}
