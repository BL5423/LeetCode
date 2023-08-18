using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _410SplitArrayLargestSum
    {
        public int SplitArray(int[] nums, int k)
        {
            int left = nums.Max(), right = nums.Sum();
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                int subarrays = 1, sum = nums[0], index = 1;
                while (index < nums.Length)
                {
                    int newSum = sum + nums[index];
                    if (newSum > mid)
                    {
                        // count the subarray so far for sum and reset
                        ++subarrays;
                        sum = 0;
                    }

                    sum += nums[index++];
                }

                // too many subarrays becaue the target value(mid) is too small
                if (subarrays > k)
                {
                    left = mid + 1;
                }
                else // subarrays <= k
                {
                    right = mid;
                }
            }

            // left == right
            return right;
        }
    }
}
