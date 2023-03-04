using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _47PermutationsII
    {
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var res = new List<IList<int>>();
            Array.Sort(nums);
            this.Permute(nums, 0, res);

            return res;
        }

        private void Permute(int[] nums, int startIndex, IList<IList<int>> res)
        {
            if (startIndex == nums.Length - 1)
            {
                res.Add(new List<int>(nums));
            }
            else
            {
                for (int i = startIndex; i < nums.Length; ++i)
                {
                    if (i > startIndex && nums[i] == nums[startIndex])
                        continue;

                    //swap
                    int n = nums[i];
                    nums[i] = nums[startIndex];
                    nums[startIndex] = n;

                    int[] newNums = new int[nums.Length];
                    Array.Copy(nums, newNums, nums.Length);
                    this.Permute(newNums, startIndex + 1, res);
                }
            }
        }
    }
}
