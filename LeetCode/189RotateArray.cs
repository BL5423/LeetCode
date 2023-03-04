using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _189RotateArray
    {
        public void Rotate(int[] nums, int k)
        {
            if (nums.Length > 1)
            {
                k = k % nums.Length;
                // reverse 0 -> nums.length - 1
                Reverse(nums, 0, nums.Length - 1);

                // reverse 0 -> k-1
                Reverse(nums, 0, k - 1);

                // reverse k -> nums.length - 1
                Reverse(nums, k, nums.Length - 1);

                //int startIndex = 0;
                //int currentNum = nums[startIndex];
                //int nextIndex = (startIndex + k) % nums.Length;
                //while (nextIndex != startIndex)
                //{
                //    int targetNum = nums[nextIndex];
                //    nums[nextIndex] = currentNum;
                //    currentNum = targetNum;

                //    nextIndex = (nextIndex + k) % nums.Length;
                //}

                //nums[startIndex] = currentNum;
            }
        }

        private void Reverse(int[] nums, int startIndex, int endIndex)
        {
            // reverse start -> end
            int s = startIndex, e = endIndex;
            while(s < e)
            {
                int end = nums[e];
                nums[e] = nums[s];
                nums[s] = end;
                ++s;
                --e;
            }
        }
    }
}
