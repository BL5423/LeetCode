using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _153FindMinimuminRotatedSortedArray
    {
        public int FindMinV2(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[left] <= nums[mid] && nums[left] > nums[right])
                {
                    // if mid is with the left part, then the minimal is only possible in right part(excluing mid)
                    left = mid + 1;
                }
                else // for any other cases, minial is in the left part
                {
                    right = mid;
                }
            }

            return nums[right];
        }

        public int FindMinV1(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[left] <= nums[mid] && nums[mid] <= nums[right])
                {
                    // if mid falls in between left and right, then the current array[left, ... ,right] is not rotated
                    // hence the minimal num should locate on the left side
                    right = mid;
                }
                else if (nums[mid] <= nums[right] && nums[right] <= nums[left])
                {
                    // if mid falls in the right part, then go left(including mid) to find the minimal
                    right = mid;
                }
                else // left < mid && left > right
                {
                    // if mid falls in the left part, then go right(excluding mid) to find the minimal
                    left = mid + 1;
                }
            }

            return nums[right];
        }
    }
}
