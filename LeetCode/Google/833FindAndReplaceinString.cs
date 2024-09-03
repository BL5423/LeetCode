using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _833FindAndReplaceinString
    {
        public string FindReplaceString(string s, int[] indices, string[] sources, string[] targets)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            int[] sortedIndices = new int[indices.Length];
            for (int i = 0; i < indices.Length; ++i)
                sortedIndices[i] = i;

            Array.Sort(sortedIndices, (a, b) => indices[a] - indices[b]);

            StringBuilder sb = new StringBuilder(s.Length);
            int sIndex = 0;
            for (int i = 0; i < sortedIndices.Length; ++i)
            {
                int index = sortedIndices[i];
                if (sIndex < indices[index])
                {
                    sb.Append(s.Substring(sIndex, indices[index] - sIndex));
                    sIndex = indices[index];
                }

                bool match = false;
                if (indices[index] + sources[index].Length <= s.Length)
                {
                    match = true;
                    for (int j = indices[index], k = 0; j < s.Length && k < sources[index].Length; ++j, ++k)
                    {
                        if (s[j] != sources[index][k])
                        {
                            match = false;
                            break;
                        }
                    }
                }


                if (match)
                {
                    sb.Append(targets[index]);
                    sIndex += sources[index].Length;
                }
            }

            if (sIndex < s.Length)
            {
                sb.Append(s.Substring(sIndex));
            }

            return sb.ToString();
        }
    }
}
