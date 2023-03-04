using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _37SudokuSolver
    {
        private LinkedList<(int, int)> empties;

        private bool solved = false;

        public void SolveSudoku(char[][] board)
        {
            this.empties = new LinkedList<(int, int)>();
            for(int r = 0; r < board.Length; ++r)
            {
                for (int c = 0; c < board[r].Length; ++c)
                {
                    if (board[r][c] == '.')
                    {
                        empties.AddLast((r, c));
                    }
                }
            }

            this.SolveSudokuRecursive(board, 1);
        }

        private void SolveSudokuRecursive(char[][] board, int depth)
        {
            if (this.empties.Count > 0)
            {
                var pos = this.empties.First();
                this.empties.RemoveFirst();
                int r = pos.Item1, c = pos.Item2;
                for (char n = '1'; !this.solved && n <= '9'; ++n)
                {
                    board[r][c] = n;
                    if (ValidSudoku(board, r, c))
                    {
                        if (this.empties.Count == 0)
                        {
                            this.solved = true;
                            return;
                        }

                        SolveSudokuRecursive(board, depth + 1);
                    }
                }

                if (!this.solved)
                {
                    // undo
                    board[r][c] = '.';
                    this.empties.AddFirst(pos);
                }
            }
        }

        private static bool ValidSudoku(char[][] board, int r, int c)
        {
            for (int col = 0; col < board[r].Length; ++col)
                if (col != c && board[r][col] == board[r][c])
                    return false;

            for (int row = 0; row < board.Length; ++row)
                if (row != r && board[row][c] == board[r][c])
                    return false;

            int rGrid = r / 3 * 3, cGrid = c / 3 * 3;
            for(int row = rGrid; row < rGrid + 3; ++row)
            {
                for(int col = cGrid;  col < cGrid + 3; ++col)
                {
                    if (row != r && col != c && board[row][col] == board[r][c])
                        return false;
                }
            }

            return true;
        }
    }
}
