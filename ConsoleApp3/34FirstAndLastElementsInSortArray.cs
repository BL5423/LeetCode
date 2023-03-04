using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _34FirstAndLastElementsInSortArray
    {
        public int[] SearchRange(int[] nums, int target)
        {
            if (nums.Length == 0)
                return new int[2] { -1, -1 };

            int[] indices = new int[2];
            indices[0] = FindMinIndex(nums, target, 0, nums.Length - 1);
            indices[1] = FindMaxIndex(nums, target, 0, nums.Length - 1);

            return indices;
        }

        private int FindMinIndex(int[] nums, int target, int start, int end)
        {
            if (nums[end] < target || nums[start] > target)
                return -1;

            if (nums[start] == target)
                return start;

            int minIndex = int.MaxValue;
            while(start <= end)
            {
                int middle = (start + end) / 2;

                if (nums[middle] == target && middle < minIndex)
                {
                    minIndex = middle;
                }

                if (nums[middle] < target)
                {
                    start = middle + 1;
                }
                else // in case nums[middle] >= target, always move left to find potential more targets
                {
                    end = middle - 1;
                }
            }

            return minIndex == int.MaxValue ? -1 : minIndex;
        }

        private int FindMaxIndex(int[] nums, int target, int start, int end)
        {
            if (nums[end] < target || nums[start] > target)
                return -1;

            if (nums[end] == target)
                return end;

            int maxIndex = int.MinValue;
            while (start <= end)
            {
                int middle = (start + end) / 2;

                if (nums[middle] == target && middle > maxIndex)
                {
                    maxIndex = middle;
                }

                if (nums[middle] > target)
                {
                    end = middle - 1;
                }
                else // in case nums[middle] <= target, move right to find the potential more targets 
                {
                    start = middle + 1;
                }
            }

            return maxIndex == int.MinValue ? -1 : maxIndex;
        }
    }
}
