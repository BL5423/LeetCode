using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Index
    {
        public int val;

        public bool seen = false;

        public int depth = 0;

        public int changes = 0;
    }

    public class _46Permutations
    {
        public IList<IList<int>> PermuteDFS(int[] nums)
        {
            HashSet<int> indicesInStack = new HashSet<int>();
            var res = new List<IList<int>>();
                Stack<Index> stack = new Stack<Index>();
                stack.Push(new Index() { val = 0, depth = 1 });
                indicesInStack.Add(0);
            while (stack.Count != 0)
            {
                Index index = stack.Peek();
                if (indicesInStack.Count == nums.Length)
                {
                    res.Add(indicesInStack.Select(v => nums[v]).ToList());

                    var removedIndex = stack.Pop();
                    indicesInStack.Remove(removedIndex.val);
                }
                else
                {
                    if (index.seen)
                    {                        
                        if (index.changes + index.depth == nums.Length)
                        {
                            var removedIndex = stack.Pop();
                            indicesInStack.Remove(removedIndex.val);
                            continue;
                        }
                        else
                        {
                            ++index.changes;
                            int nextIndex = index.val;
                            while (nextIndex < nums.Length && indicesInStack.Contains(nextIndex))
                                ++nextIndex;

                            indicesInStack.Remove(index.val);
                            index.val = nextIndex;
                            indicesInStack.Add(index.val);
                        }
                    }

                    index.seen = true;
                    for (int j = 0; j < nums.Length; ++j)
                    {
                        if (indicesInStack.Contains(j))
                            continue;

                        indicesInStack.Add(j);
                        stack.Push(new Index() { val = j, depth = index.depth + 1 });
                        break;
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> PermuteBFS(int[] nums)
        {
            Queue<HashSet<int>> queue = new Queue<HashSet<int>>();
            for(int index = 0; index < nums.Length; ++index)
            {
                var set = new HashSet<int>(nums.Length);
                set.Add(nums[index]);
                queue.Enqueue(set);
            }

            var res = new List<IList<int>>();
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                if (head.Count == nums.Length)
                {
                    res.Add(head.ToArray());
                }
                else
                {
                    for (int i = 0; i < nums.Length; ++i)
                    {
                        if (!head.Contains(nums[i]))
                        {
                            var next = new HashSet<int>(head);
                            next.Add(nums[i]);
                            queue.Enqueue(next);
                        }
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> PermuteV2(int[] nums)
        {
            bool[] used = new bool[nums.Length];
            var res = new List<IList<int>>();
            for(int index = 0; index < nums.Length; ++index)
            {
                used[index] = true;
                var permutation = new LinkedList<int>();
                permutation.AddLast(nums[index]);
                PermuteRecursively(nums, index, used, permutation, res);
                used[index] = false;
            }

            return res;
        }

        private void PermuteRecursively(int[] nums, int index, bool[] used, LinkedList<int> permutation, IList<IList<int>> res)
        {
            if (permutation.Count == nums.Length)
            {
                res.Add(permutation.ToArray());
            }
            else
            {
                for(int i = 0; i < nums.Length; ++i)
                {
                    if (used[i])
                        continue;

                    used[i] = true;
                    permutation.AddLast(nums[i]);
                    PermuteRecursively(nums, i, used, permutation, res);
                    permutation.RemoveLast();
                    used[i] = false;
                }
            }
        }

        public IList<IList<int>> PermuteV1(int[] nums)
        {
            var results = new List<IList<int>>();
            Permute(nums, 0, nums.Length - 1, results);

            return results;
        }

        private void Permute(int[] nums, int start, int end, IList<IList<int>> results)
        {
            if (start == end)
            {
                results.Add(nums.ToList());
            }
            else
            {
                for (int index = start; index <= end; ++index)
                {
                    // swap start and index
                    int startValue = nums[start];
                    nums[start] = nums[index];
                    nums[index] = startValue;

                    // calculate all permutes of sub array
                    Permute(nums, start + 1, end, results);

                    // restore start and index
                    startValue = nums[start];
                    nums[start] = nums[index];
                    nums[index] = startValue;
                }
            }
        }
    }
}
