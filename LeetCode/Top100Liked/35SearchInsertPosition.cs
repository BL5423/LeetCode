using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _35SearchInsertPosition
    {
        // Good material of binary search: https://leetcode.com/problems/search-insert-position/solutions/423166/binary-search-101/
        public int SearchInsert(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] > target)
                {
                    // move to left
                    right = mid - 1;
                }
                else // nums[mid] < target
                {
                    // move to right
                    left = mid + 1;
                }
            }

            return left;
        }
    }
}
