using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.SegmentTree
{
    public class NumArray
    {
        //private SegmentTreeArrayBased segmentTree;
        private int[] nums;

        private BinaryIndexTree bit;

        public NumArray(int[] nums)
        {
            this.bit = new BinaryIndexTree(nums);
            this.nums = nums;
        }

        public void Update(int index, int val)
        {
            int diff = val - this.nums[index];
            this.nums[index] = val;

            this.bit.Update(index, diff);
        }

        public int SumRange(int left, int right)
        {
            return this.bit.Query(right) - this.bit.Query(left - 1);
        }
    }

    public class _307RangeSumQueryMutable
    {
        private SegmentTreeArrayBased segmentTree;

        public _307RangeSumQueryMutable(int[] nums)
        {
            this.segmentTree = new SegmentTreeArrayBased(nums);
        }

        public void Update(int index, int val)
        {
            this.segmentTree.Update(index, val);
        }

        public int SumRange(int left, int right)
        {
            return this.segmentTree.Query(left, right);
        }
    }

    public class BinaryIndexTree
    {
        private int[] BIT;

        private int M;

        public BinaryIndexTree(int[] nums)
        {
            this.M = nums.Length + 1;
            this.BIT = new int[this.M];
            for (int i = 0; i < nums.Length; ++i)
            {
                this.Update(i, nums[i]);
            }
        }

        public void Update(int index, int val)
        {
            ++index;
            while (index < this.M)
            {
                this.BIT[index] += val;
                index += (index & -index);
            }
        }

        public int Query(int index)
        {
            ++index;
            int res = 0;
            while (index > 0)
            {
                res += this.BIT[index];
                index -= (index & -index);
            }

            return res;
        }
    }

    public class SegmentTreeArrayBased
    {
        private int[] treeNodes;

        private int offset;

        public SegmentTreeArrayBased(int[] nums)
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

        public void Update(int index, int value)
        {
            index += this.offset;
            int delta = this.treeNodes[index] - value;
            this.treeNodes[index] = value;
            while (index > 0)
            {
                int parent = index / 2;
                this.treeNodes[parent] -= delta;

                index = parent;
            }
        }

        public void UpdateV1(int index, int value)
        {
            index += this.offset;
            this.treeNodes[index] = value;
            int left = index, right = index;
            while (left > 0)
            {
                if (left % 2 == 1) // left is not the correct left node but right
                {
                    left -= 1;
                }
                else // left % 2 == 0, which means left is the correct left node
                {
                    right += 1;
                }

                // merge
                this.treeNodes[left / 2] = this.treeNodes[left] + this.treeNodes[right];
                left /= 2;
                right = left;
            }
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

                // move to upper level
                left /= 2;
                right /= 2;
            }

            return sum;
        }
    }


    public class SegmentTree
    {
        private int[] treeNodes;

        public SegmentTree(int[] nums)
        {
            this.treeNodes = new int[nums.Length * 4];
            this.Build(0, nums, 0, nums.Length - 1);
        }

        private void Build(int treeIndex, int[] nums, int left, int right)
        {
            if (left == right)
            {
                this.treeNodes[treeIndex] = nums[left];
                return;
            }

            int mid = left + ((right - left) >> 1);
            this.Build(treeIndex * 2 + 1, nums, left, mid);
            this.Build(treeIndex * 2 + 2, nums, mid + 1, right);

            this.treeNodes[treeIndex] = this.Merge(treeIndex * 2 + 1, treeIndex * 2 + 2);
        }

        private int Merge(int treeIndex1, int treeIndex2)
        {
            return this.treeNodes[treeIndex1] + this.treeNodes[treeIndex2];
        }

        public void Update(int left, int right, int index, int newValue)
        {
            this.Update(0, left, right, index, newValue);
        }

        private void Update(int treeNodeIndex, int left, int right, int index, int newValue)
        {
            if (left == right)
            {
                this.treeNodes[treeNodeIndex] = newValue;
            }
            else
            {
                int mid = left + ((right - left) >> 1);
                if (index > mid)
                    this.Update(treeNodeIndex * 2 + 2, mid + 1, right, index, newValue);
                else // index <= mid
                    this.Update(treeNodeIndex * 2 + 1, left, mid, index, newValue);

                this.treeNodes[treeNodeIndex] = this.Merge(treeNodeIndex * 2 + 1, treeNodeIndex * 2 + 2);
            }
        }

        public int Query(int left, int right, int leftQuery, int rightQuery)
        {
            return this.Query(0, left, right, leftQuery, rightQuery);
        }

        private int Query(int treeNodeIndex, int left, int right, int leftQuery, int rightQuery)
        {
            // no overlap
            if (leftQuery > right || rightQuery < left)
                return 0;

            // query range completely covers current node
            if (leftQuery <= left && rightQuery >= right)
                return this.treeNodes[treeNodeIndex];

            int mid = left + ((right - left) >> 1);
            if (leftQuery > mid)
                return this.Query(treeNodeIndex * 2 + 2, mid + 1, right, leftQuery, rightQuery);
            else if (rightQuery <= mid)
                return this.Query(treeNodeIndex * 2 + 1, left, mid, leftQuery, rightQuery);

            int leftQueryResult = this.Query(treeNodeIndex * 2 + 1, left, mid, leftQuery, mid);
            int rightQueryResult = this.Query(treeNodeIndex * 2 + 2, mid + 1, right, mid + 1, rightQuery);

            return leftQueryResult + rightQueryResult;
        }
    }
}
