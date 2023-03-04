using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _78Subsets
    {
        public IList<IList<int>> Subsets(int[] nums)
        {
            var res = new List<IList<int>>();
            int n = nums.Length;
            for(int i = 0; i < Math.Pow(2, n); ++i)
            {
                var subset = new List<int>(nums.Length);
                int bit = 0;
                while (bit < nums.Length)
                {
                    if ((i & (1 << bit)) != 0)
                    {
                        subset.Add(nums[bit]);
                    }
                    ++bit;
                }

                res.Add(subset);
            }

            return res;
        }

        public IList<IList<int>> Subsets_BottomUp(int[] nums)
        {
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            foreach(var num in nums)
            {
                var newSubsets = new List<IList<int>>(res.Count);
                foreach(var subset in res)
                {
                    var s = new List<int>(subset);
                    s.Add(num);
                    newSubsets.Add(s);
                }

                foreach(var subset in newSubsets)
                {
                    res.Add(subset);
                }
            }

            return res;
        }

        public IList<IList<int>> Subsets_BackTracking(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            for (int index = 0; index < nums.Length; ++index)
            {
                DFS(index, nums, res, new LinkedList<int>());
            }

            return res;
        }

        private void DFS(int index, int[] nums, IList<IList<int>> res, LinkedList<int> list)
        {
            if (index == nums.Length)
            {
                res.Add(list.ToList());
            }
            else
            {
                list.AddLast(nums[index]);
                for (int next = index + 1; next <= nums.Length; ++next)
                {
                    DFS(next, nums, res, list);
                }
                list.RemoveLast();
            }
        }
    }
}
