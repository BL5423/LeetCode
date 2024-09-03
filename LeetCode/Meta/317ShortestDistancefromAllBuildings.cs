using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _317ShortestDistancefromAllBuildings
    {
        private static int[,] dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        public int ShortestDistanceBFSFromHouse(int[][] grid)
        {
            int width = grid.Length, height = grid[0].Length, minDis = int.MaxValue, buildings = 0;
            int[,] reachableGrid = new int[width, height];
            for (int w = 0; w < width; ++w)
            {
                for (int h = 0; h < height; ++h)
                {
                    if (grid[w][h] == 1)
                        ++buildings;

                    reachableGrid[w, h] = grid[w][h];
                }
            }

            int targetGrid = 0;
            int[,] totalDis = new int[width, height];
            int[,] reachedHouses = new int[width, height];
            for (int w = 0; w < width; ++w)
            {
                for(int h = 0; h < height; ++h)
                {
                    if (grid[w][h] == 1)
                    {
                        int steps = 1;
                        var queue = new Queue<(int, int)>();
                        queue.Enqueue((w, h));
                        while (queue.Count != 0)
                        {
                            for(int c = queue.Count; c > 0; --c)
                            {
                                var node = queue.Dequeue();

                                for(int d = 0; d < dirs.GetLength(0); ++d)
                                {
                                    int nextW = node.Item1 + dirs[d, 0];
                                    int nextH = node.Item2 + dirs[d, 1];
                                    if (nextW < 0 || nextW >= width || nextH < 0 || nextH >= height || reachableGrid[nextW, nextH] != targetGrid)
                                        continue;

                                    ++reachedHouses[nextW, nextH];
                                    totalDis[nextW, nextH] += steps;
                                    reachableGrid[nextW, nextH] = targetGrid - 1;
                                    queue.Enqueue((nextW, nextH));
                                }
                            }

                            ++steps;
                        }

                        --targetGrid;
                    }
                }
            }

            for (int w = 0; w < width; ++w)
            {
                for (int h = 0; h < height; ++h)
                {
                    if (reachedHouses[w, h] == buildings)
                    {
                        minDis = Math.Min(minDis, totalDis[w, h]);
                    }
                }
            }
                    
            return minDis != int.MaxValue ? minDis : -1;
        }

        public int ShortestDistanceBFSFromEmpty(int[][] grid)
        {
            int width = grid.Length, height = grid[0].Length, minDis = int.MaxValue, buildings = 0;
            for (int w = 0; w < width; ++w)
            {
                for (int h = 0; h < height; ++h)
                {
                    if (grid[w][h] == 1)
                        ++buildings;
                }
            }

            // HashSet<(int, int)> works but it is slower especially when width * height is large
            bool[,] globalDeadEnds = new bool[width, height];
            for (int w = 0; w < width; ++w)
            {
                for (int h = 0; h < height; ++h)
                {
                    if (grid[w][h] == 0 && !globalDeadEnds[w, h])
                    {
                        int dis = 0, buildingsFound = 0, step = 0;
                        var seen = new bool[width, height];
                        var queue = new Queue<(int, int)>();
                        queue.Enqueue((w, h));
                        seen[w, h] = true;
                        bool interrupt = false;
                        while (queue.Count != 0 && buildings != buildingsFound)
                        {
                            for (int c = queue.Count; c > 0; --c)
                            {
                                var node = queue.Dequeue();
                                if (grid[node.Item1][node.Item2] == 1)
                                {
                                    ++buildingsFound;
                                    dis += step;
                                    if (dis >= minDis)
                                    {
                                        interrupt = true;
                                        break;
                                    }
                                    continue;
                                }

                                for (int i = 0; i < dirs.GetLength(0); ++i)
                                {
                                    int nextW = node.Item1 + dirs[i, 0];
                                    int nextH = node.Item2 + dirs[i, 1];
                                    if (nextW < 0 || nextW >= width || nextH < 0 || nextH >= height ||
                                       grid[nextW][nextH] == 2 || seen[nextW, nextH] || globalDeadEnds[nextW, nextH])
                                        continue;

                                    seen[nextW, nextH] = true;
                                    queue.Enqueue((nextW, nextH));
                                }
                            }

                            ++step;
                        }

                        if (buildingsFound == buildings)
                            minDis = Math.Min(minDis, dis);
                        else if (!interrupt)
                        {
                            for (int ww = 0; ww < width; ++ww)
                            {
                                for (int hh = 0; hh < height; ++hh)
                                {
                                    if (seen[ww, hh] && grid[ww][hh] == 0)
                                        globalDeadEnds[ww, hh] = true;
                                }
                            }
                        }
                    }
                }
            }

            return minDis != int.MaxValue ? minDis : -1;
        }
    }
}
