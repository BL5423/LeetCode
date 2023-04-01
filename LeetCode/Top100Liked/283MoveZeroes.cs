using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _283MoveZeroes
    {
        public void MoveZeroes(int[] nums)
        {
            int lastIndex = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] != 0)
                {
                    nums[lastIndex++] = nums[i];
                    if (i >= lastIndex)
                        nums[i] = 0;
                }
            }
        }

        public void MoveZeroesV1(int[] nums)
        {
            int lastIndex = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] != 0)
                {
                    nums[lastIndex++] = nums[i];
                }
            }

            for(int i = lastIndex; i < nums.Length; ++i)
            {
                nums[i] = 0;
            }
        }
    }
}
