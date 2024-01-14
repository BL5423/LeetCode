using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public abstract class _277FindtheCelebrity
    {
        public int FindCelebrity(int n)
        {
            int candidate = 0, next = 1;
            while (next < n)
            {
                if (Knows(candidate, next))
                {
                    // candidate can not be the celebrity                    
                    // next could be the celebrity
                    candidate = next++;
                }
                else // next can not be the celebrity
                {
                    ++next;
                }
            }

            int knows = 0;
            for (int i = 0; i < n; ++i)
            {
                if (candidate != i && Knows(candidate, i))
                    return -1;
                if (Knows(i, candidate))
                    ++knows;
            }

            return knows == n ? candidate : -1;
        }

        protected abstract bool Knows(int person1, int person2);
    }
}
