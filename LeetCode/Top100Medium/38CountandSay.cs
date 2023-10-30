using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _38CountandSay
    {
        public string CountAndSay(int n)
        {
            if (n == 1)
                return "1";

            string res = "1";
            for(int k = 2; k <= n; ++k)
            {
                StringBuilder sb = new StringBuilder();
                int count = 1;
                char prev = res[0];
                for (int i = 1; i < res.Length; ++i)
                {
                    if (res[i] == prev)
                        ++count;
                    else
                    {
                        sb.Append(count);
                        sb.Append(prev);
                        count = 1;
                    }

                    prev = res[i];
                }

                if (count != 0)
                {
                    sb.Append(count);
                    sb.Append(prev);
                }

                res = sb.ToString();
            }

            return res;
        }

        public string CountAndSay_Recursive(int n)
        {
            if (n == 1)
                return "1";

            string res = CountAndSay(n - 1);
            StringBuilder sb = new StringBuilder();
            int count = 1;
            char prev = res[0];
            for (int i = 1; i < res.Length; ++i)
            {
                if (res[i] == prev)
                    ++count;
                else
                {
                    sb.Append(count);
                    sb.Append(prev);
                    count = 1;
                }

                prev = res[i];
            }

            if (count != 0)
            {
                sb.Append(count);
                sb.Append(prev);
            }

            return sb.ToString();
        }
    }
}
