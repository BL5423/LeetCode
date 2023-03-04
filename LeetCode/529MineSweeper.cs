using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _529MineSweeper
    {
        public char[][] UpdateBoard(char[][] board, int[] click)
        {
            int x = click[0];
            int y = click[1];
            if (board[x][y] == 'M')
            {
                board[x][y] = 'X';
            }
            else
            {
                UnrevealBFS(board, x, y);
            }

            return board;
        }

        private static int[,] dirs = { { -1, -1 }, { -1, 0 }, { -1, 1 },
                                       {  0, -1 },            {  0, 1 },
                                       {  1, -1 }, {  1, 0 }, {  1, 1 } };

        private void UnrevealRecursively(char[][] board, int x, int y)
        {
            int mines = 0;
            for(int i = 0; i < dirs.GetLength(0); ++i)
            {
                int nextX = x + dirs[i, 0];
                int nextY = y + dirs[i, 1];
                if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                    continue;

                if (board[nextX][nextY] == 'M' || board[nextX][nextY] == 'X')
                    ++mines;
            }

            if (mines > 0)
            {
                board[x][y] = (char)('0' + mines);
            }
            else
            {
                board[x][y] = 'B';
                for (int i = 0; i < dirs.GetLength(0); ++i)
                {
                    int nextX = x + dirs[i, 0];
                    int nextY = y + dirs[i, 1];
                    if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                        continue;

                    if (board[nextX][nextY] == 'E')
                    {
                        UnrevealRecursively(board, nextX, nextY);
                    }
                }
            }
        }

        private void UnrevealDFSIterative(char[][] board, int x, int y)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(x, y));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                x = top.Item1;
                y = top.Item2;
                int mines = 0;
                for (int i = 0; i < dirs.GetLength(0); ++i)
                {
                    int nextX = x + dirs[i, 0];
                    int nextY = y + dirs[i, 1];
                    if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                        continue;

                    char ch = board[nextX][nextY];
                    if (ch == 'M' || ch == 'X')
                    {
                        ++mines;
                    }
                }

                if (mines > 0)
                {
                    board[x][y] = (char)('0' + mines);
                }
                else
                {
                    board[x][y] = 'B';
                    for (int i = 0; i < dirs.GetLength(0); ++i)
                    {
                        int nextX = x + dirs[i, 0];
                        int nextY = y + dirs[i, 1];
                        if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                            continue;

                        if (board[nextX][nextY] == 'E')
                        {
                            stack.Push(new Tuple<int, int>(nextX, nextY));
                        }
                    }
                }
            }
        }

        private void UnrevealBFS(char[][] board, int x, int y)
        {
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(x, y));
            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                x = top.Item1;
                y = top.Item2;
                if (board[x][y] == 'E')
                {
                    int mines = 0;
                    for (int i = 0; i < dirs.GetLength(0); ++i)
                    {
                        int nextX = x + dirs[i, 0];
                        int nextY = y + dirs[i, 1];
                        if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                            continue;

                        char ch = board[nextX][nextY];
                        if (ch == 'M' || ch == 'X')
                        {
                            ++mines;
                        }
                    }

                    if (mines > 0)
                    {
                        board[x][y] = (char)('0' + mines);
                    }
                    else
                    {
                        board[x][y] = 'B';
                        for (int i = 0; i < dirs.GetLength(0); ++i)
                        {
                            int nextX = x + dirs[i, 0];
                            int nextY = y + dirs[i, 1];
                            if (nextX < 0 || nextX >= board.Length || nextY < 0 || nextY >= board[0].Length)
                                continue;

                            if (board[nextX][nextY] == 'E')
                            {
                                queue.Enqueue(new Tuple<int, int>(nextX, nextY));
                            }
                        }
                    }
                }
            }
        }
    }
}
