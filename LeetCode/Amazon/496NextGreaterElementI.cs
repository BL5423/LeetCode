using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _496NextGreaterElementI
    {
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            var mapping = new Dictionary<int, int>(nums2.Length);
            int[] nextGreater = new int[nums2.Length];
            nextGreater[nums2.Length - 1] = -1; // no next greater for last num
            Stack<int> stack = new Stack<int>(nums2.Length);
            stack.Push(nums2.Length - 1);
            mapping.Add(nums2[nums2.Length - 1], nums2.Length - 1);
            for (int i = nums2.Length - 2; i >= 0; --i)
            {
                mapping.Add(nums2[i], i);

                nextGreater[i] = -1; // default
                while (stack.Count != 0 && nums2[stack.Peek()] < nums2[i])
                    stack.Pop();

                if (stack.Count != 0)
                    nextGreater[i] = stack.Peek();
                stack.Push(i);
            }

            int[] res = new int[nums1.Length];
            for (int i = 0; i < nums1.Length; ++i)
            {
                // nums1[i] in nums2
                int index = mapping[nums1[i]];
                res[i] = nextGreater[index] != -1 ? nums2[nextGreater[index]] : -1;
            }

            return res;
        }
    }
}
