using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _674LongestIncreasingSubsequence
    {
        public int FindLengthOfLCIS(int[] nums)
        {
            int maxLength = 1;
            int length = 1;
            for(int index = 1; index < nums.Length; ++index)
            {
                if (nums[index - 1] < nums[index])
                {
                    ++length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
                else
                {
                    length = 1;
                }
            }

            return maxLength;
        }
    }
}
