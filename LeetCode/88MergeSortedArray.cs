using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _88MergeSortedArray
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (m == 0)
            {
                for(int i = 0; i < n; ++i)
                {
                    nums1[i] = nums2[i];
                }
            }
            if (n > 0)
            {
                int index = m + n - 1;
                int index1 = m - 1, index2 = n - 1;
                while (index >= 0)
                {
                    if (index1 < 0 || index2 < 0)
                        break;

                    if (nums1[index1] >= nums2[index2])
                    {
                        nums1[index] = nums1[index1];
                        --index1;
                    }
                    else
                    {
                        nums1[index] = nums2[index2];
                        --index2;
                    }

                    --index;
                }

                if (index1 >= 0)
                {
                    while(index >= 0)
                    {
                        nums1[index--] = nums1[index1--];
                    }
                }
                else if (index2 >= 0)
                {
                    while (index >= 0)
                    {
                        nums1[index--] = nums2[index2--];
                    }
                }
            }
        }
    }
}
