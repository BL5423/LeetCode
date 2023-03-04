using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _221MaximalSquare
    {
        public int MaximalSquare_DP_1D(char[][] matrix)
        {
            int[] dp = new int[matrix[0].Length];
            int maxLength = 0;

            for(int col = 0; col < matrix[0].Length; ++col)
            {
                dp[col] = matrix[0][col] == '1' ? 1 : 0;
                if (dp[col] == 1)
                    maxLength = 1;
            }

            for (int row = 1; row < matrix.Length; ++row)
            {
                int dp11 = dp[0];
                dp[0] = matrix[row][0] == '1' ? 1 : 0;
                for (int col = 1; col < matrix[row].Length; ++col)
                {
                    int dp10 = dp[col];       // dp[i - 1, j]
                    int dp01 = dp[col - 1];   // dp[i, j - 1]

                    if (matrix[row][col] == '1')
                    {
                        dp[col] = Math.Min(dp10, Math.Min(dp11, dp01)) + 1;
                        if (dp[col] > maxLength)
                            maxLength = dp[col];
                    }
                    else
                    {
                        dp[col] = 0;
                    }

                    // dp[i - 1, j] becomes dp[i - 1, j - 1] of next cell on the same row
                    dp11 = dp10;
                }
            }

            return maxLength * maxLength;
        }

        private Dictionary<(int, int), int> cache = new Dictionary<(int, int), int>();
        
        private int maxSquareLength = 0;

        public int MaximalSquare_TopDown(char[][] matrix)
        {
            for (int row = matrix.Length - 1; row >= 0; --row)
            {
                for(int col = matrix[row].Length -1; col >= 0; --col)
                {
                    MaximalSquare_TopDown(matrix, row, col);
                }
            }

            return maxSquareLength * maxSquareLength;
        }

        private int MaximalSquare_TopDown(char[][] matrix, int row, int col)
        {
            if (!cache.TryGetValue((row, col), out int length))
            {
                if (matrix[row][col] == '0')
                {
                    length = 0;
                }
                else
                {
                    if (row - 1 >= 0 && col - 1 >= 0)
                    {
                        var dp1 = Math.Min(MaximalSquare_TopDown(matrix, row - 1, col), MaximalSquare_TopDown(matrix, row, col - 1));
                        length = Math.Min(dp1, MaximalSquare_TopDown(matrix, row - 1, col - 1)) + 1;
                    }
                    else
                    {
                        length = 1;
                    }
                }

                cache.Add((row, col), length);
            }

            if (length > maxSquareLength)
                maxSquareLength = length;

            return length;
        }

        public int MaximalSquare_BottomUp(char[][] matrix)
        {
            int row = matrix.Length;
            int col = matrix[0].Length;

            // dp[i,j] indicates the length of square that starts at matrix[i][j](if matrix[i][j] == 1)
            int[,] dp = new int[row, col];
            int maxSquare = 0;
            for (int c = col - 1; c >= 0; --c)
            {
                for (int r = row - 1; r >= 0; --r)
                {
                    if (matrix[r][c] == '1')
                    {
                        dp[r, c] = 1;
                        if (r + 1 < row && c + 1 < col)
                        {
                            if (dp[r, c + 1] >= 1 && dp[r + 1, c] >= 1)
                            {
                                if (dp[r, c + 1] != dp[r + 1, c])
                                    dp[r, c] = Math.Min(dp[r, c + 1] + 1, dp[r + 1, c] + 1);
                                else
                                {
                                    int cornerR = r + dp[r, c + 1];
                                    int cornerC = c + dp[r, c + 1];
                                    if (dp[cornerR, cornerC] != 0)
                                        dp[r, c] = dp[r, c + 1] + 1;
                                    else
                                        dp[r, c] = dp[r, c + 1];
                                }
                            }
                        }
                    }
                    else
                    {
                        dp[r, c] = 0;
                    }

                    if (dp[r, c] > maxSquare)
                        maxSquare = dp[r, c];
                }
            }

            return maxSquare * maxSquare;
        }
    }
}
