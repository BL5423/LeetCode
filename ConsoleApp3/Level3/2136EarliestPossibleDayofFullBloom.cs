using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _2136EarliestPossibleDayofFullBloom
    {
        public int EarliestFullBloom(int[] plantTime, int[] growTime)
        {
            int plantingDays = 0, maxBloomTime = 0;
            var sortedGrowTimes = new PriorityQueue<int, int>(growTime.Length);
            for(int i = 0; i < growTime.Length; ++i)
            {
                // always plant the seeds with longer grow time
                // so that not only we have more time to plant other seeds
                // but also it would not be a blocker to the overall bloom time(think about the worst case: what if we plant the seed that grows slowest at last?)
                sortedGrowTimes.Enqueue(i, -growTime[i]);
            }

            while (sortedGrowTimes.Count != 0)
            {
                int sortedGrowTime = sortedGrowTimes.Dequeue();

                // spend first couple of days on planting
                plantingDays += plantTime[sortedGrowTime];

                maxBloomTime = Math.Max(maxBloomTime, plantingDays + growTime[sortedGrowTime]);
            }

            return maxBloomTime;
        }
    }
}
