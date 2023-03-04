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
