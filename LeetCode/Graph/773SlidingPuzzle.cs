using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Graph
{
    public class _773SlidingPuzzle
    {
        private static int[,] targetPos = new int[6, 2] { { 1, 2 }, { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 1 } };

        private static int[,] diretions = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        //https://leetcode.com/problems/shortest-path-in-binary-matrix/solutions/313347/a-search-in-python/
        public int SlidingPuzzleAStar(int[][] board)
        {
            // use A* algo to greedily search for the ultimate state based on the moves so far and potential moves
            var queue = new PriorityQueue<(int, int[][]), int>();
            var clone = this.Clone(board);
            queue.Enqueue((0, clone), 0 + this.GetPotentialMoves(clone));
            HashSet<string> seen = new HashSet<string>();
            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                var curMoves = pos.Item1;
                var curBoard = pos.Item2;
                if (this.Match(curBoard))
                    return curMoves;

                var map = string.Join("  ", curBoard.SelectMany(b => string.Join(" ", b)));
                if (!seen.Add(map))
                    continue;

                for (int r = 0; r < curBoard.Length; ++r)
                {
                    for (int c = 0; c < curBoard[r].Length; ++c)
                    {
                        int num = curBoard[r][c];
                        if (num == 0)
                        {
                            for(int d = 0; d < diretions.GetLength(0); ++d)
                            {
                                int nextR = r + diretions[d, 0];
                                int nextC = c + diretions[d, 1];
                                if (nextR < 0 || nextR >= curBoard.Length || nextC < 0 || nextC >= curBoard[r].Length)
                                    continue;

                                curBoard[r][c] = curBoard[nextR][nextC];
                                curBoard[nextR][nextC] = num;

                                if (!seen.Contains(string.Join("  ", curBoard.SelectMany(b => string.Join(" ", b)))))
                                {
                                    clone = this.Clone(curBoard);
                                    queue.Enqueue((curMoves + 1, clone), curMoves + 1 + this.GetPotentialMoves(clone));
                                }

                                curBoard[nextR][nextC] = curBoard[r][c];
                                curBoard[r][c] = num;
                            }

                            break;
                        }
                    }
                }
            }

            return -1;
        }

        public int SlidingPuzzleBFS(int[][] board)
        {
            var queue = new Queue<(int, int[][])>();
            var clone = this.Clone(board);
            queue.Enqueue((0, clone));
            HashSet<string> seen = new HashSet<string>();
            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                var curMoves = pos.Item1;
                var curBoard = pos.Item2;
                if (this.Match(curBoard))
                    return curMoves;

                var map = string.Join("  ", curBoard.SelectMany(b => string.Join(" ", b)));
                if (!seen.Add(map))
                    continue;

                for (int r = 0; r < curBoard.Length; ++r)
                {
                    for (int c = 0; c < curBoard[r].Length; ++c)
                    {
                        int num = curBoard[r][c];
                        if (num == 0)
                        {
                            for (int d = 0; d < diretions.GetLength(0); ++d)
                            {
                                int nextR = r + diretions[d, 0];
                                int nextC = c + diretions[d, 1];
                                if (nextR < 0 || nextR >= curBoard.Length || nextC < 0 || nextC >= curBoard[r].Length)
                                    continue;

                                curBoard[r][c] = curBoard[nextR][nextC];
                                curBoard[nextR][nextC] = num;

                                if (!seen.Contains(string.Join("  ", curBoard.SelectMany(b => string.Join(" ", b)))))
                                {
                                    clone = this.Clone(curBoard);
                                    queue.Enqueue((curMoves + 1, clone));
                                }

                                curBoard[nextR][nextC] = curBoard[r][c];
                                curBoard[r][c] = num;
                            }

                            break;
                        }
                    }
                }
            }

            return -1;
        }

        private int GetPotentialMoves(int[][] board)
        {
            int maxMoves = 0;
            for (int r = 0; r < board.Length; ++r)
            {
                for (int c = 0; c < board[r].Length; ++c)
                {
                    int num = board[r][c];
                    if (num == 0)
                    {
                        int moves = Math.Abs(targetPos[num, 0] - r) + Math.Abs(targetPos[num, 1] - c);
                        if (moves > maxMoves)
                            maxMoves = moves;
                    }
                }
            }

            return maxMoves;
        }

        private bool Match(int[][] board)
        {
            int moves = 0;
            for(int r = 0; r < board.Length; ++r)
            {
                for (int c = 0; c < board[r].Length; ++c)
                {
                    int num = board[r][c];
                    moves += Math.Abs(targetPos[num, 0] - r) + Math.Abs(targetPos[num, 1] - c);
                    if (moves != 0)
                        break;
                }
            }

            return moves == 0;
        }

        private int[][] Clone(int[][] board)
        {
            int[][] res = new int[board.Length][];
            for(int i =0; i < board.Length; ++i)
            {
                res[i] = new int[board[i].Length];
                for(int j = 0; j < board[i].Length;++j)
                {
                    res[i][j] = board[i][j];
                }
            }

            return res;
        }
    }
}
