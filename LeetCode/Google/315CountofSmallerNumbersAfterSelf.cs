using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _315CountofSmallerNumbersAfterSelf
    {
        private const int offset = 10000;

        public IList<int> CountSmaller(int[] nums)
        {
            var tree = new BIT(offset * 2 + 2);
            var res = new int[nums.Length];
            for (int i = nums.Length - 1; i >= 0; --i)
            {
                int index = nums[i] + offset + 1;
                res[i] = tree.Query(index - 1); // find # of nums that is smaller than nums[i]
                tree.Update(index, 1); // increment the # of nums that smaller and equal to nums[i]
            }

            return res;
        }
    }

    public class BIT
    {
        private int[] buffer;

        public BIT(int size)
        {
            this.buffer = new int[size];
        }

        public int Query(int index)
        {
            int res = 0;
            while (index > 0)
            {
                res += this.buffer[index];
                index -= index & (-index);
            }

            return res;
        }

        public void Update(int index, int val)
        {
            while (index < this.buffer.Length)
            {
                this.buffer[index] += val;
                index += index & (-index);
            }
        }
    }
}
