using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _63UniquePathsII
    {
        public int UniquePathsWithObstacles_1D_V3(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length, cols = obstacleGrid[0].Length;

            // dp[r, c] means how many ways to reach on the cell (r, c)
            int[] dp = new int[Math.Min(rows, cols)];
            if (obstacleGrid[0][0] != 1)
            {
                dp[0] = 1; // base state

                if (rows < cols)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        for (int row = 0; row < rows; row++)
                        {
                            if (obstacleGrid[row][col] != 1)
                            {
                                if (row - 1 >= 0 && obstacleGrid[row - 1][col] != 1)
                                    dp[row] += dp[row - 1];
                            }
                            else
                            {
                                dp[row] = 0;
                            }
                        }
                    }
                }
                else
                {
                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            if (obstacleGrid[row][col] != 1)
                            {
                                if (col - 1 >= 0 && obstacleGrid[row][col - 1] != 1)
                                    dp[col] += dp[col - 1];
                            }
                            else
                            {
                                dp[col] = 0;
                            }
                        }
                    }
                }
            }

            return dp[Math.Min(rows, cols) - 1];
        }

        public int UniquePathsWithObstacles_1D_V2(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length, cols = obstacleGrid[0].Length;

            // dp[r, c] means how many ways to reach on the cell (r, c)
            int[] dp = new int[rows];
            if (obstacleGrid[0][0] != 1)
            {
                dp[0] = 1; // base state

                for (int col = 0; col < cols; col++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        if (obstacleGrid[row][col] != 1)
                        {
                            if (row - 1 >= 0 && obstacleGrid[row - 1][col] != 1)
                                dp[row] += dp[row - 1];
                        }
                        else
                        {
                            dp[row] = 0;
                        }
                    }
                }
            }

            return dp[rows - 1];
        }

        public int UniquePathsWithObstacles_1D(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length, cols = obstacleGrid[0].Length;

            // dp[r, c] means how many ways to reach on the cell (r, c)
            int[] dp = new int[cols];
            if (obstacleGrid[0][0] != 1)
            {
                dp[0] = 1; // base state

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (obstacleGrid[row][col] != 1)
                        {
                            if (col - 1 >= 0 && obstacleGrid[row][col - 1] != 1)
                                dp[col] += dp[col - 1];
                        }
                        else
                        {
                            dp[col] = 0;
                        }
                    }
                }
            }

            return dp[cols - 1];
        }

        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length, cols = obstacleGrid[0].Length;

            // dp[r, c] means how many ways to reach on the cell (r, c)
            int[,] dp = new int[rows, cols];
            if (obstacleGrid[0][0] != 1)
            {
                dp[0, 0] = 1; // base state

                for(int row = 0; row < rows; row++)
                {
                    for(int col = 0; col < cols; col++)
                    {
                        if (obstacleGrid[row][col] != 1)
                        {
                            if (row - 1 >= 0 && obstacleGrid[row - 1][col] != 1)
                                dp[row, col] += dp[row - 1, col];

                            if (col - 1 >= 0 && obstacleGrid[row][col - 1] != 1)
                                dp[row, col] += dp[row, col - 1];
                        }
                    }
                }
            }

            return dp[rows - 1, cols - 1];
        }
    }
}
