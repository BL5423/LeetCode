using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _64MinimumPathSum
    {
        private const int Max = int.MaxValue;

        public int MinPathSum_DP_1D(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length;
            // dp[*,j] is the minimal sum path from [*,j] to the bottom right
            int[] dp = new int[cols];
            for(int row = rows - 1; row >=0; --row)
            {
                for(int col = cols - 1; col >= 0; --col)
                {
                    int prev = dp[col];
                    dp[col] = grid[row][col];
                    if (row + 1 < rows || col + 1 < cols)
                        dp[col] += Math.Min(row + 1 < rows ? prev : Max,
                                            col + 1 < cols ? dp[col + 1] : Max);
                }
            }

            return dp[0];
        }

        public int MinPathSum_DP_2D(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length;
            // dp[i,j] is the minimal sum path from [i,j] to the bottom right
            int[,] dp = new int[rows, cols];
            
            for(int col = cols - 1; col >= 0; --col)
            {
                for(int row = rows - 1; row >= 0; --row)
                {
                    dp[row, col] = grid[row][col];
                    if (row + 1 < rows || col + 1 < cols)         
                        dp[row, col] += Math.Min(row + 1 < rows ? dp[row + 1, col] : Max,
                                                 col + 1 < cols ? dp[row, col + 1] : Max);
                }
            }

            return dp[0, 0];
        }
    }
}
