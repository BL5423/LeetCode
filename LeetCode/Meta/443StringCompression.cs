using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _443StringCompression
    {
        public int Compress(char[] chars)
        {
            int index = -1, count = 0;
            char prior = ' ';
            for (int i = 0; i <= chars.Length; ++i)
            {
                if (i < chars.Length && chars[i] == prior)
                    ++count;
                else
                {
                    if (index >= 0)
                    {
                        chars[index++] = prior;
                        if (count > 1)
                        {
                            int j = index;
                            while (count != 0)
                            {
                                chars[index++] = (char)('0' + count % 10);
                                count /= 10;
                            }

                            int k = index - 1;
                            while (j < k)
                            {
                                char c = chars[j];
                                chars[j] = chars[k];
                                chars[k] = c;
                                ++j;
                                --k;
                            }
                        }

                        count = 1;
                    }
                    else
                    {
                        ++index;
                        ++count;
                    }

                    if (i < chars.Length)
                    {
                        // reset
                        prior = chars[i];
                    }
                }
            }

            return index;
        }
    }
}
