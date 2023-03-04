using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _547NumberofProvinces
    {
        public int FindCircleNum(int[][] isConnected)
        {
            int cities = isConnected.Length, connected = 0;
            var dju = new DisjointUnion(cities);
            for(int i = 0; i < isConnected.Length; ++i)
            {
                for(int j = 0; j < isConnected.Length; ++j)
                {
                    if (isConnected[i][j] == 1)
                    {
                        if (dju.Union(i, j))
                            ++connected;
                    }
                }
            }

            return cities - connected;
        }

        public int FindCircleNumV1(int[][] isConnected)
        {
            int groups = 0;
            HashSet<int> visited = new HashSet<int>(isConnected.Length);
            for(int curCity = 0; curCity < isConnected.Length; ++curCity)
            {
                if (!visited.Contains(curCity))
                {
                    ++groups;
                    visited.Add(curCity);

                    this.DFS(curCity, visited, isConnected);
                }
            }

            return groups;
        }

        private void BFS(int city, HashSet<int> visited, int[][] isConnected)
        {
            Queue<int> nextCities = new Queue<int>();
            nextCities.Enqueue(city);
            while (nextCities.Count > 0)
            {
                var curCity = nextCities.Dequeue();
                for (int nextCity = 0; nextCity < isConnected[curCity].Length; ++nextCity)
                {
                    if (isConnected[curCity][nextCity] == 1 && !visited.Contains(nextCity))
                    {
                        visited.Add(nextCity);
                        nextCities.Enqueue(nextCity);
                    }
                }
            }
        }

        private void DFS(int city, HashSet<int> visited, int[][] isConnected)
        {
            Stack<int> nextCities = new Stack<int>();
            nextCities.Push(city);
            while (nextCities.Count > 0)
            {
                var curCity = nextCities.Pop();
                for (int nextCity = 0; nextCity < isConnected[curCity].Length; ++nextCity)
                {
                    if (isConnected[curCity][nextCity] == 1 && !visited.Contains(nextCity))
                    {
                        visited.Add(nextCity);
                        nextCities.Push(nextCity);
                    }
                }
            }
        }
    }

    public class DisjointUnion
    {
        private int[] parents, ranks;

        public DisjointUnion(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
            }
        }

        public int GetParent(int index)
        {
            int parent = this.parents[index];
            if (parent != index)
                this.parents[index] = this.GetParent(parent);

            return this.parents[index];
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
            }
            else if (this.ranks[parent1] < this.ranks[parent2])
            {
                this.parents[parent1] = parent2;
            }
            else
            {
                this.parents[parent2] = parent1;
                ++this.ranks[parent1];
            }

            return true;
        }
    }
}
