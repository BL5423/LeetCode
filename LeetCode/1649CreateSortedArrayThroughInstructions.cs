using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1649CreateSortedArrayThroughInstructions
    {
        public int CreateSortedArray(int[] instructions)
        {
            var bit = new BinaryIndexTree(100001);
            int cost = 0;
            for(int i = 0; i < instructions.Length; i++) 
            {
                // # of nums smaller than instructions[i]
                int leftCost = bit.Query(instructions[i] - 1);

                // # of nums larger than instructions[i] = total so far - # of nums smaller or equal to instructions[i]
                int rightCost = i - bit.Query(instructions[i]);
                cost += Math.Min(leftCost, rightCost);
                cost %= 1000000007;
                bit.Update(instructions[i], 1);
            }

            return cost;
        }

        public int CreateSortedArray_SegmentTree(int[] instructions)
        {
            var tree = new SegmentTreeCount(100001);
            int cost = 0;
            foreach(var instruction in instructions) 
            {
                //               # of nums in [1, instruction - 1] + # of nums in [instruction + 1, 100001]
                cost += Math.Min(tree.Query(1, instruction - 1), tree.Query(instruction + 1, 100001)) % 1000000007;
                tree.Update(instruction);
            }

            return cost;
        }

        public int CreateSortedArray_BinarySearch(int[] instructions)
        {
            List<int> res = new List<int>(instructions.Length);
            int cost = 0;
            foreach (var instruction in instructions)
            {
                int left = BinarySearchLeftwards(res, instruction);
                int right = BinarySearchRightwards(res, instruction);

                int c = Math.Min(left + 1, res.Count() - right);
                cost = (cost + c) % 1000000007;
                res.Insert(right, instruction);
            }

            return cost;
        }

        private static int BinarySearchLeftwards(IList<int> res, int target)
        {
            int left = 0, right = res.Count() - 1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                if (res[mid] >= target)
                {
                    right = mid - 1;
                }
                else // res[mid] < target
                {
                    left = mid + 1;
                }
            }

            // left > right
            return right;
        }

        private static int BinarySearchRightwards(IList<int> res, int target)
        {
            int left = 0, right = res.Count() - 1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                if (res[mid] <= target)
                {
                    left = mid + 1;
                }
                else // res[mid] > target
                {
                    right = mid - 1;
                }
            }

            // left > right
            return left;
        }
    }

    public class BinaryIndexTree
    {
        private int M;

        private int[] BIT;

        public BinaryIndexTree(int num)
        {
            this.M = num;
            this.BIT = new int[num];
        }

        public int Query(int index)
        {
            int res = 0;
            ++index;
            while (index > 0)
            {
                res += this.BIT[index];
                index -= (index & -index);
            }

            return res;
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
    }


    public class SegmentTreeCount
    {
        private int[] treeNodes;

        private int offset;

        public SegmentTreeCount(int range)
        {
            this.treeNodes = new int[range * 2];
            this.offset = range - 1;
        }

        public int Query(int num1, int num2)
        {
            if (num1 > num2)
                return 0;

            int index1 = this.offset + num1;
            int index2 = this.offset + num2;
            int totalOccurrence = 0;
            while (index1 <= index2)
            {
                if (index1 % 2 == 1)
                {
                    totalOccurrence += this.treeNodes[index1];
                    ++index1;
                }
                if (index2 % 2 == 0)
                {
                    totalOccurrence += this.treeNodes[index2];
                    --index2;
                }

                index1 /= 2;
                index2 /= 2;
            }

            return totalOccurrence;
        }

        public void Update(int num)
        {
            int index = this.offset + num;
            while (index > 0)
            {
                ++this.treeNodes[index];
                index /= 2;
            }
        }
    }
}
