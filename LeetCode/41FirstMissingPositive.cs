using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _41FirstMissingPositive
    {
        public int FirstMissingPositiveBinary(int[] nums)
        {
            int min = 1, max = nums.Max();
            if (max <= 0)
                return min;

            int res = FirstMissingPositiveRecursively(nums, min, max);
            return res == -1 ? max + 1 : res;
        }

        private int FirstMissingPositiveRecursively(int[] nums, int low, int high)
        {
            int result = -1;

            // similar to binary search
            while (low <= high)
            {
                int middle = low + ((high - low) >> 1);
                int index = Array.IndexOf(nums, middle);
                if (index > -1)
                {
                    /*
                    int r = FirstMissingPositiveRecursively(nums, low, middle - 1);
                    if (r != -1)
                        return r;

                    r = FirstMissingPositiveRecursively(nums, middle + 1, high);
                    if (r != -1)
                        return r;
                    else
                        return result;
                    */
                    index = Array.IndexOf(nums, low);
                    if (index < 0)
                    {
                        return low;
                    }

                    index = Array.IndexOf(nums, high);
                    if (index < 0)
                    {
                        if (result == -1 || result > high)
                        {
                            result = high;
                        }
                    }

                    --high;
                }
                else
                {
                    result = middle;
                    high = middle - 1;
                }
            }

            return result;
        }

        public int FirstMissingPositive(int[] nums)
        {
            int n = nums.Length;
            int index = 0;
            while (index < n)
            {
                int num = nums[index];
                int targetIndex = num - 1;
                if (targetIndex >= 0 && targetIndex < n && nums[targetIndex] != num)
                {
                    // swap
                    int r = nums[targetIndex];
                    nums[targetIndex] = num;
                    nums[index] = r;
                }
                else
                {
                    ++index;
                }
            }

            for(index = 0; index < n; ++index)
            {
                if (nums[index] != index + 1)
                    return index + 1;
            }

            return n + 1;
        }

        public int FirstMissingPositive_NegativeFlag(int[] nums)
        {
            for (int index = 0; index < nums.Length; ++index)
            {
                if (nums[index] < 0)
                    nums[index] = 0;
            }

            foreach (var num in nums)
            {
                if (num != 0)
                {
                    int value = Math.Abs(num);
                    int index = value - 1;
                    if (index < nums.Length)
                    {
                        if (nums[index] > 0)
                            nums[index] *= -1;
                        else if (nums[index] == 0)
                            nums[index] = -value;
                    }
                }
            }
            
            for (int index = 0; index < nums.Length; ++index)
            {
                if (nums[index] >= 0)
                    return index + 1;
            }

            return nums.Length + 1;
        }
    }
}
