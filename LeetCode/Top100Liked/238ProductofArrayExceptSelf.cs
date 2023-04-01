using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _238ProductofArrayExceptSelf
    {
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] res = new int[nums.Length];
            res[nums.Length - 1] = nums[nums.Length - 1];
            for(int i = nums.Length - 2; i >= 0; --i)
            {
                res[i] = res[i + 1] * nums[i];
            }

            int productSoFar = 1;
            for(int i = 0; i < nums.Length - 1; ++i)
            {
                res[i] = productSoFar * res[i + 1];
                productSoFar *= nums[i];
            }
            res[res.Length - 1] = productSoFar;

            return res;
        }

        public int[] ProductExceptSelfV1(int[] nums)
        {
            int[] res = new int[nums.Length];
            int[] p = new int[nums.Length], rp = new int[nums.Length];
            p[0] = nums[0];
            rp[nums.Length - 1] = nums[nums.Length - 1];
            int index1 = 1, index2 = nums.Length - 2;
            while (index1 < nums.Length)
            {
                p[index1] = p[index1 - 1] * nums[index1];
                rp[index2] = rp[index2 + 1] * nums[index2];

                ++index1;
                --index2;
            }

            res[0] = rp[1];
            res[nums.Length - 1] = p[nums.Length - 2];
            for(int i = 1; i < res.Length - 1; ++i)
            {
                res[i] = p[i - 1] * rp[i + 1];
            }

            return res;
        }
    }
}
