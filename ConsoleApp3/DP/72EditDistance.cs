using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _72EditDistance
    {
        public int MinDistance_1D(string word1, string word2)
        {
            // dp[i][j] is the min edit distance between word1[0,...,i] to word2[0,...,j]
            // we reuse dp[i-1] as the inital value of dp[i] to reduce the storage complexity
            int[] dp = new int[word2.Length + 1];
            // initialize dp[0][0,...,word2.length+1]
            for(int j = 0; j <= word2.Length; ++j)
            {
                dp[j] = j;
            }

            for (int i = 1; i <= word1.Length; ++i)
            {
                int prevI1J1 = i - 1;
                dp[0] = i;
                for (int j = 1; j <= word2.Length; ++j)
                {
                    //dp[i-1][j]
                    int i1j = dp[j];
                    //dp[i][j-1]
                    int ij1 = dp[j - 1];
                    //dp[i-1][j-1]
                    int i1j1 = prevI1J1;

                    if (word1[i - 1] != word2[j - 1])
                    {
                        dp[j] = Math.Min(Math.Min(i1j, ij1), i1j1) + 1;
                    }
                    else
                    {
                        dp[j] = i1j1;
                    }

                    prevI1J1 = i1j;
                }
            }

            return dp[word2.Length];
        }

        public int MinDistance_2DTwo1D(string word1, string word2)
        {
            int[] currentRow = new int[word2.Length + 1];
            int[] prevRow = new int[word2.Length + 1];
            for(int j = 1; j <= word2.Length; ++j)
            {
                prevRow[j] = j;
            }

            for(int i = 1; i <= word1.Length; ++i)
            {
                currentRow[0] = i;
                for(int j = 1; j <= word2.Length; ++j)
                {
                    if (word1[i - 1] != word2[j - 1])
                    {
                        var r1j1 = Math.Min(currentRow[j - 1], prevRow[j]);
                        currentRow[j] = Math.Min(r1j1, prevRow[j - 1]) + 1;
                    }
                    else
                    {
                        currentRow[j] = prevRow[j - 1];
                    }
                }

                // swap cur and prev
                var temp = currentRow;
                currentRow = prevRow;
                prevRow = temp;
            }

            return prevRow[word2.Length];
        }

        public int MinDistance_2D(string word1, string word2)
        {
            // distance[i, j] means the minimal distance between word1[0...i](the first i chars) to word2[0...j](the first j chars)
            int[,] distance = new int[word1.Length + 1, word2.Length + 1];
            for(int j = 0; j <= word2.Length; ++j)
            {
                distance[0, j] = j;
            }

            for(int i = 0; i <= word1.Length; ++i)
            {
                distance[i, 0] = i;
            }

            for (int i = 1; i <= word1.Length; ++i)
            {
                for (int j = 1; j <= word2.Length; ++j)
                {
                    if (word1[i - 1] != word2[j - 1])
                    {
                        var i1j1 = Math.Min(distance[i - 1, j], distance[i, j - 1]);
                        distance[i, j] = Math.Min(i1j1, distance[i - 1, j - 1]) + 1;
                    }
                    else
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                }
            }

            return distance[word1.Length, word2.Length];
        }
    }
}
