using LC.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _875KokoEatingBananas
    {
        public int MinEatingSpeed(int[] piles, int h)
        {
            int minK = 1, maxK = int.MaxValue;
            //double sum = 0;
            //for(int i = 0; i < piles.Length; i++) 
            //{
            //    sum += piles[i];
            //    if (piles[i] > maxK)
            //    {
            //        maxK = piles[i];
            //    }
            //}

            //minK = (int)Math.Ceiling(sum / h);
            while (minK < maxK)
            {
                int midK = minK + ((maxK - minK) >> 1);
                int t = 0;
                for(int i = 0; i < piles.Length; ++i)
                {
                    t += (int)Math.Ceiling((double)piles[i] / midK);
                }

                if (t <= h)
                {
                    // might eat too fast, try with smaller k
                    maxK = midK;
                }
                else
                {
                    minK = midK + 1;
                }
            }

            return minK;
        }
    }
}
