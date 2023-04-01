using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _154FindMinimuminRotatedSortedArrayII
    {
        public int FindMin(int[] nums)
        {
            return this.FindMinIterativelyV2(nums, 0, nums.Length - 1);
        }

        private int FindMinIterativelyV2(int[] nums, int left, int right)
        {
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] > nums[right])
                {
                    left = mid + 1;
                }
                else if (nums[mid] < nums[right])
                {
                    right = mid;
                }
                else // nums[mid] == nums[right]
                {
                    // there must be not rotation between left and mid
                    // if there is no rotation between mid and right, then the sub array is in increasing order
                    // on the other hand, if rotation happend between mid and right, without loss of generality, let's say the index of rotation is r, where mid <= r <= right
                    // then the original sub array should be like nums[x], ... , nums[right], ... , nums[left], ... , nums[mid], ...
                    // we notice there is a contradiction since nums[left] is in betwen of nums[right] and nums[mid], which means nums[right] <= nums[left] <= nums[mid]
                    // we already know nums[right] == nums[mid] AND nums[left] < num[mid], so it can't be rotated on x.
                    if (nums[left] < nums[mid])
                        right = mid - 1;
                    else // nums[left] >= nums[right] == nums[mid]
                    {
                        --right;
                    }
                }
            }

            // right == left
            return nums[left];
        }

        private int FindMinIteratively(int[] nums, int left, int right)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((left, right));
            int start = left, end = right, min = int.MaxValue;
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                start = item.Item1;
                end = item.Item2;
                while (start < end)
                {
                    int mid = start + ((end - start) >> 1);
                    if (nums[mid] < nums[end])
                        end = mid;
                    else if (nums[mid] > nums[end])
                        start = mid + 1;
                    else // nums[mid] == nums[end]
                    {
                        if (nums[start] < nums[mid])
                            end = mid - 1;
                        else
                        {
                            stack.Push((start, mid));
                            stack.Push((mid + 1, end));
                            break;
                        }
                    }
                }

                if (start == end)
                {
                    if (nums[start] < min)
                        min = nums[start];
                }
            }

            return min;
        }

        private int FindMinRecursively(int[] nums, int left, int right)
        {
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] > nums[right])
                {
                    left = mid + 1;
                }
                else if (nums[mid] < nums[right])
                {
                    right = mid;
                }
                else // nums[mid] == nums[right]
                {
                    if (nums[left] < nums[mid])
                        right = mid - 1;
                    else // nums[left] >= nums[right] == nums[mid]
                    {
                        // have to search both left and right sides
                        int val1 = this.FindMinRecursively(nums, left, mid);
                        int val2 = this.FindMinRecursively(nums, mid + 1, right);
                        return Math.Min(val1, val2);
                    }
                }
            }

            // right == left
            return nums[left];
        }
    }
}
