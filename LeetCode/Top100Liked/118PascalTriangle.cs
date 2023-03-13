using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _118PascalTriangle
    {
        public IList<IList<int>> Generate(int numRows)
        {
            int num = 0;
            var res = new List<IList<int>>(numRows);
            IList<int> priorList = null;
            for(int r = 0; r < numRows; ++r)
            {
                var list = new List<int>(++num);
                if (num > 2)
                {
                    list.Add(1);
                    for(int i = 0; i < priorList.Count - 1; ++i)
                    {
                        list.Add(priorList[i] + priorList[i + 1]);
                    }
                    list.Add(1);
                }
                else
                {
                    list.AddRange(Enumerable.Repeat(1, num));
                }

                res.Add(list);
                priorList = list;
            }

            return res;
        }
    }
}
