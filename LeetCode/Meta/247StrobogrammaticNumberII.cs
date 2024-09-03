using LC.Level3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _247StrobogrammaticNumberII
    {
        private static char[] Map;

        private static char[] Candidates = new char[] { '1', '6', '8', '9' };

        static _247StrobogrammaticNumberII()
        {
            Map = new char[10];
            Map[0] = '0';
            Map[1] = '1';
            Map[6] = '9';
            Map[8] = '8';
            Map[9] = '6';
        }

        public IList<string> FindStrobogrammatic(int n)
        {
            IList<string>[] res = new List<string>[n + 1];

            res[1] = new List<string>() { "0", "1", "8" };
            if (n == 1)
                return res[1];

            res[2] = new List<string>() { "11", "69", "88", "96" };
            if (n == 2)
                return res[2];

            res[2].Insert(0, "00");
            for (int k = n % 2 == 0 ? 4 : 3; k <= n; k += 2)
            {
                // A (k - 2) A
                var list = new List<string>(res[k - 2].Count * 5);
                foreach (var str in res[k - 2])
                {
                    if (k != n)
                        list.Add(string.Format("0{0}0", str));
                    for (int i = 0; i < Candidates.Length; ++i)
                    {
                        list.Add(string.Format("{0}{1}{2}", Candidates[i], str, Map[Candidates[i] - '0']));
                    }
                }

                res[k] = list;
            }

            return res[n];
        }
    }
}
