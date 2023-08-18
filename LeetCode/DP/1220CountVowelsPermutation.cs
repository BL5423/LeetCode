using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1220CountVowelsPermutation
    {
        private static int Max = 1000000007;

        public int CountVowelPermutation(int n)
        {
            long prevA = 1, prevE = 1, prevI = 1, prevO = 1, prevU = 1;
            long dpA = 0, dpE = 0, dpI = 0, dpO = 0, dpU = 0;
            for(int index = 1; index < n; ++index)
            {
                dpA = prevE % Max + prevU % Max + prevI % Max;
                dpE = prevI % Max + prevA % Max;
                dpI = prevE % Max + prevO % Max;
                dpO = prevI % Max;
                dpU = prevO % Max + prevI % Max;

                prevA = dpA;
                prevE = dpE;
                prevI = dpI;
                prevO = dpO;
                prevU = dpU;
            }

            return (int)((prevA % Max + prevE % Max + prevI % Max + prevO % Max + prevU % Max) % Max);
        }

        public int CountVowelPermutation_BottomUp(int n)
        {
            long[,] dp = new long[n , 5];
            for (int i = 0; i < 5; ++i)
                dp[n - 1, i] = 1;

            for(int index = n - 2; index >= 0; --index)
            {
                // 'a' -> 'e'
                dp[index, 0] = dp[index + 1, 1] % Max;

                // 'e' -> 'a' or 'i'
                dp[index, 1] = dp[index + 1, 0] % Max + dp[index + 1, 2] % Max;

                // 'i' -> not 'i'
                dp[index, 2] = dp[index + 1, 0] % Max + dp[index + 1, 1] % Max + dp[index + 1, 3] % Max + dp[index + 1, 4] % Max;

                // 'o' -> 'i' or 'u'
                dp[index, 3] = dp[index + 1, 2] % Max + dp[index + 1, 4] % Max;

                // 'u' -> 'a'
                dp[index, 4] = dp[index + 1, 0] % Max;
            }

            long res = 0;
            for (int i = 0; i < 5; ++i)
                res += (dp[0, i] % Max);

            return (int)(res % Max);
        }

        public int CountVowelPermutation_TopDown(int n)
        {
            long[,] cache = new long[n + 1 , 5];
            long res = CountVowelPermutation_TopDown(0, n, 'x', cache);

            return (int)(res % Max);
        }

        private long CountVowelPermutation_TopDown(int len, int n, char curChar, long[,] cache)
        {
            if (len == n)
                return 1;

            switch (curChar)
            {
                case 'a':
                    if (cache[len, 0] == 0)
                        cache[len, 0] = CountVowelPermutation_TopDown(len + 1, n, 'e', cache) % Max;
                    return cache[len, 0];

                case 'e':
                    if (cache[len, 1] == 0)
                        cache[len, 1] = CountVowelPermutation_TopDown(len + 1, n, 'a', cache) % Max +
                                        CountVowelPermutation_TopDown(len + 1, n, 'i', cache) % Max;
                    return cache[len, 1];

                case 'i':
                    if (cache[len, 2] == 0)
                        cache[len, 2] = CountVowelPermutation_TopDown(len + 1, n, 'a', cache) % Max + 
                                        CountVowelPermutation_TopDown(len + 1, n, 'e', cache) % Max +
                                        CountVowelPermutation_TopDown(len + 1, n, 'o', cache) % Max + 
                                        CountVowelPermutation_TopDown(len + 1, n, 'u', cache) % Max;
                    return cache[len, 2];

                case 'o':
                    if (cache[len, 3] == 0)
                        cache[len, 3] = CountVowelPermutation_TopDown(len + 1, n, 'i', cache) % Max +
                                        CountVowelPermutation_TopDown(len + 1, n, 'u', cache) % Max;
                    return cache[len, 3];

                case 'u':
                    if (cache[len, 4] == 0)
                        cache[len, 4] = CountVowelPermutation_TopDown(len + 1, n, 'a', cache) % Max;
                    return cache[len, 4];

                default:
                    return CountVowelPermutation_TopDown(len + 1, n, 'a', cache) % Max + 
                           CountVowelPermutation_TopDown(len + 1, n, 'e', cache) % Max +
                           CountVowelPermutation_TopDown(len + 1, n, 'i', cache) % Max + 
                           CountVowelPermutation_TopDown(len + 1, n, 'o', cache) % Max +
                           CountVowelPermutation_TopDown(len + 1, n, 'u', cache) % Max;
            }
        }
    }
}
