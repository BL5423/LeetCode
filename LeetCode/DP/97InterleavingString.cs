using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _97InterleavingString
    {
        public bool IsInterleave(string s1, string s2, string s3)
        {
            int total = s1.Length + s2.Length;
            if (total != s3.Length)
                return false;
            if (s3.Length == 0)
                return true;

            if (s1.Length < s2.Length)
                return IsInterleave(s2, s1, s3);

            bool[] dp = new bool[s2.Length + 1];
            // base states
            for (int index2 = s2.Length - 1, index3 = s3.Length - 1; index2 >= 0 && s2[index2] == s3[index3]; --index2, --index3)
            {
                dp[index2] = true;
            }

            int indexOfS3 = s3.Length - 1;
            bool endWithS1 = true;
            for (int i = s1.Length - 1; i >= 0; --i)
            {
                dp[s2.Length] = false;
                if (endWithS1 && s1[i] == s3[indexOfS3--])
                    dp[s2.Length] = true;
                else
                    endWithS1 = false;

                for (int j = s2.Length - 1; j >= 0; --j)
                {
                    bool res = false;
                    int index3 = s3.Length - (s1.Length - i + s2.Length - j);
                    if (s1[i] == s3[index3])
                        res |= dp[j];
                    if (s2[j] == s3[index3])
                        res |= dp[j+1];

                    dp[j] = res;
                }
            }
            return dp[0];
        }

        public bool IsInterleave_BottomUp(string s1, string s2, string s3)
        {
            int total = s1.Length + s2.Length;
            if (total != s3.Length)
                return false;
            if (s3.Length == 0)
                return true;

            bool[,] dp = new bool[s1.Length + 1, s2.Length + 1];
            // base states
            for(int index1 = s1.Length - 1, index3 = s3.Length - 1; index1 >= 0 && s1[index1] == s3[index3]; --index1, --index3)
            {
                dp[index1, s2.Length] = true;
            }
            for(int index2 = s2.Length - 1, index3 = s3.Length - 1; index2 >= 0 && s2[index2] == s3[index3]; --index2, --index3)
            {
                dp[s1.Length, index2] = true;
            }

            for(int i = s1.Length - 1; i >= 0; --i)
            {
                for(int j = s2.Length - 1; j >= 0; --j)
                {
                    int index3 = s3.Length - (s1.Length - i + s2.Length - j);
                    if (s1[i] == s3[index3])
                        dp[i, j] |= dp[i + 1, j];
                    if (s2[j] == s3[index3])
                        dp[i, j] |= dp[i, j + 1];

                    Console.Write(dp[i, j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            return dp[0, 0];
        }

        public bool IsInterleave_TopDown(string s1, string s2, string s3)
        {
            if (s1.Length + s2.Length != s3.Length)
                return false;

            bool?[,] cache = new bool?[s1.Length + 1, s2.Length + 1];
            return IsInterleave_TopDown(s1, 0, s2, 0, s3, 0, cache);
        }

        private bool IsInterleave_TopDown(string s1, int index1, string s2, int index2, string s3, int index3, bool?[,] cache)
        {
            if (cache[index1, index2] == null)
            {
                bool res = false;
                if (index1 == s1.Length)
                {
                    res = s3.EndsWith(s2.Substring(index2));
                }
                else if (index2 == s2.Length)
                {
                    res = s3.EndsWith(s1.Substring(index1));
                }
                else
                {
                    if (s1[index1] == s3[index3])
                        res = IsInterleave_TopDown(s1, index1 + 1, s2, index2, s3, index3 + 1, cache);

                    if (!res && s2[index2] == s3[index3])
                        res = IsInterleave_TopDown(s1, index1, s2, index2 + 1, s3, index3 + 1, cache);
                }

                cache[index1, index2] = res;
            }

            return cache[index1, index2].Value;
        }
    }
}
