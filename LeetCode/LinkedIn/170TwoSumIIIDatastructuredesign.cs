using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class TwoSum
    {
        private Dictionary<int, int> counters;

        public TwoSum()
        {
            this.counters = new Dictionary<int, int>();
        }

        public void Add(int number)
        {
            if (this.counters.TryGetValue(number, out int count))
            {
                ++this.counters[number];
            }
            else
            {
                this.counters.Add(number, 1);
            }
        }

        public bool Find(int value)
        {
            foreach (var pair in this.counters)
            {
                int left = value - pair.Key;
                if (this.counters.TryGetValue(left, out int count))
                {
                    if (left != pair.Key || count > 1)
                        return true;
                }
            }

            return false;
        }
    }
}
