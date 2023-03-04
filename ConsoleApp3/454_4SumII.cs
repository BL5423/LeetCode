using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _454_4SumII
    {
        public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);
            Array.Sort(nums3);
            Array.Sort(nums4);

            int zeros = 0;
            Dictionary<int, int> dic12 = BuildDictionary(nums1, nums2, nums3[0], nums4[0]);
            Dictionary<int, int> dic34 = BuildDictionary(nums3, nums4, nums1[0], nums2[0]);

            foreach (var num in dic12)
            {
                if (dic34.TryGetValue(-num.Key, out int count))
                {
                    zeros += (num.Value * count);
                }
            }

            return zeros;
        }

        public Dictionary<int, int> BuildDictionary(int[] nums1, int[] nums2, int minNum3, int minNum4)
        {
            Dictionary<int, int> dic12 = new Dictionary<int, int>();
            for(int i = 0; i < nums1.Length; ++i)               
            {
                int num1 = nums1[i];
                for (int j = 0; j < nums2.Length; ++j)
                {
                    int num2 = nums2[j];
                    int num = num1 + num2;
                    if (num + minNum3 + minNum4 > 0)
                        break;

                    if (dic12.TryGetValue(num, out int count))
                    {
                        ++dic12[num];
                    }
                    else
                    {
                        dic12.Add(num, 1);
                    }
                }
            }

            return dic12;
        }
    }
}
