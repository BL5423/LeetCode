using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _931MinimumFallingPathSum
    {
        public int MinFallingPathSum_BottomUp_1D(int[][] matrix)
        {
            int rows = matrix.Length, cols = matrix[0].Length;
            int[] dp = new int[cols];
            for(int row = 0; row < rows; ++row)
            {
                int prevCol = int.MaxValue;
                for(int col = 0; col < cols; ++col)
                {
                    int sum = matrix[row][col] + 
                        (row > 0 ?
                         Math.Min(Math.Min(prevCol, dp[col]), col + 1 < cols ? dp[col + 1] : int.MaxValue) : 0);

                    prevCol = dp[col];
                    dp[col] = sum;
                }
            }

            return dp.Min();
        }

        public int MinFallingPathSum_TopDown(int[][] matrix)
        {
            int minSum = int.MaxValue, rows = matrix.Length, cols = matrix[0].Length;
            int[,] cache = new int[rows, cols];
            for(int col = 0; col < cols; ++col)
            {
                minSum = Math.Min(minSum, MinFallingPathSumImpl(matrix, 0, col, cache));
            }
            return minSum;
        }

        private int MinFallingPathSumImpl(int[][] matrix, int row, int col, int[,] cache)
        {
            if (col < 0 || row >= matrix.Length || col >= matrix[row].Length)
                return int.MaxValue;

            if (cache[row, col] == 0)
            {
                cache[row, col] = matrix[row][col] + 
                    (row < matrix.Length - 1 ? 
                    Math.Min(
                        Math.Min(MinFallingPathSumImpl(matrix, row + 1, col - 1, cache),
                                 MinFallingPathSumImpl(matrix, row + 1, col,     cache)),
                        MinFallingPathSumImpl(matrix, row + 1, col + 1, cache)) : 0);
            }

            return cache[row, col];
        }

        public int MinFallingPathSum_BottomUp(int[][] matrix)
        {
            int rows = matrix.Length, cols = matrix[0].Length;
            int[,] dp = new int[rows, cols];
            for(int row = 0; row < rows; ++row)
            {
                for(int col = 0; col < cols; ++col)
                {
                    dp[row, col] = matrix[row][col];
                    int above = 0;
                    if (row > 0)
                    {
                        above = dp[row - 1, col];

                        if (col > 0)
                        {
                            above = Math.Min(above, dp[row - 1, col - 1]);
                        }
                        if (col < cols - 1)
                        {
                            above = Math.Min(above, dp[row - 1, col + 1]);
                        }
                    }

                    dp[row, col] += above;
                }
            }

            int minSum = int.MaxValue;
            for(int col = 0; col < cols; ++col)
            {
                if (dp[rows - 1, col] < minSum)
                {
                    minSum = dp[rows - 1, col];
                }
            }

            return minSum;
        }
    }
}
