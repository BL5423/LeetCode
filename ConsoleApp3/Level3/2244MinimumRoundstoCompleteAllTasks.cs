using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _2244MinimumRoundstoCompleteAllTasks
    {
        public int MinimumRounds(int[] tasks)
        {
            Dictionary<int, int> counters = new Dictionary<int, int>();
            foreach(var task in tasks)
            {
                if (!counters.TryGetValue(task, out int count))
                {
                    counters.Add(task, count = 0);
                }

                ++counters[task];
            }

            int rounds = 0;
            foreach(var pair in counters)
            {
                if (pair.Value % 3 == 0)
                {
                    rounds += pair.Value / 3;
                }
                else
                {
                    int value = pair.Value;
                    int t = value / 3;
                    int r = value % 3;
                    if (r == 1 && t > 0)
                    {
                        rounds += (t - 1); // shift one '3' to remaining r to make it '4'
                        rounds += 2; // r / 2
                    }
                    else if (r == 2)
                    {
                        rounds += t;
                        rounds += 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            return rounds;
        }
    }
}
