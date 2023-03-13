using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _73SetMatrixZeroes
    {
        public void SetZeroes(int[][] matrix)
        {
            bool fillFirstRow = false;

            // first iteration, look for the rows to be filled with 0s and mark the corresponding columns with FLAG
            int rows = matrix.Length, cols = matrix[0].Length;
            for(int row = 0; row < rows; ++row)
            {
                for(int col = 0; col < cols; ++col)
                {
                    if (matrix[row][col] == 0)
                    {
                        matrix[row][col] = 1; // set to non-0
                        if (row == 0)
                        {
                            fillFirstRow = true;
                        }
                        else
                        {
                            matrix[row][0] = 0;
                        }

                        matrix[0][col] = 0;
                    }
                }
            }
            
            // refill each row except the first row
            for (int row = rows - 1; row > 0; --row)
            {
                if (matrix[row][0] == 0)
                {
                    for (int col = 1; col < cols; ++col)
                    {
                        matrix[row][col] = 0;
                    }
                }
            }

            // refill each column based on values of first row
            for (int col = 0; col < cols; ++col)
            {
                if (matrix[0][col] == 0)
                {
                    for(int row = 1; row < rows; ++row)
                    {
                        matrix[row][col] = 0;
                    }
                }

                // refill first row if needed
                if (fillFirstRow)
                {
                    matrix[0][col] = 0;
                }
            }
        }
    }
}
