using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _152MaximumProductSubarray
    {
        public int MaxProduct(int[] nums)
        {
            // Ignore 0s for a second, there are only two possiblities then:
            // even negative numbers OR
            //  odd negative numbers
            // if even, the the max product is the cumulative product of all nums
            // if odd,  the nums have to be split into two groups, and the first and last negative numbers must to be put in different groups.
            // to achieve this, we just traverse two passes from begining to end and then from end to begining
            // traverse nums from begining to end
            int result = int.MinValue, product = 1;
            for(int index = 0; index < nums.Length; ++index)
            {
                product *= nums[index];
                if (product > result)
                    result = product;

                if (product == 0)
                    product = 1;
            }

            // traverse nums from end to begining
            product = 1;
            for(int index = nums.Length - 1; index >= 0; --index)
            {
                product *= nums[index];
                if (product > result)
                    result = product;

                if (product == 0)
                    product = 1;
            }

            return result;
        }

        public int MaxProductDP(int[] nums)
        {
            // products[i] is the max products of nums including i
            int product = nums[0];
            // productsMin[i] is the min products of nums including i, we track this for negative nums before i and hope we would find another negative num later
            int productMin = nums[0];
            int maxProduct = nums[0];
            for (int index = 1; index < nums.Length; ++index)
            {
                int numAtIndex = nums[index];
                int productAtIndex = product * numAtIndex;
                int productMinAtIndex = productMin * numAtIndex;

                product = Math.Max(numAtIndex, Math.Max(productAtIndex, productMinAtIndex));
                if (product > maxProduct)
                    maxProduct = product;

                productMin = Math.Min(numAtIndex, Math.Min(productAtIndex, productMinAtIndex));
            }

            return maxProduct;
        }

        public int MaxProduct1D(int[] nums)
        {
            // products[i] is the max products of nums including i
            int[] products = new int[nums.Length];
            // productsMin[i] is the min products of nums including i, we track this for negative nums before i and hope we would find another negative num later
            int[] productsMin = new int[nums.Length];
            products[0] = nums[0];
            productsMin[0] = nums[0];
            int maxProduct = nums[0];
            for (int index = 1; index < nums.Length; ++index)
            {
                int numAtIndex = nums[index];
                int productAtIndex = products[index - 1] * numAtIndex;
                int productMinAtIndex = productsMin[index - 1] * numAtIndex;

                products[index] = Math.Max(numAtIndex, Math.Max(productAtIndex, productMinAtIndex));
                if (products[index] > maxProduct)
                    maxProduct = products[index];

                productsMin[index] = Math.Min(numAtIndex, Math.Min(productAtIndex, productMinAtIndex));
            }

            return maxProduct;
        }
    }
}
