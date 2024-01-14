using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1419MinimumNumberofFrogsCroaking
    {
        public int MinNumberOfFrogs(string croakOfFrogs)
        {
            if (croakOfFrogs.Length < 5)
                return -1;

            int parallelFrogs = int.MinValue;
            var currentFrogs = new int['z' - 'a'];
            int total = 0;
            foreach (char croak in croakOfFrogs)
            {
                if (croak == 'c')
                {
                    // a new croaking frog found
                    // it is a frog waits for 'r'
                    ++currentFrogs['r' - 'a'];
                    ++total;
                }
                else
                {
                    // no frog is waiting for the current croak
                    if (currentFrogs[croak - 'a'] == 0)
                        return -1;

                    --currentFrogs[croak - 'a'];
                    --total;
                    switch (croak)
                    {
                        case 'r':
                            ++currentFrogs['o' - 'a'];
                            ++total;
                            break;
                        case 'o':
                            ++currentFrogs['a' - 'a'];
                            ++total;
                            break;
                        case 'a':
                            ++currentFrogs['k' - 'a'];
                            ++total;
                            break;
                        case 'k':
                            break;

                        default:
                            break;
                    }
                }

                parallelFrogs = Math.Max(parallelFrogs, total);
            }

            return total == 0 ? parallelFrogs : -1;
        }
    }
}
