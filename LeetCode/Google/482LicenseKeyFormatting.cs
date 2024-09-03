using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _482LicenseKeyFormatting
    {
        public string LicenseKeyFormatting(string s, int k)
        {
            var list = new LinkedList<char>();
            int len = 0;
            for (int i = s.Length - 1; i >= 0; --i)
            {
                if (s[i] == '-')
                    continue;

                list.AddFirst(char.ToUpper(s[i]));
                ++len;

                if (i != 0 && len % k == 0)
                {
                    list.AddFirst('-');
                }
            }
            if (list.Count != 0 && list.First() == '-')
                list.RemoveFirst();


            return string.Join("", list);
        }
    }
}
