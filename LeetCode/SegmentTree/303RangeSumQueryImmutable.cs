using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.SegmentTree
{
    public class _303RangeSumQueryImmutable
    {
        private BitSumTree bst;

        public _303RangeSumQueryImmutable(int[] nums)
        {
            this.bst = new BitSumTree(nums);
        }

        public int SumRange(int left, int right)
        {
            return this.bst.Query(right) - this.bst.Query(left - 1);
        }

        private SegmentTreeSumRange tree;

        public void _303RangeSumQueryImmutableV1(int[] nums)
        {
            this.tree = new SegmentTreeSumRange(nums);
        }

        public int SumRangeV1(int left, int right)
        {
            return this.tree.Query(left, right);
        }
    }

    public class BitSumTree
    {
        private int N;

        private int[] treeNodes;

        public BitSumTree(int[] nums)
        {
            this.N = nums.Length + 1;
            this.treeNodes = new int[N];
            for(int i = 0; i < nums.Length; ++i)
                this.Update(i, nums[i]);
        }

        public void Update(int index, int value)
        {
            ++index;
            while (index < this.N)
            {
                this.treeNodes[index] += value;
                index += (index & -index);
            }
        }

        public int Query(int index)
        {
            ++index;
            int res = 0;
            while (index > 0)
            {
                res += this.treeNodes[index];
                index -= (index & -index);
            }

            return res;
        }
    }

    public class SegmentTreeSumRange
    {
        private int[] treeNodes;

        private int offset;

        public SegmentTreeSumRange(int[] nums)
        {
            this.offset = nums.Length;
            this.treeNodes = new int[nums.Length * 2];
            this.Build(nums);
        }

        private void Build(int[] nums)
        {
            for (int i = 0; i < nums.Length; ++i)
                this.treeNodes[i + this.offset] = nums[i];

            for (int j = this.offset - 1; j > 0; --j)
                this.treeNodes[j] = this.treeNodes[j * 2] + this.treeNodes[j * 2 + 1];
        }

        public int Query(int left, int right)
        {
            left += this.offset;
            right += this.offset;
            if (left <= 0 || right <= 0)
                return 0;

            int sum = 0;
            while (left <= right)
            {
                if (left % 2 == 1)
                {
                    sum += this.treeNodes[left];
                    ++left;
                }
                if (right % 2 == 0)
                {
                    sum += this.treeNodes[right];
                    --right;
                }

                left /= 2;
                right /= 2;
            }

            return sum;
        }
    }
}
