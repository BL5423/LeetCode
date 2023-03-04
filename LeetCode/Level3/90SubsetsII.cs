using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _90SubsetsII
    {
        public IList<IList<int>> SubsetsWithDup_BottomUpV2(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            int lastIndex = 0;
            for(int index = 0; index < nums.Length; ++index)
            {
                int num = nums[index];
                // if nums[index] is dup as nums[index - 1], then only append nums[index] to the subsets created with nums[index - 1] in previous iteration
                int startIndex = index > 0 && num == nums[index - 1] ? lastIndex : 0;
                lastIndex = res.Count;
                while(startIndex < lastIndex)
                {
                    var subset = res[startIndex++];
                    var newSubset = new List<int>(subset);
                    newSubset.Add(num);
                    res.Add(newSubset);
                }
            }

            return res;
        }

        public IList<IList<int>> SubsetsWithDup_BottomUpV1(int[] nums)
        {
            HashSet<string> seen = new HashSet<string>();
            Array.Sort(nums);
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            foreach(int num in nums)
            {
                var newSubsets = new List<IList<int>>(res.Count);
                foreach(var subset in res)
                {
                    if (seen.Add(string.Concat(string.Join(" ", subset), " ", num)))
                    {
                        var newSubset = new List<int>(subset);
                        newSubset.Add(num);
                        newSubsets.Add(newSubset);
                    }
                }

                foreach(var subset in newSubsets)
                {
                    res.Add(subset);
                }
            }

            return res;
        }

        public IList<IList<int>> SubsetsWithDup_BackTrackingV2(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            for (int index = 0; index < nums.Length; ++index)
            {
                if (index > 0 && nums[index] == nums[index - 1])
                    continue;

                DFSV2(index, nums, res, new LinkedList<int>());
            }

            return res;
        }

        private void DFSV2(int index, int[] nums, IList<IList<int>> res, LinkedList<int> list)
        {
            if (index == nums.Length)
            {
                res.Add(list.ToList());
            }
            else
            {
                list.AddLast(nums[index]);
                int prevNum = int.MinValue;
                for (int next = index + 1; next <= nums.Length; ++next)
                {
                    if (next < nums.Length && prevNum == nums[next])
                        continue;
                    
                    DFSV2(next, nums, res, list);
                    if (next < nums.Length)
                        prevNum = nums[next];
                }
                list.RemoveLast();
            }
        }

        public IList<IList<int>> SubsetsWithDup_BackTrackingV1(int[] nums)
        {
            HashSet<string> seen = new HashSet<string>();
            Array.Sort(nums);
            var res = new List<IList<int>>();
            res.Add(new List<int>(0));
            for(int index = 0; index < nums.Length; ++index)
            {
                DFSV1(index, nums, res, seen, new LinkedList<int>());
            }

            return res;
        }

        private void DFSV1(int index, int[] nums, IList<IList<int>> res, HashSet<string> seen, LinkedList<int> list)
        {
            if (index == nums.Length)
            {
                var l = list.ToList();
                if (seen.Add(string.Join(" ", l)))
                {
                    res.Add(l);
                }
            }
            else
            {
                list.AddLast(nums[index]);
                for(int next = index + 1; next <= nums.Length; ++next)
                {
                    DFSV1(next, nums, res, seen, list);
                }
                list.RemoveLast();
            }
        }
    }
}
