using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _55JumpGame
    {
        public bool CanJump(int[] nums)
        {
            // for each index, we need to know if it can reach the last left most good index on its right
            int lastLeftMostGoodIndex = nums.Length - 1; // the initial left most good index is the last index(it can always reach itself)

            for(int i = nums.Length - 2; i >= 0; --i)
            {
                // if the farest index that index i can reach is beyond the last left most good index, the index i become the new last left most index
                if (i + nums[i] >= lastLeftMostGoodIndex)
                    lastLeftMostGoodIndex = i;
            }

            return lastLeftMostGoodIndex == 0;
        }

        public bool CanJumpV1(int[] nums)
        {
            // dp[i] indicates if we can jump from nums[i] to nums[length - 1]
            // then the state transition is:
            // dp[i] = OR(dp[i + x1], dp[i + x2], ... , dp[i + xi]), where x is in [0 to nums[i]]
            bool[] dp = new bool[nums.Length];
            // base case, we can always reach the last index if we are already on it
            dp[nums.Length - 1] = true;
            for(int i = nums.Length - 2; i >= 0; --i)
            {
                int jumps = Math.Min(nums[i], nums.Length - 1 - i);
                for(int next = jumps; next > 0 && !dp[i]; --next)
                {
                    dp[i] |= dp[i + next];
                }
            }

            return dp[0];
        }
    }
}
