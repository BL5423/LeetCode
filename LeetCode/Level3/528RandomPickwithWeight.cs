using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _528RandomPickwithWeight
    {
        private int sum;

        private int[][] ranges;

        public _528RandomPickwithWeight(int[] w)
        {
            int[] indices = new int[w.Length];
            this.ranges = new int[w.Length][];
            for (int i = 0; i < w.Length; ++i)
            {
                this.sum += w[i];
                indices[i] = i;
            }

            Array.Sort(indices, (a, b) => w[a] - w[b]);
            int weightSoFar = 0;
            for (int i = 0; i < indices.Length; ++i)
            {
                this.ranges[i] = new int[3];
                this.ranges[i][0] = indices[i];
                this.ranges[i][1] = weightSoFar;
                weightSoFar += w[indices[i]];
                this.ranges[i][2] = weightSoFar;
            }
        }

        public int PickIndex()
        {
            double pickedValue = new Random().NextDouble() * this.sum;
            for (int i = 0; i < this.ranges.Length; ++i)
            {
                if (this.ranges[i][1] <= pickedValue && pickedValue <= this.ranges[i][2])
                    return this.ranges[i][0];
            }

            return -1;
        }
    }

    /**
     * Your Solution object will be instantiated and called as such:
     * Solution obj = new Solution(w);
     * int param_1 = obj.PickIndex();
     */

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
