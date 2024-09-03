using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    internal class _135Candy
    {
        public int Candy(int[] ratings)
        {
            int[] candy = new int[ratings.Length];
            candy[0] = 1;
            for (int i = 1; i < ratings.Length; ++i)
            {
                if (ratings[i] > ratings[i - 1])
                    candy[i] += candy[i - 1] + 1;
                else
                    candy[i] = 1;
            }

            int sum = candy[candy.Length - 1];
            for (int i = ratings.Length - 2; i >= 0; --i)
            {
                if (ratings[i] > ratings[i + 1])
                {
                    candy[i] = Math.Max(candy[i], candy[i + 1] + 1);
                }

                sum += candy[i];
            }

            return sum;
        }
    }
}
