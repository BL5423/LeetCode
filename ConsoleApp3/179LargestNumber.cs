using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _179LargestNumber
    {
        public string LargestNumber(int[] nums)
        {
            Array.Sort(nums, new MyComparer());

            StringBuilder sb = new StringBuilder();
            foreach(int num in nums)
            {
                if (sb.Length == 0 && num == 0)
                    continue;

                sb.Append(num);
            }

            if (sb.Length == 0)
                sb.Append("0");

            return sb.ToString();
        }

        public long SmallestNumber(long num)
        {
            if (num == 0)
                return 0;

            long abNum = Math.Abs(num);
            char[] strNum = abNum.ToString().ToCharArray();
            Array.Sort(strNum, (x, y) => { return ((num < 0) ? -1 : 1) * string.Concat(x, y).CompareTo(string.Concat(y, x)); });

            StringBuilder sb = new StringBuilder();
            int leadingZeros = 0;
            foreach(var ch in strNum)
            {
                if (ch == '0' && sb.Length == 0)
                {
                    ++leadingZeros;
                    continue;
                }

                sb.Append(ch);
                if (leadingZeros > 0)
                {
                    sb.Append(new string('0', leadingZeros));
                    leadingZeros = 0;
                }
            }

            long result = long.Parse(sb.ToString());
            if (num < 0)
                return -result;

            return result;
        }
    }

    public class MyComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            string yStr = y.ToString();
            string xStr = x.ToString();

            var xy = xStr + yStr;
            var yx = yStr + xStr;

            return yx.CompareTo(xy);
        }
    }
}
