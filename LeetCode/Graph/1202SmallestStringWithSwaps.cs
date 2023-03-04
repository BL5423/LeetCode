using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Graph
{
    public class _1202SmallestStringWithSwaps
    {
        public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var uf = new UF(s.Length);
                foreach (var pair in pairs)
                {
                    uf.Union(pair[0], pair[1]);
                }

                Dictionary<int, List<int>> groupPos = new Dictionary<int, List<int>>();
                for (int i = 0; i < s.Length; ++i)
                {
                    int parent = uf.GetParent(i);
                    if (!groupPos.TryGetValue(parent, out List<int> group))
                    {
                        group = new List<int>();
                        groupPos.Add(parent, group);
                    }

                    group.Add(i);
                }

                char[] res = new char[s.Length];
                foreach(var group in groupPos)
                {
                    int index = 0;
                    char[] subStr = new char[group.Value.Count];
                    foreach(var pos in group.Value)
                    {
                        subStr[index++] = s[pos];
                    }

                    Array.Sort(subStr);

                    for(int i = 0; i < subStr.Length; ++i)
                    {
                        res[group.Value[i]] = subStr[i];
                    }
                }

                return new string(res);
            }

            return s;
        }

        public string SmallestStringWithSwaps_V1(string s, IList<IList<int>> pairs)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var uf = new UF(s.Length);
                foreach (var pair in pairs)
                {
                    uf.Union(pair[0], pair[1]);
                }

                Dictionary<int, List<char>> groups = new Dictionary<int, List<char>>();
                for (int i = 0; i < s.Length; ++i)
                {
                    int parent = uf.GetParent(i);
                    if (!groups.TryGetValue(parent, out List<char> group))
                    {
                        group = new List<char>();
                        groups.Add(parent, group);
                    }

                    group.Add(s[i]);
                }

                Dictionary<int, int> indices = new Dictionary<int, int>(groups.Count);
                foreach (var pair in groups)
                {
                    pair.Value.Sort();
                    indices.Add(pair.Key, 0);
                }

                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; ++i)
                {
                    int parent = uf.GetParent(i);

                    sb.Append(groups[parent][indices[parent]++]);
                }

                return sb.ToString();
            }

            return s;
        }
    }

    public class UF
    {
        private int[] parents, ranks;

        public UF(int n) 
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; i++)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
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

        public int GetParent(int index) 
        {
            if (this.parents[index] == index)
                return index;

            return this.parents[index] = this.GetParent(this.parents[index]);
        }
    }
}
