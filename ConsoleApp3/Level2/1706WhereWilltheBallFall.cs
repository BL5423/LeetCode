using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _1706WhereWilltheBallFall
    {
        public int[] FindBall(int[][] grid)
        {
            int[] res = new int[grid[0].Length];
            for(int startCol = 0; startCol < res.Length; ++startCol)
            {
                int row = 0, col = startCol;
                while (row < grid.Length)
                {
                    if (grid[row][col] == 1)
                    {
                        // go right
                        if (col + 1 >= res.Length || grid[row][col + 1] == -1)
                        {
                            res[startCol] = -1;
                            break;
                        }

                        ++row;
                        ++col;
                    }
                    else
                    {
                        // go left
                        if (col - 1 < 0 || grid[row][col - 1] == 1)
                        {
                            res[startCol] = -1;
                            break;
                        }

                        ++row;
                        --col;
                    }
                }

                if (row >= grid.Length)
                {
                    res[startCol] = col;
                }
            }

            return res;
        }
    }
}
