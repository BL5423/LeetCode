using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _904FruitIntoBaskets
    {

        public int TotalFruit(int[] fruits)
        {
            if (fruits.Length <= 2)
                return fruits.Length;

            var counts = new int[fruits.Length];
            int maxLen = 0;
            int left = 0, right = 0, curFruits = 0;
            while (right < fruits.Length)
            {
                if (++counts[fruits[right]] == 1)
                {
                    // new fruit
                    ++curFruits;
                }

                if (curFruits <= 2)
                {
                    // valid sliding window
                    maxLen = Math.Max(maxLen, right - left + 1);
                }
                else // curFruits > 2
                {
                    // need to shrink
                    while (curFruits > 2 && left < right)
                    {
                        if (--counts[fruits[left++]] == 0)
                            --curFruits;
                    }
                }

                ++right;
            }

            return maxLen;
        }
    }
}
