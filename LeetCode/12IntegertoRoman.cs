using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _12IntegertoRoman
    {
        private static readonly Dictionary<int, string> map = new Dictionary<int, string>()
        {
            { 1, "I"  },
            { 4, "IV" },
            { 5, "V"  },
            { 9, "IX" },
            { 10, "X" },
            { 40, "XL"},
            { 50, "L" },
            { 90, "XC" },
            { 100,"C" },
            { 400, "CD" },
            { 500, "D" },
            { 900, "CM" },
            { 1000, "M" }
        };

        private static int[] bases = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        public string IntToRoman(int num)
        {
            StringBuilder sb = new StringBuilder();
            int baseIndex = 0;
            while (num > 0)
            {
                int baseNum = bases[baseIndex];
                while (num >= baseNum)
                {
                    sb.Append(map[baseNum]);
                    num -= baseNum;
                }

                ++baseIndex;
            }

            return sb.ToString();
        }
    }
}
