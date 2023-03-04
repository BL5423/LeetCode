using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _79WordSearch
    {
        private static int[,] directions = new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };

        public bool Exist(char[][] board, string word)
        {
            for(int row = 0; row < board.Length; row++)
            {
                for(int col = 0; col < board[row].Length; col++)
                {
                    if (board[row][col] == word[0] &&
                        Search(board, row, col, word))
                        return true;
                }
            }

            return false;
        }

        private bool Search(char[][] board, int r, int c, string word)
        {
            HashSet<(int, int)> inStack = new HashSet<(int, int)>();
            Stack<Status> stack = new Stack<Status>();
            stack.Push(new Status(r, c, 0, 0));
            inStack.Add((r, c));
            while (stack.Count != 0)
            {
                var item = stack.Peek();
                if (item.strIndex == word.Length - 1)
                    return true;

                // move to next adjacent cell
                if (item.posIndex < directions.GetLength(0))
                {
                    int nextRow = item.row + directions[item.posIndex, 0];
                    int nextCol = item.col + directions[item.posIndex, 1];
                    if (!(nextRow < 0 || nextRow >= board.Length || nextCol < 0 || nextCol >= board[nextRow].Length ||
                          board[nextRow][nextCol] != word[item.strIndex + 1] ||
                          inStack.Contains((nextRow, nextCol))))
                    {
                        stack.Push(new Status(nextRow, nextCol, item.strIndex + 1, 0));
                        inStack.Add((nextRow, nextCol));
                    }

                    ++item.posIndex;
                }
                else
                {
                    // explorered all adjacent cells
                    inStack.Remove((item.row, item.col));
                    stack.Pop();
                }
            }

            return false;
        }
    }

    public class Status
    {
        public int row, col, strIndex, posIndex;

        public Status(int row, int col, int strIndex, int posIndex)
        {
            this.row = row;
            this.col = col;
            this.strIndex = strIndex;
            this.posIndex = posIndex;
        }
    }
}
