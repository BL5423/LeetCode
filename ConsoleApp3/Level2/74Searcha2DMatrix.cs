using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _74Searcha2DMatrix
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rows = matrix.Length, cols = matrix[0].Length;
            int start = 0, end = rows * cols - 1;
            while (start <= end)
            {
                int middle = start + ((end - start) >> 1);
                int row = middle / cols;
                int col = middle % cols;
                int num = matrix[row][col];
                if (num == target)
                {
                    return true;
                }
                else if (num > target)
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }

            return false;
        }

        public bool SearchMatrixV1(int[][] matrix, int target)
        {
            // binary search the potential rows
            int rows = matrix.Length, row = -1;
            int firstRow = 0, lastRow = rows - 1;
            while (firstRow <= lastRow)
            {
                int middleRow = firstRow + ((lastRow - firstRow) >> 1);
                if (matrix[middleRow][0] > target)
                {
                    lastRow = middleRow - 1;
                }
                else
                {
                    if ((middleRow + 1 <= rows - 1 && matrix[middleRow + 1][0] > target) ||
                        matrix[middleRow][matrix[middleRow].Length - 1] >= target)
                    {
                        row = middleRow;
                        break;
                    }

                    firstRow = middleRow + 1;
                }
            }

            if (row != -1)
            {
                // once found the target row, binary search within that row
                int cols = matrix[row].Length;
                int firstCol = 0, lastCol = cols - 1;
                while (firstCol <= lastCol)
                {
                    int middleCol = firstCol + ((lastCol - firstCol) >> 1);
                    int num = matrix[row][middleCol];
                    if (num == target)
                    {
                        return true;
                    }
                    else if (num > target)
                    {
                        lastCol = middleCol - 1;
                    }
                    else
                    {
                        firstCol = middleCol + 1;
                    }
                }
            }

            return false;
        }
    }
}
