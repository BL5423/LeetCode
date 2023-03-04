using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _417PacificAtlanticWaterFlow
    {
        public IList<IList<int>> PacificAtlanticDFS(int[][] heights)
        {
            bool[,] pacific = new bool[heights.Length, heights[0].Length];
            bool[,] atlantic = new bool[heights.Length, heights[0].Length];

            for(int i = 0; i < heights[0].Length; ++i)
            {
                //DFSRercusive(0, i, heights, -1, pacific);
                //DFSRercusive(heights.Length - 1, i, heights, -1, atlantic);
                DFSIterative(0, i, heights, pacific);
                DFSIterative(heights.Length - 1, i, heights, atlantic);
            }
            for (int j = 0; j < heights.Length; ++j)
            {
                //DFSRercusive(j, 0, heights, -1, pacific);
                //DFSRercusive(j, heights[0].Length - 1, heights, -1, atlantic);
                DFSIterative(j, 0, heights, pacific);
                DFSIterative(j, heights[0].Length - 1, heights, atlantic);
            }

            var res = new List<IList<int>>();
            for(int i = 0; i < heights.Length; ++i)
            {
                for(int j = 0; j < heights[0].Length; ++j)
                {
                    if (pacific[i, j] && atlantic[i, j])
                    {
                        var point = new List<int>();
                        point.Add(i);
                        point.Add(j);
                        res.Add(point);
                    }
                }
            }

            return res;
        }

        private static int[,] directions = new int[4, 2] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

        private void DFSIterative(int l, int h, int[][] heights, bool[,] oceanMarks)
        {
            if (oceanMarks[l, h])
                return;

            oceanMarks[l, h] = true;
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((l, h));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                l = top.Item1;
                h = top.Item2;
                for(int d = 0; d < directions.GetLength(0); ++d)
                {
                    int nextL = l + directions[d, 0];
                    int nextH = h + directions[d, 1];

                    if (nextL < 0 || nextH < 0 || nextL >= heights.Length || nextH >= heights[0].Length || heights[nextL][nextH] < heights[l][h])
                        continue;

                    if (oceanMarks[nextL, nextH])
                        continue;

                    oceanMarks[nextL, nextH] = true;
                    stack.Push((nextL, nextH));
                }
            }
        }

        private void DFSRercusive(int l, int h, int[][] heights, int prev, bool[,] oceanMarks)
        {
            if (l < 0 || h < 0 || l >= heights.Length || h >= heights[0].Length || heights[l][h] < prev)
                return;

            if (oceanMarks[l, h])
                return;

            oceanMarks[l, h] = true;
            DFSRercusive(l - 1, h, heights, heights[l][h], oceanMarks);
            DFSRercusive(l + 1, h, heights, heights[l][h], oceanMarks);
            DFSRercusive(l, h - 1, heights, heights[l][h], oceanMarks);
            DFSRercusive(l, h + 1, heights, heights[l][h], oceanMarks);
        }

        public IList<IList<int>> PacificAtlanticBFS(int[][] heights)
        {
            bool[,] pacific = new bool[heights.Length, heights[0].Length];
            bool[,] atlantic = new bool[heights.Length, heights[0].Length];

            for (int i = 0; i < heights[0].Length; ++i)
            {
                BFS(0, i, heights, pacific);
                BFS(heights.Length - 1, i, heights, atlantic);
            }
            for (int j = 0; j < heights.Length; ++j)
            {
                BFS(j, 0, heights, pacific);
                BFS(j, heights[0].Length - 1, heights, atlantic);
            }

            var res = new List<IList<int>>();
            for (int i = 0; i < heights.Length; ++i)
            {
                for (int j = 0; j < heights[0].Length; ++j)
                {
                    if (pacific[i, j] && atlantic[i, j])
                    {
                        var point = new List<int>();
                        point.Add(i);
                        point.Add(j);
                        res.Add(point);
                    }
                }
            }

            return res;
        }

        private void BFS(int l, int h, int[][] heights, bool[,] oceanMarks)
        {
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(l, h));
            oceanMarks[l, h] = true;
            int[,] dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            while (queue.Count > 0)
            {
                var first = queue.Dequeue();
                l = first.Item1;
                h = first.Item2;
                for (int i = 0; i < 4; ++i)
                {
                    int nextL = l + dirs[i, 0];
                    int nextH = h + dirs[i, 1];
                    if (nextL < 0 || nextL >= heights.Length || nextH < 0 || nextH >= heights[0].Length || oceanMarks[nextL, nextH] || heights[nextL][nextH] < heights[l][h])
                        continue;

                    oceanMarks[nextL, nextH] = true;
                    queue.Enqueue(new Tuple<int, int>(nextL, nextH));
                }
            }
        }

        public IList<IList<int>> PacificAtlantic(int[][] heights)
        {
            byte[,] status = new byte[heights.Length, heights[0].Length];
            for (int l = 0; l < heights.Length; ++l)
            {
                for (int h = 0; h < heights[0].Length; ++h)
                {
                    if (status[l, h] != (byte)Status.No)
                    {
                        SetStatus(l, h, status, heights);
                    }
                }
            }

            var results = new List<IList<int>>();
            for (int l = 0; l < heights.Length; ++l)
            {
                for (int h = 0; h < heights[0].Length; ++h)
                {
                    if (status[l, h] == 3)
                    {
                        var coordinate = new List<int>(2);
                        coordinate.Add(l);
                        coordinate.Add(h);
                        results.Add(coordinate);
                    }
                }
            }

            return results;
        }

        private void SetStatus(int startW, int startH, byte[,] status, int[][] heights)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(startW, startH));
            int width = status.GetLength(0);
            int height = status.GetLength(1);
            bool[,] visited = new bool[width, height];
            bool foundPathToPacific = false, foundPathToAtlantic = false;
            visited[startW, startH] = true;
            while (stack.Count > 0)
            {
                if (foundPathToPacific && foundPathToAtlantic)
                {
                    status[startW, startH] = 3;
                    break;
                }

                var top = stack.Pop();
                int l = top.Item1;
                int h = top.Item2;
                if (l == 0 || h == 0)
                {
                    foundPathToPacific = true;
                    status[l, h] |= (byte)Status.Pacific;
                }
                if (l == width - 1 || h == height - 1)
                {
                    foundPathToAtlantic = true;
                    status[l, h] |= (byte)Status.Atlantic;
                }

                if (l - 1 >= 0 && !visited[l - 1, h] && heights[l][h] >= heights[l - 1][h])
                {
                    switch (status[l - 1, h])
                    {
                        case 3:
                            // found both
                            status[l, h] = 3;
                            foundPathToPacific = true;
                            foundPathToAtlantic = true;
                            break;

                        case (byte)Status.Pacific:
                            visited[l - 1, h] = true;
                            foundPathToPacific = true;
                            stack.Push(new Tuple<int, int>(l - 1, h));
                            break;

                        case (byte)Status.Atlantic:
                            visited[l - 1, h] = true;
                            foundPathToAtlantic = true;
                            stack.Push(new Tuple<int, int>(l - 1, h));
                            break;

                        case 0:
                            visited[l - 1, h] = true;
                            stack.Push(new Tuple<int, int>(l - 1, h));
                            break;
                    }
                }
                if (l + 1 < width && !visited[l + 1, h] && heights[l][h] >= heights[l + 1][h])
                {
                    switch (status[l + 1, h])
                    {
                        case 3:
                            // found both
                            status[l, h] = 3;
                            foundPathToPacific = true;
                            foundPathToAtlantic = true;
                            break;

                        case (byte)Status.Pacific:
                            visited[l + 1, h] = true;
                            foundPathToPacific = true;
                            stack.Push(new Tuple<int, int>(l + 1, h));
                            break;

                        case (byte)Status.Atlantic:
                            visited[l + 1, h] = true;
                            foundPathToAtlantic = true;
                            stack.Push(new Tuple<int, int>(l + 1, h));
                            break;

                        case 0:
                            visited[l + 1, h] = true;
                            stack.Push(new Tuple<int, int>(l + 1, h));
                            break;
                    }
                }
                if (h - 1 >= 0 && !visited[l, h - 1] && heights[l][h] >= heights[l][h - 1])
                {
                    switch (status[l, h - 1])
                    {
                        case 3:
                            // found both
                            status[l, h] = 3;
                            foundPathToPacific = true;
                            foundPathToAtlantic = true;
                            break;

                        case (byte)Status.Pacific:
                            visited[l, h - 1] = true;
                            foundPathToPacific = true;
                            stack.Push(new Tuple<int, int>(l, h - 1));
                            break;

                        case (byte)Status.Atlantic:
                            visited[l, h - 1] = true;
                            foundPathToAtlantic = true;
                            stack.Push(new Tuple<int, int>(l, h - 1));
                            break;

                        case 0:
                            visited[l, h - 1] = true;
                            stack.Push(new Tuple<int, int>(l, h - 1));
                            break;
                    }
                }
                if (h + 1 < height && !visited[l, h + 1] && heights[l][h] >= heights[l][h + 1])
                {
                    switch (status[l, h + 1])
                    {
                        case 3:
                            // found both
                            status[l, h] = 3;
                            foundPathToPacific = true;
                            foundPathToAtlantic = true;
                            break;

                        case (byte)Status.Pacific:
                            visited[l, h + 1] = true;
                            foundPathToPacific = true;
                            stack.Push(new Tuple<int, int>(l, h + 1));
                            break;

                        case (byte)Status.Atlantic:
                            visited[l, h + 1] = true;
                            foundPathToAtlantic = true;
                            stack.Push(new Tuple<int, int>(l, h + 1));
                            break;

                        case 0:
                            visited[l, h + 1] = true;
                            stack.Push(new Tuple<int, int>(l, h + 1));
                            break;
                    }
                }
            }

            if (foundPathToPacific && foundPathToAtlantic)
            {
                status[startW, startH] = 3;
            }
            else if (status[startW, startH] == (byte)Status.Unknown)
            {
                if (foundPathToPacific)
                {
                    status[startW, startH] = (byte)Status.Pacific;
                }
                else if (foundPathToAtlantic)
                {
                    status[startW, startH] = (byte)Status.Atlantic;
                }
                else
                {
                    status[startW, startH] = (byte)Status.No;
                }
            }
        }
    }

    public enum Status : byte
    {
        Unknown = 0,

        Pacific = 1,

        Atlantic = 2,

        No = 4,
    }
}
