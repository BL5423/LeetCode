using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _240Searcha2DMatrixII
    {
        public bool SearchMatrix_SearchDiagonals(int[][] matrix, int target)
        {
            int shortDim = Math.Min(matrix.Length - 1, matrix[0].Length - 1);
            for(int dim = 0; dim < shortDim; dim++)
            {
                if (Search(matrix, target, dim, vertical: true) || Search(matrix, target, dim, vertical: false))
                    return true;
            }

            return false;
        }

        private bool Search(int[][] matrix, int target, int startIndex, bool vertical)
        {
            int start = startIndex + (vertical ? 1 : 0);
            int end = vertical ? (matrix.Length - 1) : (matrix[0].Length - 1);
            while (start <= end)
            {
                int mid = start + ((end - start) >> 1);
                if (vertical)
                {
                    if (matrix[mid][startIndex] == target)
                        return true;
                    else if (matrix[mid][startIndex] < target)
                        start = mid + 1;
                    else
                        end = mid - 1;
                }
                else
                {
                    if (matrix[startIndex][mid] == target)
                        return true;
                    else if (matrix[startIndex][mid] < target)
                        start = mid + 1;
                    else
                        end = mid - 1;
                }
            }

            return false;
        }

        public bool SearchMatrix_ReduceSearchSpace(int[][] matrix, int target)
        {
            int row = matrix.Length - 1, col = 0;
            while(row >= 0 && row < matrix.Length && col >= 0 && col < matrix[0].Length)
            {
                if (matrix[row][col] == target)
                    return true;
                else if (matrix[row][col] > target)
                    --row;
                else // (matrix[row][col] < target)
                    ++col;
            }

            return false;
        }

        public bool SearchMatrix_BinarySearch(int[][] matrix, int target)
        {
            //return this.SearchMatrixRecursively(matrix, target, 0, matrix.Length - 1, 0, matrix[0].Length - 1);
            return this.SearchMatrixIteratively(matrix, target);
        }

        public bool SearchMatrixIteratively(int[][] matrix, int target)
        {
            //     sr   er   sc   ec
            Queue<(int, int, int, int)> queue = new Queue<(int, int, int, int)>();
            queue.Enqueue((0, matrix.Length - 1, 0, matrix[0].Length - 1));
            while (queue.Count != 0)
            {
                var item = queue.Dequeue();                
                int startRow = item.Item1, endRow = item.Item2, startCol = item.Item3, endCol = item.Item4;
                if (startRow > endRow || startCol > endCol)
                    continue;

                int left = startRow, right = endRow;
                while (left <= right)
                {
                    int midRow = left + ((right - left) >> 1);
                    if (matrix[midRow][startCol] <= target && target <= matrix[midRow][endCol])
                    {
                        int targetCol = this.BinarySearch(matrix[midRow], target, startCol, endCol);
                        if (matrix[midRow][targetCol] == target)
                        {
                            return true;
                        }
                        else
                        {
                            queue.Enqueue((startRow,   midRow - 1, targetCol, endCol));
                            queue.Enqueue((midRow + 1, endRow,     startCol,  targetCol - 1));
                            break;
                        }
                    }
                    else if (matrix[midRow][startCol] > target)
                    {
                        right = midRow - 1;
                    }
                    else//matrix[midRow][endCol] < target
                    {
                        left = midRow + 1;
                    }
                }
            }

            return false;
        }

        public bool SearchMatrixRecursively(int[][] matrix, int target, int startRow, int endRow, int startColumn, int endColumn)
        {
            if (startRow > endRow || startColumn > endColumn)
                return false;

            int left = startRow, right = endRow;
            while (left <= right)
            {
                int mid = left + ((right - left) / 2);
                if (matrix[mid][startColumn] <= target && target <= matrix[mid][endColumn])
                {
                    int index = this.BinarySearch(matrix[mid], target, startColumn, endColumn);
                    if (matrix[mid][index] == target)
                    {
                        return true;
                    }
                    else
                    {
                        return this.SearchMatrixRecursively(matrix, target, startRow, mid - 1, index, endColumn) ||
                               this.SearchMatrixRecursively(matrix, target, mid + 1, endRow, startColumn, index - 1);
                    }
                }
                else if (matrix[mid][startColumn] > target)
                {
                    right = mid - 1;
                }
                else // target > matrix[mid][endColumn]
                {
                    left = mid + 1;
                }
            }

            // no matched row found
            return false;
        }

        private int BinarySearch(int[] nums, int target, int start, int end)
        {
            int left = start, right = end, index = -1;
            while (left <= right)
            {
                int mid = left + ((right - left) / 2);
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] > target)
                {
                    index = mid;
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            // the first index of num that is larger than target or -1 is target is larger than all nums
            return index;
        }
    }
}
