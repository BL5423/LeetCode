using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class State
    {
        public int times;

        public int index;

        public State(int times, int index)
        {
            this.times = times;
            this.index = index;
        }
    }

    public class _78Subsets
    {
        public IList<IList<int>> Subsets(int[] nums)
        {
            var res = new List<IList<int>>();
            var list = new LinkedList<int>();
            Stack<State> stack = new Stack<State>();
            do
            {
                State item;
                if (stack.Count != 0)
                {
                    item = stack.Peek();
                }
                else
                {
                    item = new State(0, -1);
                }

                ++item.times;
                if (item.times == 1)
                {
                    // first time, do not add
                    // move to next num
                    if (item.index + 1 < nums.Length)
                        stack.Push(new State(0, item.index + 1));
                }
                else if (item.times == 2)
                {
                    // second time, add
                    list.AddLast(nums[item.index]);

                    // move to next num
                    if (item.index + 1 < nums.Length)
                        stack.Push(new State(0, item.index + 1));
                }
                else
                {
                    // last time, remove
                    list.RemoveLast();
                    stack.Pop();
                    continue;
                }

                if (item.index == nums.Length - 1)
                {
                    res.Add(list.ToList());
                }
            }
            while (stack.Count != 0);

            return res;
        }

        public IList<IList<int>> SubsetsBFS(int[] nums)
        {
            var res = new List<IList<int>>();

            // BFS
            var queue = new Queue<(int, LinkedList<int>)>();
            queue.Enqueue((-1, new LinkedList<int>()));
            while (queue.Count != 0)
            {
                var item = queue.Dequeue();
                res.Add(item.Item2.ToList());
                for (int i = item.Item1 + 1; i < nums.Length; ++i)
                {
                    var list = new LinkedList<int>(item.Item2);
                    list.AddLast(nums[i]);
                    queue.Enqueue((i, list));
                }
            }

            return res;
        }

        public IList<IList<int>> SubsetsBits(int[] nums)
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
