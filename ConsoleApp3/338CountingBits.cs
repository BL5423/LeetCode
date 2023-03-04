using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _338CountingBits
    {
        public int[] CountBits(int n)
        {
            int[] res = new int[n + 1];
            res[0] = 0;
            for(int i = 1; i <= n; ++i)
            {
                // i is odd
                //if ((i & 1) == 1)
                //{
                //    res[i] = res[i - 1] + 1;
                //}
                //else
                //{
                    // i is even
                    res[i] = res[i >> 1] + (i & 1);
                //}
            }

            return res;
        }
    }
}
