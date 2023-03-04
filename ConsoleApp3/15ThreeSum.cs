using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _15ThreeSum
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var res = new List<IList<int>>();
            HashSet<int> seen = new HashSet<int>();
            HashSet<string> outputs = new HashSet<string>();
            for(int i = 0; i < nums.Length - 1; ++i)
            {
                if (seen.Add(nums[i]))
                {
                    HashSet<int> cache = new HashSet<int>();
                    for (int j = i + 1; j < nums.Length; ++j)
                    {
                        if (cache.Contains(-(nums[i] + nums[j])))
                        {
                            var list = new List<int>() { nums[i], nums[j], -(nums[i] + nums[j]) };
                            list.Sort();
                            if (outputs.Add(string.Join(" ", list)))
                            {
                                res.Add(list);
                            }
                        }

                        cache.Add(nums[j]);
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> ThreeSumV1(int[] nums)
        {
            if (nums.Length < 3)
                return null;

            var res = new List<IList<int>>(nums.Length / 3 + 1);
            HashSet<string> ids = new HashSet<string>(nums.Length / 3 + 1);
            HashSet<int> processed = new HashSet<int>(nums.Length);
            for(int index = 0; index < nums.Length - 2; ++index)
            {
                int anchor = nums[index];
                if (processed.Add(anchor))
                {
                    var complements = new HashSet<int>(nums.Length - 1);
                    for(int j = index + 1; j < nums.Length; ++j)
                    {
                        int target = 0 - (nums[j] + anchor);
                        if (complements.Contains(target))
                        {
                            var list = new List<int>(3);
                            list.Add(anchor);
                            list.Add(nums[j]);
                            list.Add(target);

                            list.Sort();
                            var id = string.Join(" ", list);
                            if (ids.Add(id))
                            {
                                res.Add(list);
                            }
                        }

                        complements.Add(nums[j]);
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> ThreeSumSort_Complement(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>(nums.Length / 3 + 1);
            for (int index = 0; index < nums.Length - 2 && nums[index] <= 0; ++index)
            {
                if (index != 0 && nums[index] == nums[index - 1])
                    continue;

                HashSet<int> complements = new HashSet<int>(nums.Length - index);
                for (int next = index + 1; next < nums.Length; ++next)
                {
                    int target = 0 - (nums[index] + nums[next]);
                    if (complements.Contains(target))
                    {
                        var list = new List<int>(3);
                        list.Add(nums[index]);
                        list.Add(nums[next]);
                        list.Add(target);
                        res.Add(list);

                        while (next + 1 < nums.Length && nums[next] == nums[next + 1])
                            ++next;
                    }

                    complements.Add(nums[next]);
                }
            }

            return res;
        }
         
        public IList<IList<int>> ThreeSumSort_TwoPointers(int[] nums)
        {
            Array.Sort(nums);
            var res = new List<IList<int>>(nums.Length / 3 + 1);
            for(int index = 0; index < nums.Length - 2 && nums[index] <= 0; ++index)
            {
                if (index > 0 && nums[index - 1] == nums[index])
                {
                    continue;
                }

                int num1 = nums[index];
                int start = index + 1, end = nums.Length - 1;
                while (start < end)
                {
                    int sum = num1 + nums[start] + nums[end];
                    if (sum > 0)
                    {
                        --end;
                    }
                    else if (sum < 0)
                    {
                        ++start;
                    }
                    else
                    {
                        var list = new List<int>(3);
                        list.Add(num1);
                        list.Add(nums[start]); 
                        list.Add(nums[end]);
                        res.Add(list);
                        ++start;
                        --end;

                        // ignore dup nums
                        //while (start < end && nums[start] == nums[start - 1])
                        //    ++start;
                        while (start < end && nums[end] == nums[end + 1])
                            --end;
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> ThreeSumNoSort(int[] nums)
        {
            HashSet<int> processed = new HashSet<int>(nums.Length / 3 + 1);
            Dictionary<int, int> complements = new Dictionary<int, int>(nums.Length);
            var res = new List<IList<int>>(nums.Length / 3 + 1);
            var ids = new HashSet<string>(nums.Length / 3 + 1);
            for(int i = 0; i < nums.Length; ++i)
            {
                if (processed.Add(nums[i]))
                {
                    for (int j = i + 1; j < nums.Length; ++j)
                    {
                        int target = 0 - (nums[i] + nums[j]);
                        if (complements.TryGetValue(target, out int index) && index == i)
                        {
                            var list = new List<int>(3);
                            list.Add(nums[i]);
                            list.Add(nums[j]);
                            list.Add(target);
                            list.Sort();
                            var id = string.Join(" ", list);
                            if (ids.Add(id))
                            {
                                res.Add(list);
                            }
                        }

                        complements[nums[j]] = i;
                    }
                }
            }

            return res;
        }
    }
}