using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _14LongestCommonPrefix
    {
        public string LongestCommonPrefix(string[] strs)
        {
            string str0 = strs[0];
            for (int i = 0; i < str0.Length; ++i)
            {
                for (int index = 1; index < strs.Length; ++index)
                {
                    var str = strs[index];
                    if (i == str.Length || str[i] != str0[i])
                        return str0.Substring(0, i);
                }
            }

            return str0;
        }

        public string LongestCommonPrefixV1(string[] strs)
        {
            int index = 0;
            string prefix = strs[index++];
            for(; index < strs.Length && !string.IsNullOrEmpty(prefix); ++index)
            {
                var str = strs[index];
                prefix = FindPrefix(prefix, str);
            }

            return prefix;
        }

        private string FindPrefix(string prefix, string str)
        {
            int index = 0;
            while (index < prefix.Length && index < str.Length)
            {
                if (prefix[index] == str[index])
                {
                    ++index;
                    continue;
                }

                break;
            }

            if (index > 0)
            {
                return prefix.Substring(0, index);
            }

            return string.Empty;
        }
    }
}
