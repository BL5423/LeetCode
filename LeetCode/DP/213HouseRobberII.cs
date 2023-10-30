using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _213HouseRobberII
    {
        public int Rob(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];
            if (nums.Length == 2)
                return Math.Max(nums[0], nums[1]);

            int dp1 = Rob(nums, 0, nums.Length - 2);
            int dp2 = Rob(nums, 1, nums.Length - 1);

            return Math.Max(dp1, dp2);
        }

        private int Rob(int[] nums, int start, int end)
        {
            int dp1 = nums[start];
            int dp2 = 0;
            for (int i = 1; i <= end - start; ++i)
            {
                int temp = Math.Max(nums[start + i] + dp2, dp1);
                dp2 = dp1;
                dp1 = temp;
            }

            return dp1;
        }
    }
}
