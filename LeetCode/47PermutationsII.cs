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
            PermuteRecursively(nums, 0, res);
            return res;
        }

        private void PermuteRecursively(int[] nums, int index, IList<IList<int>> res)
        {
            if (index == nums.Length - 1)
            {
                var list = new List<int>(nums);
                res.Add(list);
            }
            else
            {
                HashSet<int> seen = new HashSet<int>();
                for (int i = index; i < nums.Length; ++i)
                {
                    // swap with a number we haven't seen so far
                    if (seen.Add(nums[i]))
                    {
                        int temp = nums[index];
                        nums[index] = nums[i];
                        nums[i] = temp;

                        PermuteRecursively(nums, index + 1, res);

                        nums[i] = nums[index];
                        nums[index] = temp;
                    }
                }
            }
        }

        public IList<IList<int>> PermuteUniqueV2(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>();
            var seen = new HashSet<string>();
            PermuteRecursively(nums, 0, res, seen);
            return res;
        }

        private void PermuteRecursively(int[] nums, int index, IList<IList<int>> res, HashSet<string> seen)
        {
            if (index == nums.Length - 1)
            {
                res.Add(new List<int>(nums));
            }
            else
            {
                for (int i = index; i < nums.Length; ++i)
                {
                    int temp = nums[index];
                    nums[index] = nums[i];
                    nums[i] = temp;

                    // find an unseen prefix to expand further more
                    var prefix = string.Join(",", nums.SkipLast(nums.Length - index - 1));
                    if (seen.Add(prefix))
                        PermuteRecursively(nums, index + 1, res, seen);

                    nums[i] = nums[index];
                    nums[index] = temp;
                }
            }
        }

        public IList<IList<int>> PermuteUniqueV1(int[] nums)
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
