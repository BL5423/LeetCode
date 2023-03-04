using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _45JumpGameII
    {
        private const int TRAP = 100000;

        public int Jump_BFS(int[] nums)
        {
            int jumps = 0, leftMostPos = 0, rightMostPos = 0;
            for(int i = 0; i < nums.Length - 1; ++i)
            {
                rightMostPos = Math.Max(rightMostPos, i + nums[i]);
                if (i == leftMostPos)
                {
                    // jump to next group of positions
                    ++jumps;
                    leftMostPos = rightMostPos;
                }
                if (rightMostPos > nums.Length - 1)
                    break;
            }

            return jumps;
        }

        public int Jump_DP(int[] nums)
        {
            // dp[i] is the minimal jumps it take to reach at end of nums at index i
            int[] dp = new int[nums.Length];
            // base state, no need to jump if already at the end
            dp[nums.Length - 1] = 0;
            for(int i = nums.Length - 2; i >= 0; i--)
            {
                dp[i] = TRAP;
                int steps = nums[i];
                // try each jump step
                for(int s = 1; s <= steps && i + s < nums.Length; s++)
                {
                    // avoid jump into a trap
                    if (dp[i + s] == TRAP)
                        continue;

                    dp[i] = Math.Min(dp[i], 1 + dp[i + s]);
                }
            }

            return dp[0];
        }
    }
}
