using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _673NumberofLongestIncreasingSubsequence
    {
        public int FindNumberOfLIS_DP(int[] nums)
        {
            int[] dp = new int[nums.Length];
            int[] lens = new int[nums.Length];
            int maxLen = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                dp[i] = 1;
                lens[i] = 1;
                for (int j = i - 1; j >= 0; --j)
                {
                    if (nums[i] > nums[j])
                    {
                        if (dp[i] < dp[j] + 1)
                        {
                            dp[i] = dp[j] + 1;
                            lens[i] = 0;
                        }

                        if (dp[i] == dp[j] + 1)
                        {
                            lens[i] += lens[j];
                        }
                    }
                }

                maxLen = Math.Max(dp[i], maxLen);
            }

            int res = 0;
            for(int i = 0; i < lens.Length; ++i)
            {
                if (dp[i] == maxLen)
                    res += lens[i];
            }

            return res;
        }

        public int FindNumberOfLIS(int[] nums)
        {
            int[] sortedIndices = new int[nums.Length];
            for(int i = 0; i < sortedIndices.Length; ++i)
            {
                sortedIndices[i] = i;
            }

            Array.Sort(sortedIndices, (a, b) => nums[a]- nums[b]);

            int index = 0;
            int[] uniqueIndices = new int[nums.Length];
            for(int i = 0; i < sortedIndices.Length; ++i)
            {
                if (i > 0 && nums[sortedIndices[i]] == nums[sortedIndices[i - 1]])
                {
                    uniqueIndices[sortedIndices[i]] = index - 1;
                    continue;
                }

                uniqueIndices[sortedIndices[i]] = index++;
            }

            var tree = new Segment_Tree(index);
            Sequence sequence = null;
            for (int i = 0; i < uniqueIndices.Length; ++i)
            {
                if (i > 0 && uniqueIndices[i] == uniqueIndices[i - 1])
                {
                    tree.Update(uniqueIndices[i], new Sequence(sequence.count, sequence.length + 1));
                }
                else
                {
                    sequence = tree.Query(0, uniqueIndices[i] - 1);
                    tree.Update(uniqueIndices[i], new Sequence(sequence.count, sequence.length + 1));
                }
            }

            return tree.Query(0, index - 1).count;
        }
    }

    public class Segment_Tree
    {
        private Sequence[] treeNodes;

        private int offset;

        public Segment_Tree(int size)
        {
            this.offset = size;
            this.treeNodes = new Sequence[size * 2];
        }

        public Sequence Query(int val1, int val2)
        {
            int left = val1 + this.offset;
            int right = val2 + this.offset;
            
            Sequence res = Sequence.EMPTY;
            while (left <= right)
            {
                if (left % 2 == 1)
                {
                    res = res.Merge(this.treeNodes[left] != null ? this.treeNodes[left] : Sequence.EMPTY);
                    ++left;
                }
                if (right % 2 == 0)
                {
                    res = res.Merge(this.treeNodes[right] != null ? this.treeNodes[right] : Sequence.EMPTY);
                    --right;
                }

                left /= 2;
                right /= 2;
            }

            return res;
        }

        public void Update(int val, Sequence sequence)
        {
            int index = val + this.offset;
            this.treeNodes[index] = (this.treeNodes[index] != null ? this.treeNodes[index] : Sequence.EMPTY).Merge(sequence);

            while (index > 1)
            {
                int left = index, right = index;
                if (left % 2 == 1)
                {
                    right = left;
                    --left;
                }
                else if (right % 2 == 0)
                {
                    left = right;
                    ++right;
                }

                this.treeNodes[index / 2] = (this.treeNodes[left] != null ? this.treeNodes[left] : Sequence.EMPTY).Merge(
                    (this.treeNodes[right] != null ? this.treeNodes[right] : Sequence.EMPTY));
                index /= 2;
            }
        }
    }

    public class Sequence
    {
        public static Sequence EMPTY = new Sequence(1, 0);

        public int count, length;

        public Sequence(int count, int length)
        {
            this.count = count;
            this.length = length;
        }

        public Sequence Merge(Sequence sequence)
        {
            if (this.length == 0)
                return sequence;
            if (sequence.length == 0)
                return this;

            if (this.length > sequence.length)
                return this;
            if (this.length < sequence.length)
                return sequence;

            return new Sequence(this.count + sequence.count, this.length);
        }
    }
}
