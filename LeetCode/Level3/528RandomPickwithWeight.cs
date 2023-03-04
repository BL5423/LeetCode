using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class Solution
    {
        private int[] range;
        private int sum;

        private Random random = new Random((int)DateTime.Now.Ticks);

        public Solution(int[] w)
        {
            this.range = new int[w.Length];
            for(int i = 0; i < w.Length; i++) 
            {
                this.sum += w[i];
                this.range[i] = this.sum;
            }
        }

        public int PickIndex()
        {
            var r = this.random.NextDouble() * this.sum;

            // binary search to find the first index that range[index] is larger than or equal to p
            int left = 0, right = this.range.Length - 1;
            while (left < right)
            {
                int middle = left + ((right - left) >> 1);
                if (this.range[middle] >= r)
                {
                    // move to left
                    right = middle;
                }
                else
                {
                    // move to right
                    left = middle + 1;
                }
            }

            return right;
        }
    }
}
