using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _200NumberOfIslands
    {
        public int NumIslands(char[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            int islands = rows * cols;
            var unifiedGroup = new UnifiedGroup(islands);
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    if (grid[r][c] == '1')
                    {
                        int index = r * cols + c;
                        if (r > 0 && grid[r - 1][c] == '1')
                        {
                            // above
                            if (unifiedGroup.Union(index, index - cols))
                                --islands;
                        }
                        if (c > 0 && grid[r][c - 1] == '1')
                        {
                            // left
                            if (unifiedGroup.Union(index, index - 1))
                                --islands;
                        }
                    }
                    else
                    {
                        --islands;
                    }
                }
            }

            return islands;
        }

        private static int[,] dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        public int NumIsLandsBFS(char[][] grid)
        {
            int islands = 0;
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < grid.Length; ++i)
            {
                for(int j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] == '1')
                    {
                        ++islands;
                        grid[i][j] = '2';
                        queue.Enqueue(i);
                        queue.Enqueue(j);
                        while (queue.Count > 0)
                        {
                            int iq = queue.Dequeue();
                            int jq = queue.Dequeue();
                            for (int d = 0; d < dirs.GetLength(0); ++d)
                            {
                                int nextI = iq + dirs[d, 0];
                                int nextJ = jq + dirs[d, 1];
                                if (nextI < 0 || nextI >= grid.Length || nextJ < 0 || nextJ >= grid[i].Length || grid[nextI][nextJ] != '1')
                                    continue;

                                grid[nextI][nextJ] = '2';
                                queue.Enqueue(nextI);
                                queue.Enqueue(nextJ);
                            }
                        }
                    }
                }
            }

            return islands;
        }

        public int NumIslandsDFS(char[][] grid)
        {
            int numbers = 0;
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            for(int l = 0; l < grid.Length; ++l)
            {
                for (int h = 0; h < grid[l].Length; ++h)
                {
                    if (grid[l][h] == '1')
                    {
                        ++numbers;
                        stack.Push(new Tuple<int, int>(l, h));
                        while (stack.Count > 0)
                        {
                            var pos = stack.Pop();
                            grid[pos.Item1][pos.Item2] = '2';
                            if (pos.Item1 - 1 >= 0 && grid[pos.Item1 - 1][pos.Item2] == '1')
                            {
                                stack.Push(new Tuple<int, int>(pos.Item1 - 1, pos.Item2));
                            }
                            if (pos.Item1 + 1 < grid.Length && grid[pos.Item1 + 1][pos.Item2] == '1')
                            {
                                stack.Push(new Tuple<int, int>(pos.Item1 + 1, pos.Item2));
                            }
                            if (pos.Item2 - 1 >= 0 && grid[pos.Item1][pos.Item2 - 1] == '1')
                            {
                                stack.Push(new Tuple<int, int>(pos.Item1, pos.Item2 - 1));
                            }
                            if (pos.Item2 + 1 < grid[pos.Item1].Length && grid[pos.Item1][pos.Item2 + 1] == '1')
                            {
                                stack.Push(new Tuple<int, int>(pos.Item1, pos.Item2 + 1));
                            }
                        }
                    }
                }
            }

            return numbers;
        }

        public int NumIslandsV1(char[][] grid)
        {
            int[,] map = new int[grid.Length, grid[0].Length];
            
            int number = 0;
            for(int l = 0; l < grid.Length; ++l)
            {
                for(int h = 0; h < grid[l].Length; ++h)
                {
                    if (grid[l][h] != '0')
                    {
                        if (map[l, h] == 0)
                        {
                            map[l, h] = ++number;
                        }

                        // try to render right and bottom
                        if (h + 1 < grid[l].Length && grid[l][h + 1] != '0')
                        {
                            map[l, h + 1] = map[l, h];
                        }
                        if (l + 1 < grid.Length && grid[l + 1][h] != '0')
                        {
                            map[l + 1, h] = map[l, h];
                        }
                    }
                }
            }

            Dictionary<int, HashSet<int>> groups = new Dictionary<int, HashSet<int>>();
            for (int l = 0; l < grid.Length; ++l)
            {
                for (int h = 0; h < grid[l].Length; ++h)
                {
                    if (map[l, h] != 0)
                    {
                        if (!groups.TryGetValue(map[l, h], out HashSet<int> set))
                        {
                            set = new HashSet<int>();
                            set.Add(map[l, h]);
                            groups.Add(map[l, h], set);
                        }

                        if (h + 1 < grid[l].Length && map[l, h + 1] != 0 && map[l, h + 1] != map[l, h])
                        {
                            if (!groups.TryGetValue(map[l, h + 1], out HashSet<int> bottomGroup))
                            {
                                set.Add(map[l, h + 1]);
                                groups.Add(map[l, h + 1], set);
                            }
                            else
                            {
                                // merge
                                foreach (int group in bottomGroup)
                                {
                                    set.Add(group);
                                }
                                foreach (int group in set)
                                {
                                    groups[group] = set;
                                }
                            }
                        }

                        if (l + 1 < grid.Length && map[l + 1, h] != 0 && map[l + 1, h] != map[l ,h])
                        {
                            if (!groups.TryGetValue(map[l + 1, h], out HashSet<int> rightGroup))
                            {
                                set.Add(map[l + 1, h]);
                                groups.Add(map[l + 1, h], set);
                            }
                            else
                            {
                                // merge
                                foreach (int group in rightGroup)
                                {
                                    set.Add(group);
                                }
                                foreach(int group in set)
                                {
                                    groups[group] = set;
                                }
                            }
                        }
                    }
                }
            }

            return groups.Values.Distinct().Count();
        }
    }


    public class UnifiedGroup
    {
        private int[] ranks, parents;

        public UnifiedGroup(int num)
        {
            this.ranks = new int[num];
            this.parents = new int[num];
            for (int i = 0; i < num; ++i)
            {
                this.ranks[i] = 1;
                this.parents[i] = i;
            }
        }

        public bool Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);
            if (parent1 == parent2)
                return false;

            if (this.ranks[parent1] > this.ranks[parent2])
            {
                this.parents[parent2] = parent1;
                this.ranks[parent1] += this.ranks[parent2];
            }
            else if (this.ranks[parent1] < this.ranks[parent2])
            {
                this.parents[parent1] = parent2;
                this.ranks[parent2] += this.ranks[parent1];
            }
            else
            {
                this.parents[parent2] = parent1;
                this.ranks[parent1] += this.ranks[parent2];
            }

            return true;
        }

        private int GetParent(int index)
        {
            int parent = this.parents[index];
            if (parent == index)
                return parent;

            return this.parents[index] = this.GetParent(parent);
        }
    }
}