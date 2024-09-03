using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _2781LengthoftheLongestValidSubstring
    {
        public int LongestValidSubstring(string word, IList<string> forbidden)
        {
            var dic = new HashSet<string>(forbidden);
            int res = 0, right = word.Length - 1;
            int maxLen = forbidden.Max(f => f.Length);
            for (int left = word.Length - 1; left >= 0; --left)
            {
                for (int r = left; r <= Math.Min(right, left + maxLen); ++r)
                {
                    if (dic.Contains(word.Substring(left, r - left + 1)))
                    {
                        // if 'bc' is forbidden, then we make right points to 'b'
                        // since we don't have to check 'bc' ever again
                        right = r - 1;
                        break;
                    }
                }

                res = Math.Max(res, right - left + 1);
            }

            return res;
        }
    }
}
