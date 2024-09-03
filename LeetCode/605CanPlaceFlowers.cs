using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _605CanPlaceFlowers
    {
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            // the bed is not long enough even if empty for n flowers
            if (n > (flowerbed.Length + 1) / 2)
                return false;

            int planted = 0;
            for (int i = 0; i < flowerbed.Length && planted < n; ++i)
            {
                if (flowerbed[i] == 0 &&
                    (i == 0 || flowerbed[i - 1] == 0) &&
                    (i == flowerbed.Length - 1 || flowerbed[i + 1] == 0))
                {
                    // if we plant a flower at i, then we donot have to check i+1 so skip to i+2
                    flowerbed[i++] = 1;
                    ++planted;
                }
            }

            return planted >= n;
        }

        public bool CanPlaceFlowers1(int[] flowerbed, int n)
        {
            if (flowerbed.Length < n * 2 - 1)
                return false;

            for (int index = 0; index < flowerbed.Length; ++index)
            {
                if (n == 0)
                    return true;

                if (flowerbed[index] == 1)
                    continue;

                if (((index + 1 < flowerbed.Length && flowerbed[index + 1] == 0) || index == flowerbed.Length - 1) 
                    && ((index - 1 >= 0 && flowerbed[index - 1] == 0) || index == 0))
                {
                    --n;
                    flowerbed[index] = 1;
                }
            }

            return n == 0;
        }
    }
}
