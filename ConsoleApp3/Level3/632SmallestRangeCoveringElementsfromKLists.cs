using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _632SmallestRangeCoveringElementsfromKLists
    {
        public int[] SmallestRangeV2_LocalSort(IList<IList<int>> nums)
        {
            int[] range = new int[2] { -1000000, 1000000 };
            int[] nexts = new int[nums.Count];
            int maxNum = int.MinValue;
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
            for(int i = 0; i < nums.Count; ++i)
            {
                // push all the first nums of all lists into the queue
                nexts[i] = 0;
                maxNum = Math.Max(maxNum, nums[i][0]);
                queue.Enqueue(i, nums[i][0]);
            }

            while (queue.Count != 0)
            {
                // pop up the list whose current num is minimal currently
                int minNumGroup = queue.Dequeue();
                int minNum = nums[minNumGroup][nexts[minNumGroup]];
                if (maxNum - minNum < range[1] - range[0])
                {
                    range[0] = minNum;
                    range[1] = maxNum;
                }

                if (nexts[minNumGroup] < nums[minNumGroup].Count - 1)
                {
                    // move to the next num in the poped-up list
                    int nextNum = nums[minNumGroup][++nexts[minNumGroup]];
                    queue.Enqueue(minNumGroup, nextNum);

                    // the nextNum could be larger than current maxNum, so try to update maxNum if possible
                    maxNum = Math.Max(maxNum, nextNum);
                }
                else
                {
                    // return if run out of any list
                    break;
                }
            }

            return range;
        }

        public int[] SmallestRangeV1_GlobalSort(IList<IList<int>> nums)
        {
            int[] range = new int[2] { -1000000, 1000000 };
            int totalGroups = nums.Count;
            var queue = new PriorityQueue<(int, int), int>();
            for(int group = 0; group < nums.Count; ++group)
            {
                foreach(var num in nums[group])
                {
                    queue.Enqueue((group, num), num);
                }
            }

            List<(int, int)> global = new List<(int, int)>(queue.Count);
            while (queue.Count != 0)
            {
                global.Add(queue.Dequeue());
            }

            int leftIndex = 0, rightIndex = -1;
            int currentGroups = 0;
            int[] groupCounts = new int[totalGroups];
            while (leftIndex < global.Count)
            {
                if (currentGroups == totalGroups)
                {
                    // check the current range
                    int leftValue = global[leftIndex].Item2;
                    int rightValue = global[rightIndex].Item2;
                    if (rightValue - leftValue < range[1] - range[0])
                    {
                        range[0] = leftValue;
                        range[1] = rightValue;
                    }

                    // Fade out the left-most num
                    var current = global[leftIndex++];
                    if (--groupCounts[current.Item1] == 0)
                    {
                        --currentGroups;
                    }
                }
                else // currentGroups < totalGroups
                {
                    if (rightIndex + 1 == global.Count)
                        break;

                    // Adding new num
                    var next = global[++rightIndex];
                    if (groupCounts[next.Item1]++ == 0)
                    {
                        ++currentGroups;
                    }
                }
            }

            return range;
        }
    }
}
