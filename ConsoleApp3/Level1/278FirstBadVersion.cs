using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _278FirstBadVersion
    {
        public int FirstBadVersion(int n)
        {
            Dictionary<int, bool> results = new Dictionary<int, bool>();
            int start = 1, end = n;
            int index = -1;
            while (start <= end)
            {
                int middle = start + (end - start) / 2;
                bool bad = false;
                if ((results.TryGetValue(middle, out bad) && bad) || (bad = IsBadVersion(middle)))
                {
                    index = middle;
                    end = (middle != end ? middle : middle - 1);
                }
                else
                {
                    start = middle + 1;
                }

                results[middle] = bad;
            }

            return index;
        }

        private bool IsBadVersion(int version)
        {
            return true;
        }
    }
}
