using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _162FindPeakElement
    {
        public int FindPeakElement(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                int leftNeighbor = mid - 1 >= 0 ? nums[mid - 1] : int.MinValue;
                int rightNeighbor = mid + 1 <= nums.Length - 1 ? nums[mid + 1] : int.MinValue;

                if (leftNeighbor < nums[mid] && nums[mid] > rightNeighbor)
                {
                    return mid;
                }
                else if (leftNeighbor >= nums[mid] && nums[mid] >= rightNeighbor)
                {
                    right = mid != right ? mid : mid - 1;
                }
                else if (leftNeighbor <= nums[mid] && nums[mid] <= rightNeighbor)
                {
                    left = mid != left ? mid : mid + 1;
                }
                else
                {
                    left = mid != left ? mid : mid + 1;
                }
            }

            return left;
        }

        public int FindPeakElementV1(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                bool largerThanRight = mid == nums.Length - 1 || nums[mid] > nums[mid + 1];
                bool largerThanLeft = mid == 0 || nums[mid - 1] < nums[mid];
                if (largerThanRight && largerThanLeft)
                    return mid;
                else if (!largerThanRight)
                {
                    left = mid + 1;
                }
                else // !largerThanLeft
                {
                    right = mid;
                }
            }

            return left;
        }
    }
}
