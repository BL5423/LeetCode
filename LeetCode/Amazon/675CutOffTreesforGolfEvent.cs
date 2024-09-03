using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _675CutOffTreesforGolfEvent
    {
        public int CutOffTree(IList<IList<int>> forest)
        {
            if (forest[0][0] == 0)
                return -1;

            var indices = BFS(forest);
            if (indices == null)
                return -1;

            indices.Sort((a, b) => forest[a[0]][a[1]].CompareTo(forest[b[0]][b[1]]));
            int steps = 0;
            int[] start = new int[] { 0, 0 };
            for (int i = 0; i < indices.Count; ++i)
            {
                int[] end = indices[i];
                //int dis = BFS(start, end, forest);
                int dis = Astart(start, end, forest);
                if (dis >= 0)
                {
                    steps += dis;
                }
                else // no path found
                    return -1;

                start = end;
            }

            return steps;
        }

        public int CutOffTreeV1(IList<IList<int>> forest)
        {
            var indices = new List<int[]>();
            for (int i = 0; i < forest.Count; ++i)
            {
                for (int j = 0; j < forest[i].Count; ++j)
                {
                    if (forest[i][j] > 1)
                        indices.Add(new int[] { i, j });
                }
            }
            indices.Sort((a, b) => forest[a[0]][a[1]].CompareTo(forest[b[0]][b[1]]));

            int steps = 0;
            int[] start = new int[] { 0, 0 };
            for (int i = 0; i < indices.Count; ++i)
            {
                int[] end = indices[i];
                int dis = BFS(start, end, forest);
                if (dis >= 0)
                {
                    steps += dis;
                }
                else // no path found
                    return -1;

                start = end;
            }

            return steps;
        }

        private static int[,] dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        private static List<int[]> BFS(IList<IList<int>> forest)
        {
            var indices = new List<int[]>();
            for (int i = 0; i < forest.Count; ++i)
            {
                for (int j = 0; j < forest[i].Count; ++j)
                {
                    if (forest[i][j] > 1)
                    {
                        indices.Add(new int[] { i, j });
                    }
                }
            }

            bool[,] seen = new bool[forest.Count, forest[0].Count];
            var queue = new Queue<int[]>();
            queue.Enqueue(new int[] { 0, 0 });
            seen[0, 0] = true;
            int reachedTrees = 0;
            while (queue.Count != 0)
            {
                for (int c = queue.Count; c > 0; --c)
                {
                    var pos = queue.Dequeue();
                    if (forest[pos[0]][pos[1]] > 1)
                        ++reachedTrees;

                    for (int d = 0; d < dirs.GetLength(0); ++d)
                    {
                        int nextX = pos[0] + dirs[d, 0], nextY = pos[1] + dirs[d, 1];
                        if (nextX < 0 || nextX >= forest.Count || nextY < 0 || nextY >= forest[0].Count
                           || seen[nextX, nextY] || forest[nextX][nextY] == 0)
                            continue;

                        seen[nextX, nextY] = true;
                        queue.Enqueue(new int[] { nextX, nextY });
                    }
                }
            }

            return indices.Count == reachedTrees ? indices : null;
        }

        private static int BFS(int[] start, int[] end, IList<IList<int>> forest)
        {
            bool[,] seen = new bool[forest.Count, forest[0].Count];
            var queue = new Queue<int[]>();
            queue.Enqueue(start);
            seen[start[0], start[1]] = true;
            int layer = 0;
            bool found = false;
            while (queue.Count != 0)
            {
                for (int c = queue.Count; c > 0; --c)
                {
                    var pos = queue.Dequeue();
                    if (pos[0] == end[0] && pos[1] == end[1])
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        for (int d = 0; d < dirs.GetLength(0); ++d)
                        {
                            int nextX = pos[0] + dirs[d, 0], nextY = pos[1] + dirs[d, 1];
                            if (nextX < 0 || nextX >= forest.Count || nextY < 0 || nextY >= forest[0].Count
                               || seen[nextX, nextY] || forest[nextX][nextY] == 0)
                                continue;

                            seen[nextX, nextY] = true;
                            queue.Enqueue(new int[] { nextX, nextY });
                        }
                    }
                }

                if (!found)
                    ++layer;
            }

            return found ? layer : -1;
        }


        private static int Astart(int[] start, int[] end, IList<IList<int>> forest)
        {
            var cache = new int[forest.Count, forest[0].Count];
            var queue = new PriorityQueue<(int[], int), int>();
            queue.Enqueue((start, 0), 0 + Dis(start, start));
            cache[start[0], start[1]] = 0;
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                var pos = node.Item1;
                var dis = node.Item2;
                if (pos[0] == end[0] && pos[1] == end[1])
                {
                    return dis;
                }
                else
                {
                    for (int d = 0; d < dirs.GetLength(0); ++d)
                    {
                        int nextX = pos[0] + dirs[d, 0], nextY = pos[1] + dirs[d, 1];
                        if (nextX < 0 || nextX >= forest.Count || nextY < 0 || nextY >= forest[0].Count ||
                            forest[nextX][nextY] == 0)
                            continue;

                        var nextPos = new int[] { nextX, nextY };
                        int newDis = dis + 1 + Dis(start, nextPos);
                        if (cache[nextX, nextY] == 0 || cache[nextX, nextY] > newDis)
                        {
                            cache[nextX, nextY] = newDis;
                            queue.Enqueue((nextPos, dis + 1), newDis);
                        }
                    }
                }
            }

            return -1;
        }

        private static int Dis(int[] start, int[] pos)
        {
            // return 0; // Dijkstra
            return Math.Abs(start[0] - pos[0]) + Math.Abs(start[1] - pos[1]);
        }
    }
}
