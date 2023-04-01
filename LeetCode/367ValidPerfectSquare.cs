using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _367ValidPerfectSquare
    {
        public bool IsPerfectSquare(int num)
        {
            // Newton method
            // (x2 - x1) * f'(x1) = f(x2) - f(x1)
            // (x2 - x1) == delta(x)
            // f(x2) == 0
            // => delta(x) = - f(x1) / f'(x1)
            // => x2 = x1 + delta(x) = x1 - f(x1) / f'(x1)

            if (num < 2)
                return true;
            long n = num / 2;
            while (n * n > num)
            {
                n = (n + num / n) / 2;
            }

            return (n * n == num);
        }

        public bool IsPerfectSquareV1(int num)
        {
            int left = 1, right = num;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                float divided = num / (float)mid;
                if (mid == divided)
                    return true;
                else if (mid > divided)
                    right = mid - 1;
                else // mid < divided
                    left = mid + 1;
            }

            return false;
        }
    }
}
