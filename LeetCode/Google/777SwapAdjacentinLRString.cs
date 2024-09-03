using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _777SwapAdjacentinLRString
    {
        public bool CanTransform(string start, string end)
        {
            if (start.Length != end.Length)
                return false;

            int sIndex = 0, eIndex = 0;
            while (sIndex < start.Length && eIndex < end.Length)
            {
                while (sIndex < start.Length && start[sIndex] == 'X')
                    ++sIndex;

                while (eIndex < end.Length && end[eIndex] == 'X')
                    ++eIndex;

                if ((sIndex != start.Length && eIndex == end.Length) ||
                    (sIndex == start.Length && eIndex != end.Length))
                    return false;

                if (sIndex < start.Length && eIndex < end.Length)
                {
                    if (start[sIndex] != end[eIndex])
                        return false;

                    if (start[sIndex] == 'L' && sIndex < eIndex ||
                        start[sIndex] == 'R' && sIndex > eIndex)
                        return false;

                    ++sIndex;
                    ++eIndex;
                }
            }

            while (sIndex < start.Length && start[sIndex] == 'X')
                ++sIndex;

            while (eIndex < end.Length && end[eIndex] == 'X')
                ++eIndex;

            return sIndex == start.Length && eIndex == end.Length;
        }
    }
}
