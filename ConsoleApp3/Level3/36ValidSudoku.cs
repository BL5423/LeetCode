using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _36ValidSudoku
    {
        public bool IsValidSudokuV2(char[][] board)
        {
            HashSet<string> seen = new HashSet<string>();
            for(int r = 0; r < board.Length; ++r)
            {
                for(int c = 0; c < board[r].Length; ++c)
                {
                    char num = board[r][c];
                    if (num != '.')
                    {
                        if (!seen.Add(string.Format("{0} in row {1}", num, r)) ||
                            !seen.Add(string.Format("{0} in col {1}", num, c)) ||
                            !seen.Add(string.Format("{0} on sub board {1} {2}", num, r / 3, c / 3)))
                            return false;
                    }
                }
            }

            return true;
        }
        
        public bool IsValidSudokuV1(char[][] board)
        {
            // check each row
            for(int r = 0; r < board.Length; ++r)
            {
                int[] count = new int[9];
                char[] row = board[r];
                foreach(var num in row)
                {
                    if (num != '.' && ++count[num - '1'] > 1)
                        return false;
                }
            }

            // check each column
            for(int c = 0; c < board[0].Length; ++c)
            {
                int[] count = new int[9];
                for (int r = 0; r < board.Length; ++r)
                {
                    char num = board[r][c];
                    if (num != '.' && ++count[num - '1'] > 1)
                        return false;
                }
            }

            // check each sub board
            for(int r = 0; r < board.Length; r+=3)
            {
                for(int c = 0; c < board[r].Length; c+=3)
                {
                    int[] count = new int[9];
                    for (int row = r; row < r + 3; ++row)
                    {
                        for(int col = c; col < c + 3; ++col)
                        {
                            char num = board[row][col];
                            if (num != '.' && ++count[num - '1'] > 1)
                                return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
