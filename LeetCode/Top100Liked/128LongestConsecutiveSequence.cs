using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _128LongestConsecutiveSequence
    {
        public int LongestConsecutive(int[] nums)
        {
            var lookup = new HashSet<int>(nums.Length);
            foreach(var num in nums)
            {
                lookup.Add(num);
            }

            int maxLen = 0;
            foreach(var num in nums)
            {
                // only inspect the first num of a potential sequence
                if (!lookup.Contains(num - 1))
                {
                    int nextNum = num + 1;
                    while (lookup.Contains(nextNum++));

                    maxLen = Math.Max(maxLen, nextNum - num - 1);
                }
            }

            return maxLen;
        }

        public int LongestConsecutive_UF(int[] nums)
        {
            int maxLen = 0;
            if (nums != null && nums.Length > 0)
            {
                Dictionary<int, int> num2Index = new Dictionary<int, int>(nums.Length);
                for (int i = 0; i < nums.Length; ++i)
                {
                    if (!num2Index.TryGetValue(nums[i], out int index))
                    {
                        num2Index.Add(nums[i], num2Index.Count);
                    }
                }
                var uf = new UnionFind(num2Index.Count);

                for (int i = 0; i < nums.Length; ++i)
                {
                    int index = num2Index[nums[i]];
                    if (num2Index.TryGetValue(nums[i] - 1, out int anotherIndex))
                    {
                        uf.Union(index, anotherIndex);
                    }

                    maxLen = Math.Max(maxLen, uf.GetRank(index));
                }
            }

            return maxLen;
        }

        public int LongestConsecutive_UFV1(int[] nums)
        {
            int maxLen = 0;
            UnionFindV1 uf = new UnionFindV1(nums.Length);
            foreach(var num in nums)
            {
                uf.AddIndex(num);
            }

            foreach (int index in uf.GetIndices())
            {
                int group = index;
                var boundary = uf.GetBoundary(group);
                int left = boundary.Item1;
                int right = boundary.Item2;

                // overlaps with any union's left boundary?
                int leftIndex = uf.GetIndexByLeft(right);
                if (leftIndex != -1)
                {
                    uf.Union(leftIndex, group);
                    group = uf.GetParent(group);
                }

                // overlaps with any union's right boundary?
                int rightIndex = uf.GetIndexByRight(left);
                if (rightIndex != -1)
                {
                    uf.Union(rightIndex, group);
                    group = uf.GetParent(group);
                }

                boundary = uf.GetBoundary(group);
                maxLen = Math.Max(maxLen, boundary.Item2 - boundary.Item1);
            }            

            return maxLen;
        }
    }

    public class UnionFindV1
    {
        private Dictionary<int, (int, int)> boundries;
        private Dictionary<int, int> leftBoundries, rightBoundries;
        private Dictionary<int, int> parents, indices;

        public void AddIndex(int num)
        {
            if (!this.indices.TryGetValue(num, out int index))
            {
                index = this.indices.Count + 1;
                this.indices.Add(num, index);
                this.boundries.Add(index, (num - 1, num));
                this.leftBoundries.Add(num - 1, index);
                this.rightBoundries.Add(num, index);
                this.parents.Add(index, index);
            }
        }

        public IEnumerable<int> GetIndices()
        {
            return indices.Values;
        }

        public (int, int) GetBoundary(int index)
        {
            return this.boundries[this.GetParent(index)];
        }

        public int GetIndexByLeft(int num)
        {
            if (this.leftBoundries.TryGetValue(num, out int index))
            {
                return index;
            }

            return -1;
        }

        public int GetIndexByRight(int num)
        {
            if (this.rightBoundries.TryGetValue(num, out int index))
            {
                return index;
            }

            return -1;
        }

        public int GetParent(int index)
        {
            if (this.parents[index] == index)
                return index;

            // path compression
            return this.parents[index] = this.GetParent(this.parents[index]);
        }

        public bool Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);
            if (parent1 == parent2)
                return false;

            var boundary1 = this.boundries[parent1];
            int left1 = boundary1.Item1;
            int right1 = boundary1.Item2;
            var boundary2 = this.boundries[parent2];
            int left2 = boundary2.Item1;
            int right2 = boundary2.Item2;
            this.boundries.Remove(parent1);
            this.boundries.Remove(parent2);
            this.leftBoundries.Remove(left1);
            this.rightBoundries.Remove(right1);
            this.leftBoundries.Remove(left2);
            this.rightBoundries.Remove(right2);

            int left = Math.Min(left1, left2);
            int right = Math.Max(right1, right2);

            if (right1 - left1 >= right2 - left2)
            {
                this.parents[parent2] = parent1;
                this.boundries.Add(parent1, (left, right));
                this.leftBoundries.Add(left, parent1);
                this.rightBoundries.Add(right, parent1);
            }
            else
            {
                this.parents[parent1] = parent2;
                this.boundries.Add(parent2, (left, right));
                this.leftBoundries.Add(left, parent2);
                this.rightBoundries.Add(right, parent2);
            }

            return true;
        }

        public UnionFindV1(int n) 
        {
            this.parents = new Dictionary<int, int>(n);
            this.indices = new Dictionary<int, int>(n);
            this.boundries = new Dictionary<int, (int, int)>(n);
            this.leftBoundries = new Dictionary<int, int>(n);
            this.rightBoundries = new Dictionary<int, int>(n);
        }
    }

    public class UnionFind
    {
        private int[] parents, ranks;

        public int GetRank(int index)
        {
            return this.ranks[this.GetParent(index)];
        }

        public int GetParentIterative(int index)
        {
            int oriIndex = index;

            // find the root parent
            while (this.parents[index] != index)
            {
                index = this.parents[index];
            }

            // update the parent chain with root parent
            while (this.parents[oriIndex] != index)
            {
                var temp = this.parents[oriIndex];
                this.parents[oriIndex] = index;
                oriIndex = temp;
            }

            return index;
        }

        public int GetParent(int index)
        {
            if (this.parents[index] == index)
            {
                return index;
            }

            return this.parents[index] = this.GetParent(this.parents[index]);
        }

        public bool Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);
            if (parent1 == parent2)
                return false;

            if (this.ranks[parent1] >= this.ranks[parent2])
            {
                this.parents[parent2] = parent1;
                this.ranks[parent1] += this.ranks[parent2];
            }
            else if (this.ranks[parent1] < this.ranks[parent2])
            {
                this.parents[parent1] = parent2;
                this.ranks[parent2] += this.ranks[parent1];
            }

            return true;
        }

        public UnionFind(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
            }
        }
    }
}
